using SGED.Domain;
using SGED.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGED.Models
{
    public class Aluno_Atividade
    {
        public async Task<bool> Salvar(Aluno_AtividadeDTO aluno_atividade)
        {
            try
            {
                var dao = new Aluno_AtividadeDAO();
                return await dao.Salvar(aluno_atividade);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao salvar Aluno_Atividade! Erro: " + ex.Message);
            }
        }

        public async Task<List<Aluno_AtividadeDTO>> Listar(string busca)
        {
            try
            {
                var dao = new Aluno_AtividadeDAO();
                return await dao.Listar(busca);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao listar Aluno_Atividade! Erro: " + ex.Message);
            }
        }

        public async Task<Aluno_AtividadeDTO> Carregar(int id)
        {
            try
            {
                var dao = new Aluno_AtividadeDAO();
                return await dao.Carregar(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao carregar Aluno_Atividade! Erro: " + ex.Message);
            }
        }

        public async Task<bool> Excluir(int id)
        {
            try
            {
                var dao = new Aluno_AtividadeDAO();
                return await dao.Excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao excluir Aluno_Atividade! Erro: " + ex.Message);
            }
        }
    }
}