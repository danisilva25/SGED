namespace Domain.Entidades;

public class BnccProcessamentoControle
{
    public int Id { get; set; }

    public int PaginaInicial { get; set; }

    public int PaginaFinal { get; set; }

    public string Status { get; set; } = "Processando";

    public string? ResultadoJson { get; set; }

    public string? Erro { get; set; }
}