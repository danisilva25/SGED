using Microsoft.AspNetCore.Mvc;
using SGED.Domain;
using SGED.Models;

namespace SGED.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiretorController : ControllerBase
    {
        // GET: api/Diretor
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                IEnumerable<DiretorDTO> lista = await new Diretor().Listar(null);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET: api/Diretor/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Carregar(int id)
        {
            try
            {
                return Ok(await new Diretor().Carregar(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST: api/Diretor
        [HttpPost]
        public async Task<Boolean> Salvar(DiretorDTO diretor)
        {
            try
            {
                return await new Diretor().Salvar(diretor);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao salvar Diretor! Erro: " + ex.Message);
                return false;
            }
        }

        // PUT: api/Diretor/5
        [HttpPut("{id}")]
        public async Task<Boolean> Alterar(DiretorDTO diretor)
        {
            try
            {
                return await new Diretor().Salvar(diretor);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao alterar Diretor! Erro: " + ex.Message);
                return false;
            }
        }

        // DELETE: api/Diretor/5
        [HttpDelete("{id}")]
        public async Task<Boolean> Excluir(int id)
        {
            try
            {
                return await new Diretor().Excluir(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao excluir Diretor! Erro: " + ex.Message);
                return false;
            }
        }
    }
}