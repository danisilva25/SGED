using Microsoft.AspNetCore.Mvc;
using SGED.Application.Curriculo.Disciplinas.CadastrarDisciplina;

namespace SGEDApi.Controllers.Disciplinas;

[ApiController]
[Route("api/disciplinas")]
public class DisciplinaController(
    CadastrarDisciplinaHandler handler) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        CadastrarDisciplinaCommand command,
        CancellationToken cancellationToken)
    {
        var id = await handler.Handle(command, cancellationToken);
        
        return Created($"api/disciplinas/{id}", new { id });
    }
}