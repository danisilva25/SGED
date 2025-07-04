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
    public class Turma_ProfessorController : ControllerBase
    {
        // GET: api/Turma_Professor
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                IEnumerable<Turma_ProfessorDTO> lista = await new Turma_Professor().Listar(null);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET: api/Turma_Professor/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Carregar(int id)
        {
            try
            {
                return Ok(await new Turma_Professor().Carregar(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST: api/Turma_Professor
        [HttpPost]
        public async Task<bool> Salvar(Turma_ProfessorDTO turmaProfessor)
        {
            try
            {
                return await new Turma_Professor().Salvar(turmaProfessor);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao salvar Turma_Professor! Erro: " + ex.Message);
                return false;
            }
        }

        // PUT: api/Turma_Professor/5
        [HttpPut("{id}")]
        public async Task<bool> Alterar(Turma_ProfessorDTO turmaProfessor)
        {
            try
            {
                return await new Turma_Professor().Salvar(turmaProfessor);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao alterar Turma_Professor! Erro: " + ex.Message);
                return false;
            }
        }

        // DELETE: api/Turma_Professor/5
        [HttpDelete("{id}")]
        public async Task<bool> Excluir(int id)
        {
            try
            {
                return await new Turma_Professor().Excluir(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao excluir Turma_Professor! Erro: " + ex.Message);
                return false;
            }
        }
    }
}
