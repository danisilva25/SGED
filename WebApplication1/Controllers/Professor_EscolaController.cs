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
    public class Professor_EscolaController : ControllerBase
    {
        // GET: api/Professor_Escola
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                IEnumerable<Professor_EscolaDTO> lista = await new Professor_Escola().Listar(null);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET: api/Professor_Escola/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Carregar(int id)
        {
            try
            {
                return Ok(await new Professor_Escola().Carregar(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST: api/Professor_Escola
        [HttpPost]
        public async Task<bool> Salvar(Professor_EscolaDTO professorEscola)
        {
            try
            {
                return await new Professor_Escola().Salvar(professorEscola);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao salvar Professor_Escola! Erro: " + ex.Message);
                return false;
            }
        }

        // PUT: api/Professor_Escola/5
        [HttpPut("{id}")]
        public async Task<bool> Alterar(Professor_EscolaDTO professorEscola)
        {
            try
            {
                return await new Professor_Escola().Salvar(professorEscola);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao alterar Professor_Escola! Erro: " + ex.Message);
                return false;
            }
        }

        // DELETE: api/Professor_Escola/5
        [HttpDelete("{id}")]
        public async Task<bool> Excluir(int id)
        {
            try
            {
                return await new Professor_Escola().Excluir(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao excluir Professor_Escola! Erro: " + ex.Message);
                return false;
            }
        }
    }
}
