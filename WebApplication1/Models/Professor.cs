using SGED.Domain;
using SGED.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGED.Models
{
    public class Professor
    {
        public async Task<bool> Salvar(ProfessorDTO professor)
        {
            try
            {
                ProfessorDAO dao = new ProfessorDAO();
                return await dao.Salvar(professor);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao salvar Professor! Erro: " + ex.Message);
            }
        }

        public async Task<List<ProfessorDTO>> Listar(string busca)
        {
            try
            {
                ProfessorDAO dao = new ProfessorDAO();
                return await dao.Listar(busca);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao listar Professor! Erro: " + ex.Message);
            }
        }

        public async Task<ProfessorDTO> Carregar(int id)
        {
            try
            {
                ProfessorDAO dao = new ProfessorDAO();
                return await dao.Carregar(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao carregar Professor! Erro: " + ex.Message);
            }
        }

        public async Task<bool> Excluir(int id)
        {
            try
            {
                ProfessorDAO dao = new ProfessorDAO();
                return await dao.Excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao excluir Professor! Erro: " + ex.Message);
            }
        }
    }
}