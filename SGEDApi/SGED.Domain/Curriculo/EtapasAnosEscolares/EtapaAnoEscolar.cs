using SGED.Domain.Common;
using SGED.Domain.Common.Results;

namespace SGED.Domain.Curriculo.EtapasAnosEscolares;

public sealed class EtapaAnoEscolar : Entity
{
    public string? Nome { get; set; }

    private EtapaAnoEscolar()
    {
    }

    private EtapaAnoEscolar(string nome)
        => Nome = nome;

    public static Result<EtapaAnoEscolar> Create(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            return Result<EtapaAnoEscolar>.Failure(new Error(
                "EtapaAnoEscolar.NomeObrigatorio",
                "O nome da Etapa Ano Escolar é obrigatório.",
                ErrorType.Validation
            ));

        return Result<EtapaAnoEscolar>.Success(
            new EtapaAnoEscolar(nome.Trim()));
    }

    public void UpdateName(string nome)
    {
        Nome = nome;
        MarkAsUpdated();
    }
}