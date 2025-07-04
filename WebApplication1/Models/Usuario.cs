using SGED.Domain;
using SGED.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGED.Models
{
    public class Usuario
    {
        public async Task<Boolean> Salvar(UsuarioDTO usuario)
        {
            try
            {
                UsuarioDAO dao = new UsuarioDAO();
                return await dao.Salvar(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao salvar Usuário! Erro: " + ex.Message);
            }
        }

        public async Task<List<UsuarioDTO>> Listar(string busca)
        {
            try
            {
                UsuarioDAO dao = new UsuarioDAO();
                return await dao.Listar(busca);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao listar Usuário! Erro: " + ex.Message);
            }
        }

        public async Task<UsuarioDTO> Carregar(int id)
        {
            try
            {
                UsuarioDAO dao = new UsuarioDAO();
                return await dao.Carregar(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao carregar Usuário! Erro: " + ex.Message);
            }
        }

        public async Task<Boolean> Excluir(int id)
        {
            try
            {
                UsuarioDAO dao = new UsuarioDAO();
                return await dao.Excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao excluir Usuário! Erro: " + ex.Message);
            }
        }

        public async Task<UsuarioDTO> Logar(String login, String senha)
        {
            try
            {
                UsuarioDAO dao = new UsuarioDAO();
                return await dao.Logar(login, senha);
            }
            catch(Exception ex)
            {
                throw new Exception("Erro na Model ao logar! Erro: " + ex.Message);
            }
        }
    }
}