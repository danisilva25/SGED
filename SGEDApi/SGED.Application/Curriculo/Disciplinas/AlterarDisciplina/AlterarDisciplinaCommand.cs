using SGED.Domain.Curriculo.Disciplinas;

namespace SGED.Application.Curriculo.Disciplinas.AlterarDisciplina;

public record AlterarDisciplinaCommand(Guid  IdDisciplina, string NomeDisciplina);