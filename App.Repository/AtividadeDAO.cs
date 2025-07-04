using App.Repository;
using Microsoft.Data.SqlClient;
using SGED.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SGED.Repository
{
    public class AtividadeDAO : IGenericDAO<AtividadeDTO>
    {
        public async Task<bool> Salvar(AtividadeDTO obj)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                if (obj.idAtividade == 0)
                {
                    if (obj.dataFinalAtividade != null)
                    {
                        cmd.CommandText = @"INSERT INTO atividade (descricaoAtividade, dataInicioAtividade, dataFinalAtividade, disciplina)
                                        VALUES (@descricao, @dataInicial, @dataFinal, @disciplina)";
                        cmd.Parameters.AddWithValue("@dataFinal", obj.dataFinalAtividade);
                    }
                    else
                    {
                        cmd.CommandText = @"INSERT INTO atividade (descricaoAtividade, dataInicioAtividade, disciplina)
                                        VALUES (@descricao, @dataInicial, @disciplina)";
                    }
                }
                else
                {
                    if (obj.dataFinalAtividade != null)
                    {
                        cmd.CommandText = @"UPDATE atividade 
                                        SET descricaoAtividade = @descricao, dataInicioAtividade = @dataInicial,
                                            dataFinalAtividade = @dataFinal, disciplina = @disciplina 
                                        WHERE idAtividade = @id";
                        cmd.Parameters.AddWithValue("@dataFinal", obj.dataFinalAtividade);
                    }
                    else
                    {
                        cmd.CommandText = @"UPDATE atividade 
                                        SET descricaoAtividade = @descricao, dataInicioAtividade = @dataInicial,
                                            dataFinalAtividade = NULL, disciplina = @disciplina 
                                        WHERE idAtividade = @id";
                    }

                    cmd.Parameters.AddWithValue("@id", obj.idAtividade);
                }

                cmd.Parameters.AddWithValue("@descricao", obj.descricaoAtividade);
                cmd.Parameters.AddWithValue("@dataInicial", obj.dataInicioAtividade);
                cmd.Parameters.AddWithValue("@disciplina", obj.disciplina.idDisciplina);

                await conexao.ExecuteQuery(cmd);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao salvar Atividade! Erro: " + ex.Message);
                return false;
            }
        }

        public async Task<List<AtividadeDTO>> Listar(string busca)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                cmd.CommandText = @"
                SELECT 
                    a.idAtividade,
                    a.descricaoAtividade,
                    a.dataInicioAtividade,
                    a.dataFinalAtividade,
                    d.idDisciplina,
                    d.descricaoDisciplina
                FROM atividade a
                INNER JOIN disciplina d ON d.idDisciplina = a.disciplina";

                var lista = await conexao.ExecuteQueryToListAsync(cmd, reader => new AtividadeDTO
                {
                    idAtividade = reader.GetInt32(reader.GetOrdinal("idAtividade")),
                    descricaoAtividade = reader["descricaoAtividade"].ToString(),
                    dataInicioAtividade = Convert.ToDateTime(reader["dataInicioAtividade"]),
                    dataFinalAtividade = reader["dataFinalAtividade"] != DBNull.Value
                                         ? Convert.ToDateTime(reader["dataFinalAtividade"])
                                         : null,
                    disciplina = new DisciplinaDTO(
                        reader.GetInt32(reader.GetOrdinal("idDisciplina")),
                        reader["descricaoDisciplina"].ToString()
                    )
                });

                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao listar Atividade! Erro: " + ex.Message);
                return null;
            }
        }

        public async Task<AtividadeDTO> Carregar(int idObject)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                cmd.CommandText = @"
                SELECT 
                    a.idAtividade,
                    a.descricaoAtividade,
                    a.dataInicioAtividade,
                    a.dataFinalAtividade,
                    d.idDisciplina,
                    d.descricaoDisciplina
                FROM atividade a
                INNER JOIN disciplina d ON d.idDisciplina = a.disciplina
                WHERE a.idAtividade = @id";

                cmd.Parameters.AddWithValue("@id", idObject);

                var lista = await conexao.ExecuteQueryToListAsync(cmd, reader => new AtividadeDTO
                {
                    idAtividade = reader.GetInt32(reader.GetOrdinal("idAtividade")),
                    descricaoAtividade = reader["descricaoAtividade"].ToString(),
                    dataInicioAtividade = Convert.ToDateTime(reader["dataInicioAtividade"]),
                    dataFinalAtividade = reader["dataFinalAtividade"] != DBNull.Value
                                         ? Convert.ToDateTime(reader["dataFinalAtividade"])
                                         : null,
                    disciplina = new DisciplinaDTO(
                        reader.GetInt32(reader.GetOrdinal("idDisciplina")),
                        reader["descricaoDisciplina"].ToString()
                    )
                });

                return lista.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao carregar Atividade! Erro: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> Excluir(int idObject)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                cmd.CommandText = "DELETE FROM atividade WHERE idAtividade = @id";
                cmd.Parameters.AddWithValue("@id", idObject);

                await conexao.ExecuteQuery(cmd);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao excluir Atividade! Erro: " + ex.Message);
                return false;
            }
        }
    }
}
