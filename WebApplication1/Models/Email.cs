using SGED.Domain;
using SGED.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGED.Models
{
    public class Email
    {
        public async Task<bool> Salvar(EmailDTO email)
        {
            try
            {
                EmailDAO dao = new EmailDAO();
                return await dao.Salvar(email);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao salvar Email! Erro: " + ex.Message);
            }
        }

        public async Task<List<EmailDTO>> Listar(string busca)
        {
            try
            {
                EmailDAO dao = new EmailDAO();
                return await dao.Listar(busca);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao listar Email! Erro: " + ex.Message);
            }
        }

        public async Task<EmailDTO> Carregar(int id)
        {
            try
            {
                EmailDAO dao = new EmailDAO();
                return await dao.Carregar(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao carregar Email! Erro: " + ex.Message);
            }
        }

        public async Task<bool> Excluir(int id)
        {
            try
            {
                EmailDAO dao = new EmailDAO();
                return await dao.Excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao excluir Email! Erro: " + ex.Message);
            }
        }
    }

}