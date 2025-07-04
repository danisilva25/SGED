using App.Repository;
using Microsoft.Data.SqlClient;
using SGED.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SGED.Repository
{
    public class AlunoDAO : IGenericDAO<AlunoDTO>
    {
        public async Task<bool> Salvar(AlunoDTO obj)
        {
            try
            {
                Conexao Conexao = new();
                SqlCommand cmd = new SqlCommand();

                cmd.Parameters.AddWithValue("@ra", obj.ra);
                cmd.Parameters.AddWithValue("@turma", obj.turma.idTurma);
                cmd.Parameters.AddWithValue("@escola", obj.escola.idEscola);

                if (obj.idUsuario == 0)
                {
                    cmd.CommandText = "insert into aluno(ra, turma, escola, usuario) values (@ra, @turma, " +
                    "@escola, @usuario)";

                    cmd.Parameters.AddWithValue("@usuario", await new UsuarioDAO().GravarOutrosUsuarios(obj, 3));
                    await Conexao.ExecuteQuery(cmd);
                }
                else if (string.IsNullOrEmpty(obj.senhaUsuario))
                {
                    await new UsuarioDAO().GravarOutrosUsuarios(obj, null);
                    cmd.CommandText = "update aluno set ra = @ra, turma = @turma, escola = @escola where usuario = @id";

                    cmd.Parameters.AddWithValue("@id", obj.idUsuario);
                    await Conexao.ExecuteQuery(cmd);
                }
                else
                {
                    await new UsuarioDAO().GravarOutrosUsuarios(obj, null);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao salvar Aluno_Atividade! Erro: " + ex.Message);
                return false;
            }
        }

        public async Task<List<AlunoDTO>> Listar(string busca)
        {
            Conexao Conexao = new();
            SqlCommand cmd = new SqlCommand();

            try
            {
                if (string.IsNullOrEmpty(busca))
                    cmd.CommandText = "select a.idAluno, u.nomeUsuario, e.nomeEscola, t.descricaoTurma from aluno a " +
                    "inner join escola e on e.idEscola = a.escola inner join usuario u on u.idUsuario = a.usuario " +
                    "inner join turma t on t.idTurma = a.turma";
                else
                    cmd.CommandText = "select a.idAluno, u.nomeUsuario, e.nomeEscola, t.descricaoTurma from aluno a " +
                    "inner join escola e on e.idEscola = a.escola inner join usuario u on u.idUsuario = a.usuario " +
                    "inner join turma t on t.idTurma = a.turma where u.nomeUsuario like '%" + busca;

                List<AlunoDTO> ListaAluno = await Conexao.ExecuteQueryToListAsync(cmd, reader => new AlunoDTO
                {
                    idUsuario = reader.GetInt32(reader.GetOrdinal("idAluno")),
                    nomeUsuario = reader.GetString(reader.GetOrdinal("nomeUsuario")),
                    escola = new EscolaDTO(0, reader.GetString(reader.GetOrdinal("nomeEscola"))),
                    turma = new TurmaDTO(0, reader.GetString(reader.GetOrdinal("descricaoTurma")))
                });

                return ListaAluno;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao listar Aluno_Atividade! Erro: " + ex.Message);
                return null;
            }
        }

        public async Task<AlunoDTO> Carregar(int idObject)
        {
            Conexao Conexao = new();
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandText = "select a.*, u.*, t.idTurma, e.idEscola from aluno a " +
                    "inner join usuario u on u.idUsuario = a.usuario inner join turma t on t.idTurma = a.turma " +
                    "inner join escola e on e.idEscola = a.escola where a.usuario = @id";

                cmd.Parameters.AddWithValue("@id", idObject);
                var resul = cmd.ExecuteReader();

                List<AlunoDTO> ListaAluno_Atividade = await Conexao.ExecuteQueryToListAsync(cmd, reader => new AlunoDTO
                {
                    idUsuario = reader.GetInt32(reader.GetOrdinal("idUsuario")),
                    nomeUsuario = reader.GetString(reader.GetOrdinal("nomeUsuario")),
                    idade = reader.GetInt16(reader.GetOrdinal("idade")),
                    loginUsuario = reader.GetString(reader.GetOrdinal("loginUsuario")),
                    ra = reader.GetString(reader.GetOrdinal("ra")),
                    turma = new TurmaDTO(
                        reader.GetInt32(reader.GetOrdinal("turma")),
                        null
                    ),
                                    escola = new EscolaDTO(
                        reader.GetInt32(reader.GetOrdinal("escola")),
                        null
                    )
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

                cmd.CommandText = "delete from aluno where idaluno_atividade = @id";
                cmd.Parameters.AddWithValue("@id", idObject);
                await Conexao.ExecuteQuery(cmd);
                cmd.Parameters.Clear();

                cmd.CommandText = "delete from usuario where idUsuario = @id;";
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
