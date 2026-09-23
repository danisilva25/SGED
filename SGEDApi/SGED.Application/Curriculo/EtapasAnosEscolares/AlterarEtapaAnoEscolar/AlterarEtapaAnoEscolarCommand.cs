using SGED.Domain.Enums;

namespace SGED.Application.Curriculo.EtapasAnosEscolares.AlterarEtapaAnoEscolar;

public record AlterarEtapaAnoEscolarCommand(
    Guid IdEtapaAnoEscolar, 
    string NomeEtapaAnoEscolar,
    int? Codigo,
    Modalidade? Modalidade,
    int? Ordem);