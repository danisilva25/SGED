using SGED.Domain;
using SGED.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGED.Models
{
    public class Turma_Disciplina
    {
        public async Task<bool> Salvar(Turma_DisciplinaDTO turma_disciplina)
        {
            try
            {
                Turma_DisciplinaDAO dao = new Turma_DisciplinaDAO();
                return await dao.Salvar(turma_disciplina);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao salvar Turma_Disciplina! Erro: " + ex.Message);
            }
        }

        public async Task<List<Turma_DisciplinaDTO>> Listar(string busca)
        {
            try
            {
                Turma_DisciplinaDAO dao = new Turma_DisciplinaDAO();
                return await dao.Listar(busca);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao listar Turma_Disciplina! Erro: " + ex.Message);
            }
        }

        public async Task<Turma_DisciplinaDTO> Carregar(int id)
        {
            try
            {
                Turma_DisciplinaDAO dao = new Turma_DisciplinaDAO();
                return await dao.Carregar(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao carregar Turma_Disciplina! Erro: " + ex.Message);
            }
        }

        public async Task<bool> Excluir(int id)
        {
            try
            {
                Turma_DisciplinaDAO dao = new Turma_DisciplinaDAO();
                return await dao.Excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao excluir Turma_Disciplina! Erro: " + ex.Message);
            }
        }
    }
}