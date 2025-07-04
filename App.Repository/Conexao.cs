using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repository
{
    public class Conexao
    {
        private String StringConexao = @"Data Source=.\SQLExpress; Initial Catalog=sged; user id=sa; password=damuse10;TrustServerCertificate=True;";

        public async Task ExecuteQuery(SqlCommand SqlCommand)
        {
            SqlConnection conn = new SqlConnection(StringConexao);

            try
            {
                conn.Open();

                SqlCommand.Connection = conn;

                await SqlCommand.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                try
                {
                    conn.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro na DAO Aluno_Atividade ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }
        public async Task<List<T>> ExecuteQueryToListAsync<T>(SqlCommand sqlCommand, Func<SqlDataReader, T> mapFunction)
        {
            List<T> result = new List<T>();

            using (SqlConnection conn = new SqlConnection(StringConexao))
            {
                try
                {
                    await conn.OpenAsync();
                    sqlCommand.Connection = conn;

                    using (SqlDataReader reader = await sqlCommand.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(mapFunction(reader));
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao executar consulta: " + ex.Message, ex);
                }
            }

            return result;
        }
        public async Task<object> ExecuteScalarAsync(SqlCommand cmd)
        {
            await using var conn = new SqlConnection(StringConexao);

            try
            {
                await conn.OpenAsync().ConfigureAwait(false);
                cmd.Connection = conn;

                return await cmd.ExecuteScalarAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao executar ExecuteScalarAsync! Erro: " + ex.Message);
                throw;
            }
        }
    }
}
