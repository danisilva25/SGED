using App.Repository;
using Microsoft.Data.SqlClient;
using SGED.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SGED.Repository
{
    public class ProfessorDAO : IGenericDAO<ProfessorDTO>
    {
        public async Task<bool> Salvar(ProfessorDTO obj)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                if (obj.idUsuario == 0)
                {
                    obj.idUsuario = Convert.ToInt32(await new UsuarioDAO().GravarOutrosUsuarios(obj, 2)); // cria usuário e retorna id

                    cmd.CommandText = @"INSERT INTO professor(cpf, rg, matricula, categoria, horaaula, disponivel, diretoriaEnsino, usuario)
                                    VALUES (@cpf, @rg, @matricula, @categoria, @hora, @disponivel, @diretoria, @usuario);";
                    cmd.Parameters.AddWithValue("@usuario", obj.idUsuario);
                }
                else if (string.IsNullOrEmpty(obj.senhaUsuario))
                {
                    await new UsuarioDAO().GravarOutrosUsuarios(obj, null);

                    cmd.CommandText = @"UPDATE professor 
                                    SET cpf = @cpf, rg = @rg, matricula = @matricula, categoria = @categoria, 
                                        horaaula = @hora, disponivel = @disponivel, diretoriaEnsino = @diretoria 
                                    WHERE usuario = @id;";
                    cmd.Parameters.AddWithValue("@id", obj.idUsuario);
                }
                else
                {
                    await new UsuarioDAO().GravarOutrosUsuarios(obj, null);
                    return true;
                }

                cmd.Parameters.AddWithValue("@cpf", obj.cpf);
                cmd.Parameters.AddWithValue("@rg", obj.rg);
                cmd.Parameters.AddWithValue("@matricula", obj.matricula);
                cmd.Parameters.AddWithValue("@categoria", obj.categoria);
                cmd.Parameters.AddWithValue("@hora", obj.horaAula);
                cmd.Parameters.AddWithValue("@disponivel", obj.disponivel);
                cmd.Parameters.AddWithValue("@diretoria", obj.diretoriaEnsino.idDiretoriaEnsino);

                await conexao.ExecuteQuery(cmd);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao salvar Professor! Erro: " + ex.Message);
                return false;
            }
        }

        public async Task<List<ProfessorDTO>> Listar(string busca)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                if (string.IsNullOrEmpty(busca))
                {
                    cmd.CommandText = @"SELECT u.idusuario, u.nomeUsuario, d.descricaoDiretoriaEnsino, 
                                           p.idprofessor, p.categoria, p.horaaula
                                    FROM professor p
                                    INNER JOIN usuario u ON u.idUsuario = p.usuario
                                    INNER JOIN diretoriaEnsino d ON d.idDiretoriaEnsino = p.diretoriaEnsino;";
                }
                else
                {
                    cmd.CommandText = @"SELECT u.idusuario, u.nomeUsuario, d.descricaoDiretoriaEnsino, 
                                           p.idprofessor, p.categoria, p.horaaula
                                    FROM professor p
                                    INNER JOIN usuario u ON u.idUsuario = p.usuario
                                    INNER JOIN diretoriaEnsino d ON d.idDiretoriaEnsino = p.diretoriaEnsino
                                    WHERE u.nomeUsuario LIKE @busca;";
                    cmd.Parameters.AddWithValue("@busca", $"%{busca}%");
                }

                return await conexao.ExecuteQueryToListAsync(cmd, reader => new ProfessorDTO
                {
                    idUsuario = Convert.ToInt32(reader["idusuario"]),
                    nomeUsuario = reader["nomeUsuario"].ToString(),
                    categoria = Convert.ToChar(reader["categoria"].ToString()),
                    horaAula = Convert.ToInt16(reader["horaaula"].ToString()),
                    diretoriaEnsino = new DiretoriaEnsinoDTO(0, reader["descricaoDiretoriaEnsino"].ToString(), null)
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao listar Professor! Erro: " + ex.Message);
                return null;
            }
        }

        public async Task<ProfessorDTO> Carregar(int idObject)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                cmd.CommandText = @"SELECT p.*, d.*, u.* 
                                FROM professor p 
                                INNER JOIN diretoriaEnsino d ON d.idDiretoriaEnsino = p.diretoriaEnsino
                                INNER JOIN usuario u ON u.idusuario = p.usuario 
                                WHERE u.idUsuario = @id;";
                cmd.Parameters.AddWithValue("@id", idObject);

                var lista = await conexao.ExecuteQueryToListAsync(cmd, reader => new ProfessorDTO
                {
                    idUsuario = Convert.ToInt32(reader["idusuario"]),
                    nomeUsuario = reader["nomeUsuario"].ToString(),
                    idade = Convert.ToInt16(reader["idade"]),
                    loginUsuario = reader["loginUsuario"].ToString(),
                    matricula = reader["matricula"].ToString(),
                    rg = reader["rg"].ToString(),
                    cpf = reader["cpf"].ToString(),
                    categoria = Convert.ToChar(reader["categoria"]),
                    horaAula = Convert.ToInt16(reader["horaAula"]),
                    disponivel = Convert.ToBoolean(reader["disponivel"]),
                    diretoriaEnsino = new DiretoriaEnsinoDTO(
                        Convert.ToInt32(reader["diretoriaEnsino"]),
                        reader["descricaoDiretoriaEnsino"].ToString(),
                        null
                    )
                });

                return lista.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao carregar Professor! Erro: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> Excluir(int idObject)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                cmd.CommandText = "DELETE FROM professor WHERE usuario = @id;";
                cmd.Parameters.AddWithValue("@id", idObject);
                await conexao.ExecuteQuery(cmd);

                cmd.Parameters.Clear();
                cmd.CommandText = "DELETE FROM usuario WHERE idUsuario = @id;";
                cmd.Parameters.AddWithValue("@id", idObject);
                await conexao.ExecuteQuery(cmd);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao excluir Professor! Erro: " + ex.Message);
                return false;
            }
        }
    }
}
