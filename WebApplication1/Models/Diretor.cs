using SGED.Domain;
using SGED.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGED.Models
{
    public class Diretor
    {
        public async Task<bool> Salvar(DiretorDTO diretor)
        {
            try
            {
                DiretorDAO dao = new DiretorDAO();
                return await dao.Salvar(diretor);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao salvar Diretor! Erro: " + ex.Message);
            }
        }

        public async Task<List<DiretorDTO>> Listar(string busca)
        {
            try
            {
                DiretorDAO dao = new DiretorDAO();
                return await dao.Listar(busca);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao listar Diretor! Erro: " + ex.Message);
            }
        }

        public async Task<DiretorDTO> Carregar(int id)
        {
            try
            {
                DiretorDAO dao = new DiretorDAO();
                return await dao.Carregar(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao carregar Diretor! Erro: " + ex.Message);
            }
        }

        public async Task<bool> Excluir(int id)
        {
            try
            {
                DiretorDAO dao = new DiretorDAO();
                return await dao.Excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao excluir Diretor! Erro: " + ex.Message);
            }
        }
    }
}