using SGED.Domain;
using SGED.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGED.Models
{
    public class Professor_Escola
    {
        public async Task<bool> Salvar(Professor_EscolaDTO professorEscola)
        {
            try
            {
                Professor_EscolaDAO dao = new Professor_EscolaDAO();
                return await dao.Salvar(professorEscola);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao salvar Professor_Escola! Erro: " + ex.Message);
            }
        }

        public async Task<List<Professor_EscolaDTO>> Listar(string busca)
        {
            try
            {
                Professor_EscolaDAO dao = new Professor_EscolaDAO();
                return await dao.Listar(busca);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao listar Professor_Escola! Erro: " + ex.Message);
            }
        }

        public async Task<Professor_EscolaDTO> Carregar(int id)
        {
            try
            {
                Professor_EscolaDAO dao = new Professor_EscolaDAO();
                return await dao.Carregar(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao carregar Professor_Escola! Erro: " + ex.Message);
            }
        }

        public async Task<bool> Excluir(int id)
        {
            try
            {
                Professor_EscolaDAO dao = new Professor_EscolaDAO();
                return await dao.Excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao excluir Professor_Escola! Erro: " + ex.Message);
            }
        }
    }
}