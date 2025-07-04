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
    public class ProfessorController : ControllerBase
    {
        // GET: api/Professor
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                IEnumerable<ProfessorDTO> lista = await new Professor().Listar(null);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET: api/Professor/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Carregar(int id)
        {
            try
            {
                return Ok(await new Professor().Carregar(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST: api/Professor
        [HttpPost]
        public async Task<bool> Salvar(ProfessorDTO professor)
        {
            try
            {
                return await new Professor().Salvar(professor);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao salvar Professor! Erro: " + ex.Message);
                return false;
            }
        }

        // PUT: api/Professor/5
        [HttpPut("{id}")]
        public async Task<bool> Alterar(ProfessorDTO professor)
        {
            try
            {
                return await new Professor().Salvar(professor);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao alterar Professor! Erro: " + ex.Message);
                return false;
            }
        }

        // DELETE: api/Professor/5
        [HttpDelete("{id}")]
        public async Task<bool> Excluir(int id)
        {
            try
            {
                return await new Professor().Excluir(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao excluir Professor! Erro: " + ex.Message);
                return false;
            }
        }
    }
}
