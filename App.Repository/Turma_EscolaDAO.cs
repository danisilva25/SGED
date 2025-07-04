using App.Repository;
using Microsoft.Data.SqlClient;
using SGED.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SGED.Repository
{
    public class Turma_EscolaDAO : IGenericDAO<Turma_EscolaDTO>
    {
        public async Task<bool> Salvar(Turma_EscolaDTO obj)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();

                if (obj.idTurma_Escola == 0)
                    cmd.CommandText = "INSERT INTO turma_escola(turma, escola) VALUES(@turma, @escola);";
                else
                {
                    cmd.CommandText = "UPDATE turma_escola SET turma = @turma, escola = @escola WHERE idTurma_Escola = @id;";
                    cmd.Parameters.AddWithValue("@id", obj.idTurma_Escola);
                }

                cmd.Parameters.AddWithValue("@turma", obj.turma.idTurma);
                cmd.Parameters.AddWithValue("@escola", obj.escola.idEscola);

                await conexao.ExecuteQuery(cmd);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao salvar Turma_Escola: Erro: " + ex.Message);
                return false;
            }
        }

        public async Task<List<Turma_EscolaDTO>> Listar(string busca)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();
                cmd.CommandText = @"
                SELECT te.idTurma_Escola, t.descricaoTurma, e.nomeEscola 
                FROM turma_escola te 
                INNER JOIN turma t ON t.idTurma = te.turma 
                INNER JOIN escola e ON e.idEscola = te.escola;";

                return await conexao.ExecuteQueryToListAsync(cmd, reader => new Turma_EscolaDTO
                {
                    idTurma_Escola = Convert.ToInt32(reader["idTurma_Escola"]),
                    turma = new TurmaDTO(0, reader["descricaoTurma"].ToString()),
                    escola = new EscolaDTO(0, reader["nomeEscola"].ToString())
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao listar Turma_Escola! Erro: " + ex.Message);
                return null;
            }
        }

        public async Task<Turma_EscolaDTO> Carregar(int idObject)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();
                cmd.CommandText = "SELECT * FROM turma_escola WHERE idTurma_Escola = @id;";
                cmd.Parameters.AddWithValue("@id", idObject);

                var lista = await conexao.ExecuteQueryToListAsync(cmd, reader => new Turma_EscolaDTO
                {
                    idTurma_Escola = Convert.ToInt32(reader["idTurma_Escola"]),
                    turma = new TurmaDTO(Convert.ToInt32(reader["turma"]), null),
                    escola = new EscolaDTO(Convert.ToInt32(reader["escola"]), null)
                });

                return lista.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao carregar Turma_Escola! Erro: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> Excluir(int idObject)
        {
            try
            {
                Conexao conexao = new();
                SqlCommand cmd = new();
                cmd.CommandText = "DELETE FROM turma_escola WHERE idTurma_Escola = @id;";
                cmd.Parameters.AddWithValue("@id", idObject);

                await conexao.ExecuteQuery(cmd);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao excluir Turma_Escola! Erro: " + ex.Message);
                return false;
            }
        }
    }
}
