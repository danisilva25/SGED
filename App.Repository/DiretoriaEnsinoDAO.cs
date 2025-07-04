using App.Repository;
using Microsoft.Data.SqlClient;
using SGED.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SGED.Repository
{
    public class DiretoriaEnsinoDAO : IGenericDAO<DiretoriaEnsinoDTO>
    {
        public async Task<bool> Salvar(DiretoriaEnsinoDTO obj)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                if (obj.idDiretoriaEnsino == 0)
                {
                    cmd.CommandText = @"INSERT INTO diretoriaEnsino (descricaoDiretoriaEnsino, cep) 
                                    VALUES (@descricao, @cep)";
                }
                else
                {
                    cmd.CommandText = @"UPDATE diretoriaEnsino 
                                    SET descricaoDiretoriaEnsino = @descricao, cep = @cep 
                                    WHERE idDiretoriaEnsino = @id";
                    cmd.Parameters.AddWithValue("@id", obj.idDiretoriaEnsino);
                }

                cmd.Parameters.AddWithValue("@descricao", obj.descricaoDiretoriaEnsino);
                cmd.Parameters.AddWithValue("@cep", obj.cep);

                await conexao.ExecuteQuery(cmd);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao salvar DiretoriaEnsino! Erro: " + ex.Message);
                return false;
            }
        }

        public async Task<List<DiretoriaEnsinoDTO>> Listar(string busca)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                if (string.IsNullOrEmpty(busca))
                {
                    cmd.CommandText = "SELECT * FROM diretoriaEnsino";
                }
                else
                {
                    cmd.CommandText = "SELECT * FROM diretoriaEnsino WHERE cep LIKE @busca";
                    cmd.Parameters.AddWithValue("@busca", "%" + busca + "%");
                }

                var lista = await conexao.ExecuteQueryToListAsync(cmd, reader => new DiretoriaEnsinoDTO
                {
                    idDiretoriaEnsino = Convert.ToInt32(reader["idDiretoriaEnsino"]),
                    descricaoDiretoriaEnsino = reader["descricaoDiretoriaEnsino"].ToString(),
                    cep = reader["cep"].ToString()
                });

                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao listar Diretoria de Ensino! Erro: " + ex.Message);
                return null;
            }
        }

        public async Task<DiretoriaEnsinoDTO> Carregar(int idObject)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                cmd.CommandText = "SELECT * FROM diretoriaEnsino WHERE idDiretoriaEnsino = @id";
                cmd.Parameters.AddWithValue("@id", idObject);

                var lista = await conexao.ExecuteQueryToListAsync(cmd, reader => new DiretoriaEnsinoDTO
                {
                    idDiretoriaEnsino = Convert.ToInt32(reader["idDiretoriaEnsino"]),
                    descricaoDiretoriaEnsino = reader["descricaoDiretoriaEnsino"].ToString(),
                    cep = reader["cep"].ToString()
                });

                return lista.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao carregar DiretoriaEnsino! Erro: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> Excluir(int idObject)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                cmd.CommandText = "DELETE FROM diretoriaEnsino WHERE idDiretoriaEnsino = @id";
                cmd.Parameters.AddWithValue("@id", idObject);

                await conexao.ExecuteQuery(cmd);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao excluir DiretoriaEnsino! Erro: " + ex.Message);
                return false;
            }
        }
    }
}
