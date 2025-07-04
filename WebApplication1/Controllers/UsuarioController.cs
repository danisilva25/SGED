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
    public class UsuarioController : ControllerBase
    {
        // GET: api/Usuario
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                IEnumerable<UsuarioDTO> lista = await new Usuario().Listar(null);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET: api/Usuario/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Carregar(int id)
        {
            try
            {
                return Ok(await new Usuario().Carregar(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST: api/Usuario
        [HttpPost]
        public async Task<bool> Salvar(UsuarioDTO usuario)
        {
            try
            {
                return await new Usuario().Salvar(usuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao salvar Usuario! Erro: " + ex.Message);
                return false;
            }
        }

        // PUT: api/Usuario/5
        [HttpPut("{id}")]
        public async Task<bool> Alterar(UsuarioDTO usuario)
        {
            try
            {
                return await new Usuario().Salvar(usuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao alterar Usuario! Erro: " + ex.Message);
                return false;
            }
        }

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public async Task<bool> Excluir(int id)
        {
            try
            {
                return await new Usuario().Excluir(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao excluir Usuario! Erro: " + ex.Message);
                return false;
            }
        }
    }
}
