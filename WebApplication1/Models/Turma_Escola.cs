using SGED.Domain;
using SGED.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGED.Models
{
    public class Turma_Escola
    {
        public async Task<bool> Salvar(Turma_EscolaDTO turma_escola)
        {
            try
            {
                Turma_EscolaDAO dao = new Turma_EscolaDAO();
                return await dao.Salvar(turma_escola);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao salvar Turma_Escola! Erro: " + ex.Message);
            }
        }

        public async Task<List<Turma_EscolaDTO>> Listar(string busca)
        {
            try
            {
                Turma_EscolaDAO dao = new Turma_EscolaDAO();
                return await dao.Listar(busca);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao listar Turma_Escola! Erro: " + ex.Message);
            }
        }

        public async Task<Turma_EscolaDTO> Carregar(int id)
        {
            try
            {
                Turma_EscolaDAO dao = new Turma_EscolaDAO();
                return await dao.Carregar(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao carregar Turma_Escola! Erro: " + ex.Message);
            }
        }

        public async Task<bool> Excluir(int id)
        {
            try
            {
                Turma_EscolaDAO dao = new Turma_EscolaDAO();
                return await dao.Excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao excluir Turma_Escola! Erro: " + ex.Message);
            }
        }
    }
}