using App.Repository;
using Microsoft.Data.SqlClient;
using SGED.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SGED.Repository
{
    public class EscolaDAO : IGenericDAO<EscolaDTO>
    {
        public async Task<bool> Salvar(EscolaDTO obj)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                if (obj.idEscola == 0)
                {
                    cmd.CommandText = @"INSERT INTO escola 
                    (nomeEscola, telefone, cep, diretoriaEnsino, idebAnosFinais, idebEnsinoMedio) 
                    VALUES (@nome, @telefone, @cep, @diretoria, @idebAnosFinais, @idebEnsinoMedio)";
                }
                else
                {
                    cmd.CommandText = @"UPDATE escola SET 
                    nomeEscola = @nome,
                    telefone = @telefone,
                    cep = @cep,
                    diretoriaEnsino = @diretoria,
                    idebAnosFinais = @idebAnosFinais,
                    idebEnsinoMedio = @idebEnsinoMedio 
                    WHERE idEscola = @id";
                    cmd.Parameters.AddWithValue("@id", obj.idEscola);
                }

                cmd.Parameters.AddWithValue("@nome", obj.nomeEscola);
                cmd.Parameters.AddWithValue("@telefone", obj.telefone);
                cmd.Parameters.AddWithValue("@cep", obj.cep);
                cmd.Parameters.AddWithValue("@diretoria", obj.diretoriaEnsino.idDiretoriaEnsino);
                cmd.Parameters.AddWithValue("@idebAnosFinais", obj.idebAnosFinais);
                cmd.Parameters.AddWithValue("@idebEnsinoMedio", obj.idebEnsinoMedio);

                await conexao.ExecuteQuery(cmd);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao salvar Escola! Erro: " + ex.Message);
                return false;
            }
        }

        public async Task<List<EscolaDTO>> Listar(string busca)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                if (string.IsNullOrEmpty(busca))
                {
                    cmd.CommandText = @"SELECT e.*, de.cep AS cepDiretoria, de.descricaoDiretoriaEnsino 
                    FROM escola e 
                    INNER JOIN DiretoriaEnsino de ON e.diretoriaEnsino = de.idDiretoriaEnsino";
                }
                else
                {
                    cmd.CommandText = @"SELECT e.*, de.cep AS cepDiretoria, de.descricaoDiretoriaEnsino 
                    FROM escola e 
                    INNER JOIN DiretoriaEnsino de ON e.diretoriaEnsino = de.idDiretoriaEnsino 
                    WHERE e.cep LIKE @busca";
                    cmd.Parameters.AddWithValue("@busca", $"%{busca}%");
                }

                var lista = await conexao.ExecuteQueryToListAsync(cmd, reader =>
                {
                    return new EscolaDTO
                    {
                        idEscola = Convert.ToInt32(reader["idEscola"]),
                        nomeEscola = reader["nomeEscola"].ToString(),
                        telefone = reader["telefone"].ToString(),
                        cep = reader["cep"].ToString(),
                        diretoriaEnsino = new DiretoriaEnsinoDTO(
                            Convert.ToInt32(reader["diretoriaEnsino"]),
                            reader["descricaoDiretoriaEnsino"].ToString(),
                            reader["cepDiretoria"].ToString()
                        ),
                        idebAnosFinais = Convert.ToDouble(reader["idebAnosFinais"]),
                        idebEnsinoMedio = Convert.ToDouble(reader["idebEnsinoMedio"])
                    };
                });

                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao listar Escola! Erro: " + ex.Message);
                return null;
            }
        }

        public async Task<EscolaDTO> Carregar(int idObject)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                cmd.CommandText = @"SELECT e.*, de.descricaoDiretoriaEnsino, de.cep AS cepDiretoria 
                FROM escola e 
                INNER JOIN DiretoriaEnsino de ON de.idDiretoriaEnsino = e.diretoriaEnsino 
                WHERE e.idEscola = @id";
                cmd.Parameters.AddWithValue("@id", idObject);

                var lista = await conexao.ExecuteQueryToListAsync(cmd, reader =>
                {
                    return new EscolaDTO
                    {
                        idEscola = Convert.ToInt32(reader["idEscola"]),
                        nomeEscola = reader["nomeEscola"].ToString(),
                        telefone = reader["telefone"].ToString(),
                        cep = reader["cep"].ToString(),
                        diretoriaEnsino = new DiretoriaEnsinoDTO(
                            Convert.ToInt32(reader["diretoriaEnsino"]),
                            reader["descricaoDiretoriaEnsino"].ToString(),
                            reader["cepDiretoria"].ToString()
                        ),
                        idebAnosFinais = Convert.ToDouble(reader["idebAnosFinais"]),
                        idebEnsinoMedio = Convert.ToDouble(reader["idebEnsinoMedio"])
                    };
                });

                return lista.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao carregar Escola! Erro: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> Excluir(int idObject)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                cmd.CommandText = "DELETE FROM escola WHERE idEscola = @id";
                cmd.Parameters.AddWithValue("@id", idObject);

                await conexao.ExecuteQuery(cmd);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao excluir Escola! Erro: " + ex.Message);
                return false;
            }
        }
    }
}
