using Domain.Entidades;
using Infrastructure.Json.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Infrastructure.Persistence;

public sealed class ControleProcessamentoBncc(
    IDbContextFactory<BnccSyncDbContext> contextFactory)
{
    public async Task InicializarAsync(
        int totalPaginas,
        int paginasPorLote,
        CancellationToken cancellationToken = default)
    {
        await using var db = await contextFactory.CreateDbContextAsync(
            cancellationToken);

        var controle = await db.Processamentos
            .FirstOrDefaultAsync(cancellationToken);

        if (controle is not null)
            return;

        if (totalPaginas < 1 || paginasPorLote < 1)
            throw new ArgumentOutOfRangeException(
                nameof(totalPaginas),
                "O total de páginas e o tamanho do lote devem ser positivos.");

        controle = new BnccProcessamentoControle
        {
            PaginaInicial = 1,
            PaginaFinal = Math.Min(paginasPorLote, totalPaginas),
            Status = "Processando",
            ResultadoJson = "[]",
            Erro = null
        };

        db.Processamentos.Add(controle);
        await db.SaveChangesAsync(cancellationToken);
    }

    public async Task<BnccProcessamentoControle?> ObterAsync(
        CancellationToken cancellationToken = default)
    {
        await using var db = await contextFactory.CreateDbContextAsync(
            cancellationToken);

        return await db.Processamentos
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> LoteJaConcluidoAsync(
        int paginaInicial,
        CancellationToken cancellationToken = default)
    {
        var controle = await ObterAsync(cancellationToken);
        
        if (controle is null)
            return false;
        
        return paginaInicial < controle.PaginaInicial;
    }

    public async Task RegistrarLoteAsync(
        int paginaInicial,
        int paginaFinal,
        Root resultado,
        int totalPaginas,
        int paginasPorLote,
        CancellationToken cancellationToken = default)
    {
        await using var db = await contextFactory.CreateDbContextAsync(
            cancellationToken);

        await using var transacao =
            await db.Database.BeginTransactionAsync(cancellationToken);

        var controle = await db.Processamentos
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new InvalidOperationException(
                "O controle do processamento não foi inicializado.");
        
        if (paginaInicial < controle.PaginaInicial)
        {
            await transacao.CommitAsync(cancellationToken);
            return;
        }
        
        if (paginaInicial != controle.PaginaInicial ||
            paginaFinal != controle.PaginaFinal)
            throw new InvalidOperationException(
                $"Lote inesperado {paginaInicial}-{paginaFinal}. " +
                $"O banco espera {controle.PaginaInicial}-{controle.PaginaFinal}.");

        var resultados = JsonConvert.DeserializeObject<List<Root>>(
            controle.ResultadoJson ?? "[]") ?? [];

        resultados.Add(resultado);

        controle.ResultadoJson = JsonConvert.SerializeObject(resultados);
        controle.Erro = null;

        var proximaPagina = paginaFinal + 1;

        if (proximaPagina > totalPaginas)
        {
            controle.Status = "Concluido";
            controle.PaginaInicial = proximaPagina;
            controle.PaginaFinal = totalPaginas;
        }
        else
        {
            controle.Status = "Processando";
            controle.PaginaInicial = proximaPagina;
            controle.PaginaFinal = Math.Min(
                proximaPagina + paginasPorLote - 1,
                totalPaginas);
        }

        await db.SaveChangesAsync(cancellationToken);
        await transacao.CommitAsync(cancellationToken);
    }

    public async Task RegistrarErroAsync(
        string status,
        string erro,
        CancellationToken cancellationToken = default)
    {
        await using var db = await contextFactory.CreateDbContextAsync(
            cancellationToken);

        var controle = await db.Processamentos
            .OrderBy(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (controle is null)
            return;

        controle.Status = status;
        controle.Erro = erro;

        await db.SaveChangesAsync(cancellationToken);
    }

    public async Task ExcluirControleAsync(
        CancellationToken cancellationToken = default)
    {
        await using var db = await contextFactory.CreateDbContextAsync(
            cancellationToken);

        var controle = await db.Processamentos
            .OrderBy(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (controle is null)
            return;

        db.Processamentos.Remove(controle);
        await db.SaveChangesAsync(cancellationToken);
    }
}