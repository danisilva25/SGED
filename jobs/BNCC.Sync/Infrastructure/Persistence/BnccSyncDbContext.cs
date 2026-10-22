using Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class BnccSyncDbContext(
    DbContextOptions<BnccSyncDbContext> options)
    : DbContext(options)
{
    public DbSet<BnccProcessamentoControle> Processamentos =>
        Set<BnccProcessamentoControle>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var entidade = modelBuilder.Entity<BnccProcessamentoControle>();

        entidade.ToTable("bncc_processamento_controle");

        entidade.HasKey(x => x.Id);

        entidade.Property(x => x.Id)
            .HasColumnName("id")
            .UseIdentityAlwaysColumn();

        entidade.Property(x => x.PaginaInicial)
            .HasColumnName("pagina_inicial")
            .IsRequired();

        entidade.Property(x => x.PaginaFinal)
            .HasColumnName("pagina_final")
            .IsRequired();

        entidade.Property(x => x.Status)
            .HasColumnName("status")
            .HasMaxLength(30)
            .IsRequired();

        entidade.Property(x => x.ResultadoJson)
            .HasColumnName("resultado_json")
            .HasColumnType("jsonb");

        entidade.Property(x => x.Erro)
            .HasColumnName("erro")
            .HasColumnType("text");
    }
}