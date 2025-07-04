using App.Repository;
using Microsoft.Data.SqlClient;
using SGED.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SGED.Repository
{
    public class Professor_EscolaDAO : IGenericDAO<Professor_EscolaDTO>
    {
        public async Task<bool> Salvar(Professor_EscolaDTO obj)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                if (obj.idProfessor_Escola == 0)
                {
                    cmd.CommandText = @"INSERT INTO professor_escola(professor, escola)
                                    VALUES(@professor, @escola);";
                }
                else
                {
                    cmd.CommandText = @"UPDATE professor_escola 
                                    SET professor = @professor, escola = @escola 
                                    WHERE idProfessor_escola = @id;";
                    cmd.Parameters.AddWithValue("@id", obj.idProfessor_Escola);
                }

                cmd.Parameters.AddWithValue("@professor", obj.professor.idUsuario);
                cmd.Parameters.AddWithValue("@escola", obj.escola.idEscola);

                await conexao.ExecuteQuery(cmd);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao salvar Professor_Escola! Erro: " + ex.Message);
                return false;
            }
        }

        public async Task<List<Professor_EscolaDTO>> Listar(string busca)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                cmd.CommandText = @"SELECT pe.idprofessor_escola, u.nomeUsuario, e.nomeEscola 
                                FROM professor p
                                INNER JOIN professor_escola pe ON pe.professor = p.idProfessor 
                                INNER JOIN usuario u ON u.idUsuario = p.usuario 
                                INNER JOIN escola e ON e.idEscola = pe.escola;";

                return await conexao.ExecuteQueryToListAsync(cmd, reader => new Professor_EscolaDTO
                {
                    idProfessor_Escola = Convert.ToInt32(reader["idprofessor_escola"]),
                    professor = new ProfessorDTO(0, reader["nomeUsuario"].ToString()),
                    escola = new EscolaDTO(0, reader["nomeEscola"].ToString())
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao listar Professor_Escola! Erro: " + ex.Message);
                return null;
            }
        }

        public async Task<Professor_EscolaDTO> Carregar(int idObject)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                cmd.CommandText = @"SELECT pe.idprofessor_escola, e.idEscola, p.idProfessor 
                                FROM professor p 
                                INNER JOIN professor_escola pe ON pe.professor = p.idProfessor 
                                INNER JOIN escola e ON e.idEscola = pe.escola 
                                WHERE idprofessor_escola = @id;";
                cmd.Parameters.AddWithValue("@id", idObject);

                var lista = await conexao.ExecuteQueryToListAsync(cmd, reader => new Professor_EscolaDTO
                {
                    idProfessor_Escola = Convert.ToInt32(reader["idprofessor_escola"]),
                    professor = new ProfessorDTO(Convert.ToInt32(reader["idProfessor"]), null),
                    escola = new EscolaDTO(Convert.ToInt32(reader["idEscola"]), null)
                });

                return lista.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao carregar Professor_Escola! Erro: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> Excluir(int idObject)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                cmd.CommandText = @"DELETE FROM professor_escola WHERE idprofessor_escola = @id;";
                cmd.Parameters.AddWithValue("@id", idObject);

                await conexao.ExecuteQuery(cmd);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao excluir Professor_Escola! Erro: " + ex.Message);
                return false;
            }
        }
    }
}
