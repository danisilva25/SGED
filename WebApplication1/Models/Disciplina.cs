using SGED.Domain;
using SGED.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGED.Models
{
    public class Disciplina
    {
        public async Task<bool> Salvar(DisciplinaDTO disciplina)
        {
            try
            {
                DisciplinaDAO dao = new DisciplinaDAO();
                return await dao.Salvar(disciplina);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao salvar Disciplina! Erro: " + ex.Message);
            }
        }

        public async Task<List<DisciplinaDTO>> Listar(string buscar)
        {
            try
            {
                DisciplinaDAO dao = new DisciplinaDAO();
                return await dao.Listar(buscar);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao listar Disciplina! Erro: " + ex.Message);
            }
        }

        public async Task<DisciplinaDTO> Carregar(int id)
        {
            try
            {
                DisciplinaDAO dao = new DisciplinaDAO();
                return await dao.Carregar(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao carregar Disciplina! Erro: " + ex.Message);
            }
        }

        public async Task<bool> Excluir(int id)
        {
            try
            {
                DisciplinaDAO dao = new DisciplinaDAO();
                return await dao.Excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao excluir Disciplina! Erro: " + ex.Message);
            }
        }
    }
}