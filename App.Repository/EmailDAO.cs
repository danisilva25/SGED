using App.Repository;
using Microsoft.Data.SqlClient;
using SGED.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SGED.Repository
{
    public class EmailDAO : IGenericDAO<EmailDTO>
    {
        public async Task<bool> Salvar(EmailDTO obj)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                if (obj.idEmail == 0 && obj.escola != null)
                {
                    cmd.CommandText = "INSERT INTO email (descricaoEmail, escola) VALUES (@descricao, @escola)";
                    cmd.Parameters.AddWithValue("@escola", obj.escola.idEscola);
                }
                else if (obj.idEmail == 0 && obj.usuario != null)
                {
                    cmd.CommandText = "INSERT INTO email (descricaoEmail, usuario) VALUES (@descricao, @usuario)";
                    cmd.Parameters.AddWithValue("@usuario", obj.usuario.idUsuario);
                }
                else
                {
                    if (obj.usuario != null)
                    {
                        cmd.CommandText = @"UPDATE email 
                                        SET descricaoEmail = @descricao, usuario = @usuario, escola = NULL 
                                        WHERE idEmail = @id";
                        cmd.Parameters.AddWithValue("@usuario", obj.usuario.idUsuario);
                    }
                    else
                    {
                        cmd.CommandText = @"UPDATE email 
                                        SET descricaoEmail = @descricao, escola = @escola, usuario = NULL 
                                        WHERE idEmail = @id";
                        cmd.Parameters.AddWithValue("@escola", obj.escola.idEscola);
                    }
                    cmd.Parameters.AddWithValue("@id", obj.idEmail);
                }

                cmd.Parameters.AddWithValue("@descricao", obj.descricaoEmail);
                await conexao.ExecuteQuery(cmd);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao salvar Email! Erro: " + ex.Message);
                return false;
            }
        }

        public async Task<List<EmailDTO>> Listar(string busca)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                cmd.CommandText = @"SELECT e.*, es.nomeEscola, u.nomeUsuario 
                                FROM email e 
                                LEFT JOIN escola es ON es.idEscola = e.escola 
                                LEFT JOIN usuario u ON u.idUsuario = e.usuario";

                var lista = await conexao.ExecuteQueryToListAsync(cmd, reader =>
                {
                    var email = new EmailDTO
                    {
                        idEmail = Convert.ToInt32(reader["idEmail"]),
                        descricaoEmail = reader["descricaoEmail"].ToString()
                    };

                    if (reader["nomeEscola"] != DBNull.Value)
                        email.escola = new EscolaDTO(0, reader["nomeEscola"].ToString());

                    if (reader["nomeUsuario"] != DBNull.Value)
                        email.usuario = new UsuarioDTO(0, reader["nomeUsuario"].ToString());

                    return email;
                });

                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao listar Email! Erro: " + ex.Message);
                return null;
            }
        }

        public async Task<EmailDTO> Carregar(int idObject)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                cmd.CommandText = "SELECT * FROM email WHERE idEmail = @id";
                cmd.Parameters.AddWithValue("@id", idObject);

                var lista = await conexao.ExecuteQueryToListAsync(cmd, reader =>
                {
                    var email = new EmailDTO
                    {
                        idEmail = Convert.ToInt32(reader["idEmail"]),
                        descricaoEmail = reader["descricaoEmail"].ToString()
                    };

                    if (reader["escola"] != DBNull.Value)
                        email.escola = new EscolaDTO(Convert.ToInt32(reader["escola"]), null);

                    if (reader["usuario"] != DBNull.Value)
                        email.usuario = new UsuarioDTO(Convert.ToInt32(reader["usuario"]), null);

                    return email;
                });

                return lista.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao carregar Email! Erro: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> Excluir(int idObject)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                cmd.CommandText = "DELETE FROM email WHERE idEmail = @id";
                cmd.Parameters.AddWithValue("@id", idObject);

                await conexao.ExecuteQuery(cmd);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao excluir Email! Erro: " + ex.Message);
                return false;
            }
        }
    }
}
