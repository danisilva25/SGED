namespace Application.Mapeadores.CatalogoComponentesCurriculares;

public sealed class CatalogoComponentesCurriculares
{
    private static readonly Dictionary<string, string> Siglas =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["Língua Portuguesa"] = "LP",
            ["Matemática"] = "MAT",
            ["História"] = "HIST",
            ["Geografia"] = "GEO",
            ["Biologia"] = "BIO",
            ["Física"] = "FIS",
            ["Química"] = "QUI",
            ["Educação Física"] = "EDF",
            ["Arte"] = "ART",
            ["Língua Inglesa"] = "ING",
            ["Filosofia"] = "FIL",
            ["Sociologia"] = "SOC"
        };

    public bool Possui(string nome)
        => Siglas.ContainsKey(nome);

    public string Obter(string nome)
    {
        if (!Siglas.TryGetValue(nome, out var sigla))
            throw new InvalidOperationException(
                $"Componente curricular sem sigla cadastrada: {nome}");

        return sigla;
    }
}