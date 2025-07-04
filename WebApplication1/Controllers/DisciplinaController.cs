using Microsoft.AspNetCore.Mvc;
using SGED.Domain;
using SGED.Models;

namespace SGED.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisciplinaController : ControllerBase
    {
        // GET: api/Disciplina
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                IEnumerable<DisciplinaDTO> lista = await new Disciplina().Listar(null);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET: api/Disciplina/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Carregar(int id)
        {
            try 
            {
                return Ok(await new Disciplina().Carregar(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST: api/Disciplina
        [HttpPost]
        public async Task<Boolean> Salvar(DisciplinaDTO disciplina)
        {
            try
            {
                return await new Disciplina().Salvar(disciplina);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao salvar Disciplina! Erro: " + ex.Message);
                return false;
            }
        }

        // PUT: api/Disciplina/5
        [HttpPut("{id}")]
        public async Task<Boolean> Alterar(DisciplinaDTO disciplina)
        {
            try
            {
                return await new Disciplina().Salvar(disciplina);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao alterar Disciplina! Erro: " + ex.Message);
                return false;
            }
        }

        // DELETE: api/Disciplina/5
        [HttpDelete("{id}")]
        public async Task<Boolean> Excluir(int id)
        {
            try
            {
                return await new Disciplina().Excluir(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao excluir Disciplina. Erro: " + ex.Message);
                return false;
            }
        }
    }
}
