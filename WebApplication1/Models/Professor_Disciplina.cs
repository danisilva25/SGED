using SGED.Domain;
using SGED.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGED.Models
{
    public class Professor_Disciplina
    {
        public async Task<bool> Salvar(Professor_DisciplinaDTO professorDisciplina)
        {
            try
            {
                Professor_DisciplinaDAO dao = new Professor_DisciplinaDAO();
                return await dao.Salvar(professorDisciplina);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao salvar Professor_Disciplina! Erro: " + ex.Message);
            }
        }

        public async Task<List<Professor_DisciplinaDTO>> Listar(string busca)
        {
            try
            {
                Professor_DisciplinaDAO dao = new Professor_DisciplinaDAO();
                return await dao.Listar(busca);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao listar Professor_Disciplina! Erro: " + ex.Message);
            }
        }

        public async Task<Professor_DisciplinaDTO> Carregar(int id)
        {
            try
            {
                Professor_DisciplinaDAO dao = new Professor_DisciplinaDAO();
                return await dao.Carregar(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao carregar Professor_Disciplina! Erro: " + ex.Message);
            }
        }

        public async Task<bool> Excluir(int id)
        {
            try
            {
                Professor_DisciplinaDAO dao = new Professor_DisciplinaDAO();
                return await dao.Excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao excluir Professor_Disciplina! Erro: " + ex.Message);
            }
        }
    }
}