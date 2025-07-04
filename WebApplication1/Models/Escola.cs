using SGED.Domain;
using SGED.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGED.Models
{
    public class Escola
    {
        public async Task<bool> Salvar(EscolaDTO escola)
        {
            try
            {
                EscolaDAO dao = new EscolaDAO();
                return await dao.Salvar(escola);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao salvar Escola! Erro: " + ex.Message);
            }
        }

        public async Task<List<EscolaDTO>> Listar(string busca)
        {
            try
            {
                EscolaDAO dao = new EscolaDAO();
                return await dao.Listar(busca);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao listar Escola! Erro: " + ex.Message);
            }
        }

        public async Task<EscolaDTO> Carregar(int id)
        {
            try
            {
                EscolaDAO dao = new EscolaDAO();
                return await dao.Carregar(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao carregar Escola! Erro: " + ex.Message);
            }
        }

        public async Task<bool> Excluir(int id)
        {
            try
            {
                EscolaDAO dao = new EscolaDAO();
                return await dao.Excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao excluir Escola! Erro: " + ex.Message);
            }
        }
    }
}