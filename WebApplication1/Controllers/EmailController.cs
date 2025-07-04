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
    public class EmailController : ControllerBase
    {
        // GET: api/Email
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                IEnumerable<EmailDTO> lista = await new Email().Listar(null);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET: api/Email/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Carregar(int id)
        {
            try
            {
                return Ok(await new Email().Carregar(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST: api/Email
        [HttpPost]
        public async Task<Boolean> Salvar(EmailDTO email)
        {
            try
            {
                return await new Email().Salvar(email);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao salvar Email! Erro: " + ex.Message);
                return false;
            }
        }

        // PUT: api/Email/5
        [HttpPut("{id}")]
        public async Task<Boolean> Alterar(EmailDTO email)
        {
            try
            {
                return await new Email().Salvar(email);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao alterar Email! Erro: " + ex.Message);
                return false;
            }
        }

        // DELETE: api/Email/5
        [HttpDelete("{id}")]
        public async Task<Boolean> Excluir(int id)
        {
            try
            {
                return await new Email().Excluir(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao excluir Email! Erro: " + ex.Message);
                return false;
            }
        }
    }
}
