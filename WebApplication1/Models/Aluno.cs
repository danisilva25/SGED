using SGED.Domain;
using SGED.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGED.Models
{
    public class Aluno
    {
        public async Task<Boolean> Salvar(AlunoDTO aluno)
        {
            try
            {
                AlunoDAO dao = new AlunoDAO();
                return await dao.Salvar(aluno);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao salvar Aluno! Erro: " + ex.Message);
            }
        }
        public async Task<List<AlunoDTO>> Listar(string busca)
        {
            try
            {
                AlunoDAO dao = new AlunoDAO();
                return await dao.Listar(busca);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao listar Aluno! Erro: " + ex.Message);
            }
        }
        public async Task<AlunoDTO> Carregar(int id)
        {
            try
            {
                AlunoDAO dao = new AlunoDAO();
                return await dao.Carregar(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao carregar Aluno! Erro: " + ex.Message);
            }
        }
        public async Task<Boolean> Excluir(int id)
        {
            try
            {
                AlunoDAO dao = new AlunoDAO();
                return await dao.Excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao excluir Aluno! Erro: " + ex.Message);
            }
        }
    }
}