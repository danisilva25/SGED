using SGED.Domain;
using SGED.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGED.Models
{
    public class Turma
    {
        public async Task<bool> Salvar(TurmaDTO turma)
        {
            try
            {
                TurmaDAO dao = new TurmaDAO();
                return await dao.Salvar(turma);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao salvar Turma! Erro: " + ex.Message);
            }
        }

        public async Task<List<TurmaDTO>> Listar(string busca)
        {
            try
            {
                TurmaDAO dao = new TurmaDAO();
                return await dao.Listar(busca);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao listar Turma! Erro: " + ex.Message);
            }
        }

        public async Task<TurmaDTO> Carregar(int id)
        {
            try
            {
                TurmaDAO dao = new TurmaDAO();
                return await dao.Carregar(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao carregar Turma! Erro: " + ex.Message);
            }
        }

        public async Task<bool> Excluir(int id)
        {
            try
            {
                TurmaDAO dao = new TurmaDAO();
                return await dao.Excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao excluir Turma! Erro: " + ex.Message);
            }
        }
    }
}