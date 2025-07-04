using App.Repository;
using Microsoft.Data.SqlClient;
using SGED.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SGED.Repository
{
    public class Turma_ProfessorDAO : IGenericDAO<Turma_ProfessorDTO>
    {
        public async Task<bool> Salvar(Turma_ProfessorDTO obj)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                if (obj.idTurma_Professor == 0)
                    cmd.CommandText = "INSERT INTO turma_professor(turma, professor) VALUES(@turma, @professor);";
                else
                {
                    cmd.CommandText = "UPDATE turma_professor SET turma = @turma, professor = @professor WHERE idTurma_professor = @id;";
                    cmd.Parameters.AddWithValue("@id", obj.idTurma_Professor);
                }

                cmd.Parameters.AddWithValue("@turma", obj.turma.idTurma);
                cmd.Parameters.AddWithValue("@professor", obj.professor.idUsuario);

                await conexao.ExecuteQuery(cmd);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao salvar Turma_Professor! Erro: " + ex.Message);
                return false;
            }
        }

        public async Task<List<Turma_ProfessorDTO>> Listar(string busca)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();
                cmd.CommandText = @"
                SELECT tp.idturma_professor, u.nomeUsuario, t.descricaoTurma
                FROM professor p
                INNER JOIN usuario u ON p.usuario = u.idUsuario
                INNER JOIN turma_professor tp ON tp.professor = p.idProfessor
                INNER JOIN turma t ON t.idTurma = tp.turma;";

                return await conexao.ExecuteQueryToListAsync(cmd, reader => new Turma_ProfessorDTO
                {
                    idTurma_Professor = Convert.ToInt32(reader["idturma_professor"]),
                    professor = new ProfessorDTO(0, reader["nomeUsuario"].ToString()),
                    turma = new TurmaDTO(0, reader["descricaoTurma"].ToString())
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao listar Turma_Professor! Erro: " + ex.Message);
                return null;
            }
        }

        public async Task<Turma_ProfessorDTO> Carregar(int idObject)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();
                cmd.CommandText = "SELECT * FROM turma_professor WHERE idturma_professor = @id;";
                cmd.Parameters.AddWithValue("@id", idObject);

                var lista = await conexao.ExecuteQueryToListAsync(cmd, reader => new Turma_ProfessorDTO
                {
                    idTurma_Professor = Convert.ToInt32(reader["idturma_professor"]),
                    turma = new TurmaDTO(Convert.ToInt32(reader["turma"]), null),
                    professor = new ProfessorDTO(Convert.ToInt32(reader["professor"]), null)
                });

                return lista.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao carregar Turma_Professor! Erro: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> Excluir(int idObject)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();
                cmd.CommandText = "DELETE FROM turma_professor WHERE idTurma_professor = @id;";
                cmd.Parameters.AddWithValue("@id", idObject);

                await conexao.ExecuteQuery(cmd);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao excluir Turma_Professor! Erro: " + ex.Message);
                return false;
            }
        }
    }
}
