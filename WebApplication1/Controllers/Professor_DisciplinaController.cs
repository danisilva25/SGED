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
    public class Professor_DisciplinaController : ControllerBase
    {
        // GET: api/Professor_Disciplina
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                IEnumerable<Professor_DisciplinaDTO> lista = await new Professor_Disciplina().Listar(null);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET: api/Professor_Disciplina/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Carregar(int id)
        {
            try
            {
                return Ok(await new Professor_Disciplina().Carregar(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST: api/Professor_Disciplina
        [HttpPost]
        public async Task<Boolean> Salvar(Professor_DisciplinaDTO professorDisciplina)
        {
            try
            {
                return await new Professor_Disciplina().Salvar(professorDisciplina);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao salvar Professor_Disciplina! Erro: " + ex.Message);
                return false;
            }
        }

        // PUT: api/Professor_Disciplina/5
        [HttpPut("{id}")]
        public async Task<Boolean> Alterar(Professor_DisciplinaDTO professorDisciplina)
        {
            try
            {
                return await new Professor_Disciplina().Salvar(professorDisciplina);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao alterar Professor_Disciplina! Erro: " + ex.Message);
                return false;
            }
        }

        // DELETE: api/Professor_Disciplina/5
        [HttpDelete("{id}")]
        public async Task<Boolean> Excluir(int id)
        {
            try
            {
                return await new Professor_Disciplina().Excluir(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao excluir Professor_Disciplina! Erro: " + ex.Message);
                return false;
            }
        }
    }
}
