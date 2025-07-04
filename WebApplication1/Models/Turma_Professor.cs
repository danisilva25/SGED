using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SGED.Domain;
using SGED.Repository;

namespace SGED.Models
{
    public class Turma_Professor
    {
        public async Task<bool> Salvar(Turma_ProfessorDTO turma_professor)
        {
            try
            {
                Turma_ProfessorDAO dao = new Turma_ProfessorDAO();
                return await dao.Salvar(turma_professor);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao salvar Turma_Professor! Erro: " + ex.Message);
            }
        }

        public async Task<List<Turma_ProfessorDTO>> Listar(string busca)
        {
            try
            {
                Turma_ProfessorDAO dao = new Turma_ProfessorDAO();
                return await dao.Listar(busca);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao listar Turma_Professor! Erro: " + ex.Message);
            }
        }

        public async Task<Turma_ProfessorDTO> Carregar(int id)
        {
            try
            {
                Turma_ProfessorDAO dao = new Turma_ProfessorDAO();
                return await dao.Carregar(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao carregar Turma_Professor! Erro: " + ex.Message);
            }
        }

        public async Task<bool> Excluir(int id)
        {
            try
            {
                Turma_ProfessorDAO dao = new Turma_ProfessorDAO();
                return await dao.Excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao excluir Turma_Professor! Erro: " + ex.Message);
            }
        }
    }
}