using Microsoft.AspNetCore.Mvc;
using SGED.Application.Curriculo.EtapasAnosEscolares.AlterarEtapaAnoEscolar;
using SGED.Application.Curriculo.EtapasAnosEscolares.CadastrarEtapaAnoEscolar;
using SGED.Application.Curriculo.EtapasAnosEscolares.DeletarEtapaAnoEscolar;
using SGED.Application.Curriculo.EtapasAnosEscolares.ListarEtapasAnosEscolares;
using SGED.Domain.Curriculo.EtapasAnosEscolares;

namespace SGEDApi.Controllers.EtapasAnosEscolares;

[ApiController]
[Route("api/anos-escolares")]
public class EtapaAnoEscolarController(
    CadastrarEtapaAnoEscolarHandler handler,
    ListarEtapaAnoEscolarHandler listarHandler,
    AlterarEtapaAnoEscolarHandler alterarHandler,
    DeletarEtapaAnoEscolarHandler deletarHandler) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<EtapaAnoEscolar>> Create(
        CadastrarEtapaAnoEscolarCommand command,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Created($"api/anos-escolares/{result.Value}", new { id = result.Value });
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
        DeletarEtapaAnoEscolarCommand command,
        CancellationToken cancellationToken)
    {
        var result = await deletarHandler.Handle(command, cancellationToken);

        if (result.IsFailure)
            BadRequest(result.Error);
        
        return Ok(result.Value);
    }

    [HttpPut]
    public async Task<IActionResult> Edit(
        AlterarEtapaAnoEscolarCommand command,
        CancellationToken cancellationToken)
    {
        var result = await alterarHandler.Handle(command, cancellationToken);

        if (result.IsFailure)
            BadRequest(result.Error);

        return Ok(result.Value);
    }
}