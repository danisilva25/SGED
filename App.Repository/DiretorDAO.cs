using App.Repository;
using Microsoft.Data.SqlClient;
using SGED.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SGED.Repository
{
    public class DiretorDAO : IGenericDAO<DiretorDTO>
    {
        public async Task<bool> Salvar(DiretorDTO obj)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                if (obj.idUsuario == 0)
                {
                    int idUsuarioCriado = Convert.ToInt32(await new UsuarioDAO().GravarOutrosUsuarios(obj, 1));
                    cmd.CommandText = "INSERT INTO diretor (escola, usuario) VALUES (@escola, @usuario)";
                    cmd.Parameters.AddWithValue("@escola", obj.escola.idEscola);
                    cmd.Parameters.AddWithValue("@usuario", idUsuarioCriado);
                    await conexao.ExecuteQuery(cmd);
                }
                else if (string.IsNullOrEmpty(obj.senhaUsuario))
                {
                    await new UsuarioDAO().GravarOutrosUsuarios(obj, null);
                    cmd.CommandText = "UPDATE diretor SET escola = @escola WHERE usuario = @idUser";
                    cmd.Parameters.AddWithValue("@escola", obj.escola.idEscola);
                    cmd.Parameters.AddWithValue("@idUser", obj.idUsuario);
                    await conexao.ExecuteQuery(cmd);
                }
                else
                {
                    await new UsuarioDAO().GravarOutrosUsuarios(obj, null);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao salvar Diretor! Erro: " + ex.Message);
                return false;
            }
        }

        public async Task<List<DiretorDTO>> Listar(string busca)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                if (!string.IsNullOrEmpty(busca))
                {
                    cmd.CommandText = @"
                    SELECT u.nomeUsuario, u.idade, d.*, e.* 
                    FROM diretor d
                    INNER JOIN usuario u ON d.usuario = u.idUsuario
                    INNER JOIN escola e ON e.idEscola = d.escola
                    WHERE u.nomeUsuario LIKE @busca";
                    cmd.Parameters.AddWithValue("@busca", "%" + busca + "%");
                }
                else
                {
                    cmd.CommandText = @"
                    SELECT u.nomeUsuario, u.idade, d.*, e.* 
                    FROM diretor d
                    INNER JOIN usuario u ON d.usuario = u.idUsuario
                    INNER JOIN escola e ON e.idEscola = d.escola";
                }

                var lista = await conexao.ExecuteQueryToListAsync(cmd, reader => new DiretorDTO
                {
                    idUsuario = Convert.ToInt32(reader["usuario"]),
                    nomeUsuario = reader["nomeUsuario"].ToString(),
                    idade = Convert.ToInt16(reader["idade"]),
                    escola = new EscolaDTO(
                        Convert.ToInt32(reader["idEscola"]),
                        reader["nomeEscola"].ToString()
                    )
                });

                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao listar Diretor! Erro: " + ex.Message);
                return null;
            }
        }

        public async Task<DiretorDTO> Carregar(int idObject)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                cmd.CommandText = @"
                SELECT d.*, u.* 
                FROM diretor d
                INNER JOIN usuario u ON d.usuario = u.idUsuario
                WHERE d.usuario = @id";
                cmd.Parameters.AddWithValue("@id", idObject);

                var lista = await conexao.ExecuteQueryToListAsync(cmd, reader => new DiretorDTO
                {
                    idUsuario = Convert.ToInt32(reader["idUsuario"]),
                    nomeUsuario = reader["nomeUsuario"].ToString(),
                    idade = Convert.ToInt16(reader["idade"]),
                    loginUsuario = reader["loginUsuario"].ToString(),
                    escola = new EscolaDTO(
                        Convert.ToInt32(reader["escola"]),
                        null
                    )
                });

                return lista.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao carregar Diretor! Erro: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> Excluir(int idObject)
        {
            try
            {
                Conexao conexao = new();

                SqlCommand cmd = new();
                cmd.CommandText = "DELETE FROM diretor WHERE usuario = @id";
                cmd.Parameters.AddWithValue("@id", idObject);
                await conexao.ExecuteQuery(cmd);

                cmd = new SqlCommand(); // novo comando para nova query
                cmd.CommandText = "DELETE FROM usuario WHERE idUsuario = @id";
                cmd.Parameters.AddWithValue("@id", idObject);
                await conexao.ExecuteQuery(cmd);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao excluir Diretor! Erro: " + ex.Message);
                return false;
            }
        }
    }
}
