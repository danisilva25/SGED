using Microsoft.AspNetCore.Mvc;
using SGED.Domain;
using SGED.Models;

namespace SGED.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Aluno_AtividadeController : ControllerBase
    {
        // GET: api/Aluno_Atividade
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                IEnumerable<Aluno_AtividadeDTO> lista = await new Aluno_Atividade().Listar(null);
                return Ok(lista);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET: api/Aluno_Atividade/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Carregar(int id)
        {
            try
            {
                return Ok(await new Aluno_Atividade().Carregar(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST: api/Aluno_Atividade
        [HttpPost]
        public async Task<Boolean> Salvar(Aluno_AtividadeDTO Aluno_Atividade)
        {
            try
            {
                return await new Aluno_Atividade().Salvar(Aluno_Atividade);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Erro na Controller ao salvar Aluno_Atividade! Erro: " + ex.Message);
                return false;
            }
        }

        // PUT: api/Aluno_Atividade/5
        [HttpPut("{id}")]
        public async Task<Boolean> Alterar(Aluno_AtividadeDTO Aluno_Atividade)
        {
            try
            {
                return await new Aluno_Atividade().Salvar(Aluno_Atividade);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao alterar Aluno_Atividade! Erro: " + ex.Message);
                return false;
            }
        }

        // DELETE: api/Aluno_Atividade/5
        [HttpDelete("{id}")]
        public async Task<Boolean> Excluir(int id)
        {
            try
            {
                return await new Aluno_Atividade().Excluir(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao excluir Aluno_Atividade! Erro: " + ex.Message);
                return false;
            }

        }
    }
}
