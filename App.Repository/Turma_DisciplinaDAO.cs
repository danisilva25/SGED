using App.Repository;
using Microsoft.Data.SqlClient;
using SGED.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SGED.Repository
{
    public class Turma_DisciplinaDAO : IGenericDAO<Turma_DisciplinaDTO>
    {
        public async Task<bool> Salvar(Turma_DisciplinaDTO obj)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                if (obj.idTurma_Disciplina == 0)
                {
                    cmd.CommandText = "INSERT INTO turma_disciplina(turma, disciplina) VALUES(@turma, @disciplina);";
                }
                else
                {
                    cmd.CommandText = "UPDATE turma_disciplina SET turma = @turma, disciplina = @disciplina WHERE idturma_disciplina = @id;";
                    cmd.Parameters.AddWithValue("@id", obj.idTurma_Disciplina);
                }

                cmd.Parameters.AddWithValue("@turma", obj.turma.idTurma);
                cmd.Parameters.AddWithValue("@disciplina", obj.disciplina.idDisciplina);

                await conexao.ExecuteQuery(cmd);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao salvar Turma_Disciplina! Erro: " + ex.Message);
                return false;
            }
        }

        public async Task<List<Turma_DisciplinaDTO>> Listar(string busca)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                // A busca não é usada neste caso
                cmd.CommandText = @"SELECT td.idTurma_disciplina, t.descricaoTurma, d.descricaoDisciplina 
                                FROM turma_disciplina td 
                                INNER JOIN turma t ON t.idTurma = td.turma 
                                INNER JOIN disciplina d ON d.idDisciplina = td.disciplina;";

                return await conexao.ExecuteQueryToListAsync(cmd, reader => new Turma_DisciplinaDTO
                {
                    idTurma_Disciplina = Convert.ToInt32(reader["idTurma_disciplina"]),
                    turma = new TurmaDTO(0, reader["descricaoTurma"].ToString()),
                    disciplina = new DisciplinaDTO(0, reader["descricaoDisciplina"].ToString())
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao listar Turma_Disciplina! Erro: " + ex.Message);
                return null;
            }
        }

        public async Task<Turma_DisciplinaDTO> Carregar(int idObject)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                cmd.CommandText = "SELECT * FROM turma_disciplina WHERE idturma_disciplina = @id;";
                cmd.Parameters.AddWithValue("@id", idObject);

                var lista = await conexao.ExecuteQueryToListAsync(cmd, reader => new Turma_DisciplinaDTO
                {
                    idTurma_Disciplina = Convert.ToInt32(reader["idturma_disciplina"]),
                    turma = new TurmaDTO(Convert.ToInt32(reader["turma"]), null),
                    disciplina = new DisciplinaDTO(Convert.ToInt32(reader["disciplina"]), null)
                });

                return lista.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao carregar Turma_Disciplina! Erro: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> Excluir(int idObject)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                cmd.CommandText = "DELETE FROM turma_disciplina WHERE idturma_disciplina = @id;";
                cmd.Parameters.AddWithValue("@id", idObject);

                await conexao.ExecuteQuery(cmd);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao excluir Turma_Disciplina! Erro: " + ex.Message);
                return false;
            }
        }
    }
}
