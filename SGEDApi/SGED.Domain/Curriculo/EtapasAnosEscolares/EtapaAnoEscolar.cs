using SGED.Domain.Common;
using SGED.Domain.Common.Results;
using SGED.Domain.Enums;

namespace SGED.Domain.Curriculo.EtapasAnosEscolares;

public sealed class EtapaAnoEscolar : Entity
{
    public string? Nome { get; private set; }
    public int? Codigo { get; private set; }
    public Modalidade? Modalidade { get; private set; }
    public int? Ordem { get; private set; }

    private EtapaAnoEscolar()
    {
    }

    private EtapaAnoEscolar(string nome, int codigo, Modalidade modalidade, int ordem)
    {
        Nome = nome;
        Codigo = codigo;
        Modalidade = modalidade;
        Ordem = ordem;
    }

    private static Result<bool> ValidateEtapaAnoEscolar(
        string nome,
        int? codigo,
        Modalidade? modalidade,
        int? ordem)
    {
        if (string.IsNullOrWhiteSpace(nome))
            return Result<bool>.Failure(
                new Error(
                    "EtapaAnoEscolar.NomeObrigatorio",
                    "O nome da Etapa Ano Escolar é obrigatório.",
                    ErrorType.Validation));

        if (codigo is null)
            return Result<bool>.Failure(
                new Error(
                    "EtapaAnoEscolar.CodigoObrigatorio",
                    "O código da Etapa Ano Escolar é obrigatório.",
                    ErrorType.Validation));

        if (modalidade is null)
            return Result<bool>.Failure(
                new Error(
                    "EtapaAnoEscolar.ModalidadeObrigatoria",
                    "A modalidade da Etapa Ano Escolar é obrigatória.",
                    ErrorType.Validation));

        if (ordem is null)
            return Result<bool>.Failure(
                new Error(
                    "EtapaAnoEscolar.OrdemObrigatoria",
                    "A ordem da Etapa Ano Escolar é obrigatória.",
                    ErrorType.Validation));

        return Result<bool>.Success(true);
    }
    public static Result<EtapaAnoEscolar> Create(
        string nome,
        int? codigo,
        Modalidade? modalidade,
        int? ordem)
    {
        var validation = ValidateEtapaAnoEscolar(
            nome,
            codigo,
            modalidade,
            ordem);

        if (validation.IsFailure)
            return Result<EtapaAnoEscolar>.Failure(
                validation.Error!);

        return Result<EtapaAnoEscolar>.Success(
            new EtapaAnoEscolar(
                nome.Trim(),
                codigo!.Value,
                modalidade!.Value,
                ordem!.Value));
    }

    public Result<bool> Update(
        string nome,
        int? codigo,
        Modalidade? modalidade,
        int? ordem)
    {
        var validation = ValidateEtapaAnoEscolar(
            nome,
            codigo,
            modalidade,
            ordem);

        if (validation.IsFailure)
            return Result<bool>.Failure(
                validation.Error!);

        Nome = nome.Trim();
        Codigo = codigo!.Value;
        Modalidade = modalidade!.Value;
        Ordem = ordem.Value;

        MarkAsUpdated();

        return Result<bool>.Success(true);
    }
}