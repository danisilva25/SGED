using App.Repository;
using Microsoft.Data.SqlClient;
using SGED.Domain;
using System.Formats.Asn1;

namespace SGED.Repository
{
    public class Aluno_AtividadeDAO : IGenericDAO<Aluno_AtividadeDTO>
    {
        public async Task<bool> Salvar(Aluno_AtividadeDTO obj)
        {
            try
            {
                Conexao Conexao = new();
                SqlCommand cmd = new SqlCommand();

                if (obj.idAluno_Disciplina == 0)
                    cmd.CommandText = "insert into aluno_atividade(nota, aluno, atividade) values (@nota, @aluno, @atividade);";
                else
                {
                    cmd.CommandText = "update aluno_atividade set nota = @nota, aluno = @aluno, atividade = @atividade where idaluno_atividade = @id";
                    cmd.Parameters.AddWithValue("@id", obj.idAluno_Disciplina);
                }
                cmd.Parameters.AddWithValue("@nota", obj.nota);
                cmd.Parameters.AddWithValue("@aluno", obj.aluno.idUsuario);
                cmd.Parameters.AddWithValue("@atividade", obj.atividade.idAtividade);

                await Conexao.ExecuteQuery(cmd);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao salvar Aluno_Atividade! Erro: " + ex.Message);
                return false;
            }
        }

        public async Task<List<Aluno_AtividadeDTO>> Listar(string busca)
        {
            Conexao Conexao = new();
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandText = "select aat.idAluno_Atividade, u.nomeUsuario, at.descricaoAtividade, aat.nota from aluno a inner join " + 
                "usuario u on u.idUsuario = a.usuario inner join aluno_atividade aat on aat.aluno = a.idAluno " + 
                "inner join atividade at on aat.atividade = at.idatividade";

                List<Aluno_AtividadeDTO> ListaAluno_Atividade = await Conexao.ExecuteQueryToListAsync(cmd, reader => new Aluno_AtividadeDTO
                {
                    idAluno_Disciplina = reader.GetInt32(reader.GetOrdinal("idAluno_atividade")),
                    nota = reader.GetInt16(reader.GetOrdinal("nota")),
                    aluno = new AlunoDTO(0, reader.GetString(reader.GetOrdinal("nomeUsuario"))),
                    atividade = new AtividadeDTO(0, reader.GetString(reader.GetOrdinal("descricaoAtividade"))),
                });

                return ListaAluno_Atividade;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao listar Aluno_Atividade! Erro: " + ex.Message);
                return null;
            }
        }

        public async Task<Aluno_AtividadeDTO> Carregar(int idObject)
        {
            Conexao Conexao = new();
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandText = "select * from aluno_atividade where idAluno_atividade = @id";

                List<Aluno_AtividadeDTO> ListaAluno_Atividade = await Conexao.ExecuteQueryToListAsync(cmd, reader => new Aluno_AtividadeDTO
                {
                    idAluno_Disciplina = reader.GetInt32(reader.GetOrdinal("idAluno_atividade")),
                    nota = reader.GetInt16(reader.GetOrdinal("nota")),
                    aluno = new AlunoDTO(0, reader.GetString(reader.GetOrdinal("nomeUsuario"))),
                    atividade = new AtividadeDTO(0, reader.GetString(reader.GetOrdinal("descricaoAtividade"))),
                });

                if (ListaAluno_Atividade.Count > 0)
                {
                    return ListaAluno_Atividade.First();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao listar Aluno_Atividade! Erro: " + ex.Message);
                return null;
            }
        }

        public async Task<Boolean> Excluir(int idObject)
        {
            try
            {
                Conexao Conexao = new();
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "delete from aluno_atividade where idaluno_atividade = @id";
                cmd.Parameters.AddWithValue("@id", idObject);

                await Conexao.ExecuteQuery(cmd);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao salvar Aluno_Atividade! Erro: " + ex.Message);
                return false;
            }
        }
    }
}
