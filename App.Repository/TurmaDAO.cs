using App.Repository;
using Microsoft.Data.SqlClient;
using SGED.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SGED.Repository
{
    public class TurmaDAO : IGenericDAO<TurmaDTO>
    {
        public async Task<bool> Salvar(TurmaDTO obj)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                if (obj.idTurma == 0)
                    cmd.CommandText = "INSERT INTO turma(descricaoTurma) VALUES(@descricao);";
                else
                {
                    cmd.CommandText = "UPDATE turma SET descricaoTurma = @descricao WHERE idTurma = @id;";
                    cmd.Parameters.AddWithValue("@id", obj.idTurma);
                }

                cmd.Parameters.AddWithValue("@descricao", obj.descricaoTurma);

                await conexao.ExecuteQuery(cmd);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao salvar Turma! Erro: " + ex.Message);
                return false;
            }
        }

        public async Task<List<TurmaDTO>> Listar(string busca)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                if (string.IsNullOrEmpty(busca))
                    cmd.CommandText = "SELECT * FROM turma;";
                else
                {
                    cmd.CommandText = "SELECT * FROM turma WHERE descricaoTurma LIKE @busca;";
                    cmd.Parameters.AddWithValue("@busca", $"%{busca}%");
                }

                return await conexao.ExecuteQueryToListAsync(cmd, reader => new TurmaDTO
                {
                    idTurma = Convert.ToInt32(reader["idTurma"]),
                    descricaoTurma = reader["descricaoTurma"].ToString()
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao listar Turma! Erro: " + ex.Message);
                return null;
            }
        }

        public async Task<TurmaDTO> Carregar(int idObject)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new()
                {
                    CommandText = "SELECT * FROM turma WHERE idTurma = @id;"
                };
                cmd.Parameters.AddWithValue("@id", idObject);

                var lista = await conexao.ExecuteQueryToListAsync(cmd, reader => new TurmaDTO
                {
                    idTurma = Convert.ToInt32(reader["idTurma"]),
                    descricaoTurma = reader["descricaoTurma"].ToString()
                });

                return lista.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao carregar Turma! Erro: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> Excluir(int idObject)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new()
                {
                    CommandText = "DELETE FROM turma WHERE idTurma = @id"
                };
                cmd.Parameters.AddWithValue("@id", idObject);

                await conexao.ExecuteQuery(cmd);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao excluir Turma! Erro: " + ex.Message);
                return false;
            }
        }
    }
}
