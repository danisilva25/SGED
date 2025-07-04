using App.Repository;
using Microsoft.Data.SqlClient;
using SGED.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SGED.Repository
{
    public class DisciplinaDAO : IGenericDAO<DisciplinaDTO>
    {
        public async Task<bool> Salvar(DisciplinaDTO obj)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                if (obj.idDisciplina == 0)
                {
                    cmd.CommandText = "INSERT INTO disciplina (descricaoDisciplina) VALUES (@descricao)";
                }
                else
                {
                    cmd.CommandText = "UPDATE disciplina SET descricaoDisciplina = @descricao WHERE idDisciplina = @id";
                    cmd.Parameters.AddWithValue("@id", obj.idDisciplina);
                }

                cmd.Parameters.AddWithValue("@descricao", obj.descricaoDisciplina);

                await conexao.ExecuteQuery(cmd);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao salvar Disciplina! Erro: " + ex.Message);
                return false;
            }
        }

        public async Task<List<DisciplinaDTO>> Listar(string busca)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                if (string.IsNullOrEmpty(busca))
                {
                    cmd.CommandText = "SELECT * FROM disciplina";
                }
                else
                {
                    cmd.CommandText = "SELECT * FROM disciplina WHERE descricaoDisciplina LIKE @busca";
                    cmd.Parameters.AddWithValue("@busca", "%" + busca + "%");
                }

                var disciplinas = await conexao.ExecuteQueryToListAsync(cmd, reader => new DisciplinaDTO
                {
                    idDisciplina = Convert.ToInt32(reader["idDisciplina"]),
                    descricaoDisciplina = reader["descricaoDisciplina"].ToString()
                });

                return disciplinas;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao listar Disciplina! Erro: " + ex.Message);
                return null;
            }
        }

        public async Task<DisciplinaDTO> Carregar(int idObject)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                cmd.CommandText = "SELECT * FROM disciplina WHERE idDisciplina = @id";
                cmd.Parameters.AddWithValue("@id", idObject);

                var lista = await conexao.ExecuteQueryToListAsync(cmd, reader => new DisciplinaDTO
                {
                    idDisciplina = Convert.ToInt32(reader["idDisciplina"]),
                    descricaoDisciplina = reader["descricaoDisciplina"].ToString()
                });

                return lista.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao carregar Disciplina! Erro: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> Excluir(int idObject)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                cmd.CommandText = "DELETE FROM disciplina WHERE idDisciplina = @id";
                cmd.Parameters.AddWithValue("@id", idObject);

                await conexao.ExecuteQuery(cmd);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao excluir Disciplina! Erro: " + ex.Message);
                return false;
            }
        }
    }
}
