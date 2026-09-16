using Microsoft.AspNetCore.Mvc;
using SGED.Application.Curriculo.Disciplinas.AlterarDisciplina;
using SGED.Application.Curriculo.Disciplinas.CadastrarDisciplina;
using SGED.Application.Curriculo.Disciplinas.DeletarDisciplina;
using SGED.Application.Curriculo.Disciplinas.ListarDisciplinas;

namespace SGEDApi.Controllers.Disciplinas;

[ApiController]
[Route("api/disciplinas")]
public class DisciplinaController(
    CadastrarDisciplinaHandler handler,
    ListarDisciplinaHandler listarHandler,
    DeletarDisciplinaHandler deletarHandler,
    AlterarDisciplinaHandler alterarHandler) : ControllerBase
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
    
    [HttpDelete]
    public async Task<IActionResult> Delete(
        DeletarDisciplinaCommand command,
        CancellationToken cancellationToken)
    {
        var result = await deletarHandler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);
        
        return Ok(result.Value);
    }

    [HttpPut]
    public async Task<IActionResult> Edit(
        AlterarDisciplinaCommand command,
        CancellationToken cancellationToken)
    {
        var result = await alterarHandler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);
        
        return Ok(result.Value);
    }
}