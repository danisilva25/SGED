using Microsoft.AspNetCore.Mvc;
using SGED.Domain;
using SGED.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace SGED.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtividadeController : ControllerBase
    {
        // GET: api/Atividade
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                IEnumerable<AtividadeDTO> lista = await new Atividade().Listar(null);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET: api/Atividade/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Carregar(int id)
        {
            try
            {
                return Ok(await new Atividade().Carregar(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST: api/Atividade
        [HttpPost]
        public async Task<Boolean> Salvar(AtividadeDTO atividade)
        {
            try
            {
                return await new Atividade().Salvar(atividade);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao salvar Atividade! Erro: " + ex.Message);
                return false;
            }
        }

        // PUT: api/Atividade/5
        [HttpPut("{id}")]
        public async Task<Boolean> Alterar(AtividadeDTO atividade)
        {
            try
            {
                return await new Atividade().Salvar(atividade);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao alterar Atividade! Erro: " + ex.Message);
                return false;
            }
        }

        // DELETE: api/Atividade/5
        [HttpDelete("{id}")]
        public async Task<Boolean> Excluir(int id)
        {
            try
            {
                return await new Atividade().Excluir(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao excluir Atividade! Erro: " + ex.Message);
                return false;
            }
        }
    }
}
