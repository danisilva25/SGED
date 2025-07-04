using App.Repository;
using Microsoft.Data.SqlClient;
using SGED.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SGED.Repository
{
    public class Professor_DisciplinaDAO : IGenericDAO<Professor_DisciplinaDTO>
    {
        public async Task<bool> Salvar(Professor_DisciplinaDTO obj)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                if (obj.idProfessor_Disciplina == 0)
                {
                    cmd.CommandText = @"INSERT INTO professor_disciplina(professor, disciplina) 
                                    VALUES(@professor, @disciplina)";
                }
                else
                {
                    cmd.CommandText = @"UPDATE professor_disciplina 
                                    SET professor = @professor, disciplina = @disciplina 
                                    WHERE idprofessor_disciplina = @id";
                    cmd.Parameters.AddWithValue("@id", obj.idProfessor_Disciplina);
                }

                cmd.Parameters.AddWithValue("@professor", obj.professor.idUsuario);
                cmd.Parameters.AddWithValue("@disciplina", obj.disciplina.idDisciplina);

                await conexao.ExecuteQuery(cmd);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao salvar Professor_Disciplina! Erro: " + ex.Message);
                return false;
            }
        }

        public async Task<List<Professor_DisciplinaDTO>> Listar(string busca)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                cmd.CommandText = @"SELECT pd.idprofessor_disciplina, u.nomeUsuario, d.descricaoDisciplina 
                                FROM professor p 
                                INNER JOIN usuario u ON u.idUsuario = p.usuario 
                                INNER JOIN professor_disciplina pd ON pd.professor = p.idprofessor 
                                INNER JOIN disciplina d ON d.idDisciplina = pd.disciplina";

                var lista = await conexao.ExecuteQueryToListAsync(cmd, reader => new Professor_DisciplinaDTO
                {
                    idProfessor_Disciplina = Convert.ToInt32(reader["idprofessor_disciplina"]),
                    professor = new ProfessorDTO(0, reader["nomeUsuario"].ToString()),
                    disciplina = new DisciplinaDTO(0, reader["descricaoDisciplina"].ToString())
                });

                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao listar Professor_Disciplina! Erro: " + ex.Message);
                return null;
            }
        }

        public async Task<Professor_DisciplinaDTO> Carregar(int idObject)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                cmd.CommandText = @"SELECT * FROM professor_disciplina 
                                WHERE idprofessor_disciplina = @id";
                cmd.Parameters.AddWithValue("@id", idObject);

                var lista = await conexao.ExecuteQueryToListAsync(cmd, reader => new Professor_DisciplinaDTO
                {
                    idProfessor_Disciplina = Convert.ToInt32(reader["idprofessor_disciplina"]),
                    professor = new ProfessorDTO(Convert.ToInt32(reader["professor"]), null),
                    disciplina = new DisciplinaDTO(Convert.ToInt32(reader["disciplina"]), null)
                });

                return lista.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao carregar Professor_Disciplina! Erro: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> Excluir(int idObject)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                cmd.CommandText = "DELETE FROM professor_disciplina WHERE idprofessor_disciplina = @id";
                cmd.Parameters.AddWithValue("@id", idObject);

                await conexao.ExecuteQuery(cmd);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao excluir Professor_Disciplina! Erro: " + ex.Message);
                return false;
            }
        }
    }

}
