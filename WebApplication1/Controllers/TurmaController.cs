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
    [Route("api/[controller]")]
    public class TurmaController : ControllerBase
    {
        // GET: api/Turma
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                IEnumerable<TurmaDTO> lista = await new Turma().Listar(null);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET: api/Turma/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Carregar(int id)
        {
            try
            {
                return Ok(await new Turma().Carregar(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST: api/Turma
        [HttpPost]
        public async Task<bool> Salvar(TurmaDTO turma)
        {
            try
            {
                return await new Turma().Salvar(turma);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao salvar Turma! Erro: " + ex.Message);
                return false;
            }
        }

        // PUT: api/Turma/5
        [HttpPut("{id}")]
        public async Task<bool> Alterar(TurmaDTO turma)
        {
            try
            {
                return await new Turma().Salvar(turma);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao alterar Turma! Erro: " + ex.Message);
                return false;
            }
        }

        // DELETE: api/Turma/5
        [HttpDelete("{id}")]
        public async Task<bool> Excluir(int id)
        {
            try
            {
                return await new Turma().Excluir(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao excluir Turma! Erro: " + ex.Message);
                return false;
            }
        }
    }
}
