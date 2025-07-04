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
    public class DiretoriaEnsinoController : ControllerBase
    {
        // GET: api/DiretoriaEnsino
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                IEnumerable<DiretoriaEnsinoDTO> lista = await new DiretoriaEnsino().Listar(null);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET: api/DiretoriaEnsino/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Carregar(int id)
        {
            try
            {
                return Ok(await new DiretoriaEnsino().Carregar(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST: api/DiretoriaEnsino
        [HttpPost]
        public async Task<Boolean> Salvar(DiretoriaEnsinoDTO diretoriaEnsino)
        {
            try
            {
                return await new DiretoriaEnsino().Salvar(diretoriaEnsino);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao salvar DiretoriaEnsino! Erro: " + ex.Message);
                return false;
            }
        }

        // PUT: api/DiretoriaEnsino/5
        [HttpPut("{id}")]
        public async Task<Boolean> Alterar(DiretoriaEnsinoDTO diretoriaEnsino)
        {
            try
            {
                return await new DiretoriaEnsino().Salvar(diretoriaEnsino);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao alterar DiretoriaEnsino! Erro: " + ex.Message);
                return false;
            }
        }

        // DELETE: api/DiretoriaEnsino/5
        [HttpDelete("{id}")]
        public async Task<Boolean> Excluir(int id)
        {
            try
            {
                return await new DiretoriaEnsino().Excluir(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao excluir DiretoriaEnsino! Erro: " + ex.Message);
                return false;
            }
        }
    }
}
