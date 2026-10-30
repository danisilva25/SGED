namespace Application.Validacao;

public record OcorrenciaValidacaoBncc(
    TipoOcorrenciaBncc Tipo,
    string Mensagem,
    string? Codigo,
    string Localizacao);