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
    public class Turma_DisciplinaController : ControllerBase
    {
        // GET: api/Turma_Disciplina
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                IEnumerable<Turma_DisciplinaDTO> lista = await new Turma_Disciplina().Listar(null);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET: api/Turma_Disciplina/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Carregar(int id)
        {
            try
            {
                return Ok(await new Turma_Disciplina().Carregar(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST: api/Turma_Disciplina
        [HttpPost]
        public async Task<bool> Salvar(Turma_DisciplinaDTO turmaDisciplina)
        {
            try
            {
                return await new Turma_Disciplina().Salvar(turmaDisciplina);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao salvar Turma_Disciplina! Erro: " + ex.Message);
                return false;
            }
        }

        // PUT: api/Turma_Disciplina/5
        [HttpPut("{id}")]
        public async Task<bool> Alterar(Turma_DisciplinaDTO turmaDisciplina)
        {
            try
            {
                return await new Turma_Disciplina().Salvar(turmaDisciplina);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao alterar Turma_Disciplina! Erro: " + ex.Message);
                return false;
            }
        }

        // DELETE: api/Turma_Disciplina/5
        [HttpDelete("{id}")]
        public async Task<bool> Excluir(int id)
        {
            try
            {
                return await new Turma_Disciplina().Excluir(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao excluir Turma_Disciplina! Erro: " + ex.Message);
                return false;
            }
        }
    }
}
