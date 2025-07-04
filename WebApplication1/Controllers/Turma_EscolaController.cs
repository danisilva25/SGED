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
    public class Turma_EscolaController : ControllerBase
    {
        // GET: api/Turma_Escola
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                IEnumerable<Turma_EscolaDTO> lista = await new Turma_Escola().Listar(null);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET: api/Turma_Escola/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Carregar(int id)
        {
            try
            {
                return Ok(await new Turma_Escola().Carregar(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST: api/Turma_Escola
        [HttpPost]
        public async Task<bool> Salvar(Turma_EscolaDTO turmaEscola)
        {
            try
            {
                return await new Turma_Escola().Salvar(turmaEscola);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao salvar Turma_Escola! Erro: " + ex.Message);
                return false;
            }
        }

        // PUT: api/Turma_Escola/5
        [HttpPut("{id}")]
        public async Task<bool> Alterar(Turma_EscolaDTO turmaEscola)
        {
            try
            {
                return await new Turma_Escola().Salvar(turmaEscola);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao alterar Turma_Escola! Erro: " + ex.Message);
                return false;
            }
        }

        // DELETE: api/Turma_Escola/5
        [HttpDelete("{id}")]
        public async Task<bool> Excluir(int id)
        {
            try
            {
                return await new Turma_Escola().Excluir(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao excluir Turma_Escola! Erro: " + ex.Message);
                return false;
            }
        }
    }
}
