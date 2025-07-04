using SGED.Domain;
using SGED.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGED.Models
{
    public class Atividade
    {
        public async Task<bool> Salvar(AtividadeDTO atividade)
        {
            try
            {
                var dao = new AtividadeDAO();
                return await dao.Salvar(atividade);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao salvar Atividade! Erro: " + ex.Message);
            }
        }

        public async Task<List<AtividadeDTO>> Listar(string busca)
        {
            try
            {
                var dao = new AtividadeDAO();
                return await dao.Listar(busca);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao listar Atividade! Erro: " + ex.Message);
            }
        }

        public async Task<AtividadeDTO> Carregar(int id)
        {
            try
            {
                var dao = new AtividadeDAO();
                return await dao.Carregar(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao carregar Atividade! Erro: " + ex.Message);
            }
        }

        public async Task<bool> Excluir(int id)
        {
            try
            {
                var dao = new AtividadeDAO();
                return await dao.Excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao excluir Atividade! Erro: " + ex.Message);
            }
        }
    }
}