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
    public class EscolaController : ControllerBase
    {
        // GET: api/Escola
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                IEnumerable<EscolaDTO> lista = await new Escola().Listar(null);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET: api/Escola/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Carregar(int id)
        {
            try
            {
                return Ok(await new Escola().Carregar(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST: api/Escola
        [HttpPost]
        public async Task<Boolean> Salvar(EscolaDTO escola)
        {
            try
            {
                return await new Escola().Salvar(escola);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao salvar Escola! Erro: " + ex.Message);
                return false;
            }
        }

        // PUT: api/Escola/5
        [HttpPut("{id}")]
        public async Task<Boolean> Alterar(EscolaDTO escola)
        {
            try
            {
                return await new Escola().Salvar(escola);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao alterar Escola! Erro: " + ex.Message);
                return false;
            }
        }

        // DELETE: api/Escola/5
        [HttpDelete("{id}")]
        public async Task<Boolean> Excluir(int id)
        {
            try
            {
                return await new Escola().Excluir(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao excluir Escola! Erro: " + ex.Message);
                return false;
            }
        }
    }
}
