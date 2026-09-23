using SGED.Domain.Enums;

namespace SGED.Application.Curriculo.EtapasAnosEscolares.CadastrarEtapaAnoEscolar;

public record CadastrarEtapaAnoEscolarCommand(
    string NomeEtapaAnoEscolar,
    int? Codigo,
    Modalidade? Modalidade,
    int? Ordem
    );