using SGED.Domain;
using SGED.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGED.Models
{
    public class DiretoriaEnsino
    {
        public async Task<bool> Salvar(DiretoriaEnsinoDTO diretoria)
        {
            try
            {
                DiretoriaEnsinoDAO dao = new DiretoriaEnsinoDAO();
                return await dao.Salvar(diretoria);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao salvar DiretoriaEnsino! Erro: " + ex.Message);
            }
        }

        public async Task<List<DiretoriaEnsinoDTO>> Listar(string busca)
        {
            try
            {
                DiretoriaEnsinoDAO dao = new DiretoriaEnsinoDAO();
                return await dao.Listar(busca);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao listar DiretoriaEnsino! Erro: " + ex.Message);
            }
        }

        public async Task<DiretoriaEnsinoDTO> Carregar(int id)
        {
            try
            {
                DiretoriaEnsinoDAO dao = new DiretoriaEnsinoDAO();
                return await dao.Carregar(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao carregar DiretoriaEnsino! Erro: " + ex.Message);
            }
        }

        public async Task<bool> Excluir(int id)
        {
            try
            {
                DiretoriaEnsinoDAO dao = new DiretoriaEnsinoDAO();
                return await dao.Excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao excluir DiretoriaEnsino! Erro: " + ex.Message);
            }
        }
    }
}