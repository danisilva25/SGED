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
    public class AlunoController : ControllerBase
    {
        // GET: api/Aluno
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                IEnumerable<AlunoDTO> lista = await new Aluno().Listar(null);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET: api/Aluno/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Carregar(int id)
        {
            try
            {
                return Ok(await new Aluno().Carregar(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST: api/Aluno
        [HttpPost]
        public async Task<Boolean> Salvar(AlunoDTO aluno)
        {
            try
            {
                return await new Aluno().Salvar(aluno);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao salvar Aluno! Erro: " + ex.Message);
                return false;
            }
        }

        // PUT: api/Aluno/5
        [HttpPut("{id}")]
        public async Task<Boolean> Alterar(AlunoDTO aluno)
        {
            try
            {
                return await new Aluno().Salvar(aluno);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao alterar Aluno! Erro: " + ex.Message);
                return false;
            }
        }

        // DELETE: api/Aluno/5
        [HttpDelete("{id}")]
        public async Task<Boolean> Excluir(int id)
        {
            try
            {
                return await new Aluno().Excluir(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao excluir Aluno! Erro: " + ex.Message);
                return false;
            }
        }
    }
}
