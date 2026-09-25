using Microsoft.AspNetCore.Mvc;
using SGED.Application.Curriculo.Modalidades.AlterarModalidade;
using SGED.Application.Curriculo.Modalidades.CadastrarModalidade;
using SGED.Application.Curriculo.Modalidades.DeletarModalidade;
using SGED.Application.Curriculo.Modalidades.ListarModalidades;
using SGED.Domain.Curriculo.Modalidades;

namespace SGEDApi.Controllers.Modalidades;

[ApiController]
[Route("api/modalidades")]
public class ModalidadeController(
    CadastrarModalidadeHandler cadastrarHandler,
    ListarModalidadeHandler listarHandler,
    AlterarModalidadeHandler alterarHandler,
    DeletarModalidadeHandler deletarHandler
    ) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Modalidade>> Create(
        CadastrarModalidadeCommand command,
        CancellationToken cancellationToken)
    {
        var result = await cadastrarHandler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);
        
        return Created($"api/modalidades/{result.Value}", new { id = result.Value });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await listarHandler.Handle(cancellationToken);
        
        if(result.IsFailure)
            return BadRequest(result.Error);
        
        return Ok(result.Value);
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete(
        DeletarModalidadeCommand command,
        CancellationToken cancellationToken)
    {
        var result = await deletarHandler.Handle(command, cancellationToken);
        
        if (result.IsFailure)
            return BadRequest(result.Error);
        
        return Ok(result.Value);
    }

    [HttpPut]
    public async Task<IActionResult> Edit(
        AlterarModalidadeCommand command,
        CancellationToken cancellationToken)
    {
        var result = await alterarHandler.Handle(command, cancellationToken);
        
        if (result.IsFailure)
            return BadRequest(result.Error);
        
        return Ok(result.Value);
    }
}