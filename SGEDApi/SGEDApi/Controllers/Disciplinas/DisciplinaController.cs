using Microsoft.AspNetCore.Mvc;
using SGED.Application.Curriculo.Disciplinas.CadastrarDisciplina;
using SGED.Application.Curriculo.Disciplinas.ListarDisciplinas;

namespace SGEDApi.Controllers.Disciplinas;

[ApiController]
[Route("api/disciplinas")]
public class DisciplinaController(
    CadastrarDisciplinaHandler handler,
    ListarDisciplinaHandler listarHandler) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        CadastrarDisciplinaCommand command,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(command, cancellationToken);

        if(result.IsFailure)
            return BadRequest(result.Error);
        
        return Created($"api/disciplinas/{result.Value}", new { id = result.Value});
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await listarHandler.Handle(cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);
        
        return Ok(result.Value);
    }
}