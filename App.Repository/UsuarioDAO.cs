using App.Repository;
using Microsoft.Data.SqlClient;
using SGED.Domain;
using SGED.Repository;
using System.Security.Cryptography;

public class UsuarioDAO : IGenericDAO<UsuarioDTO>
{
    public async Task<bool> Salvar(UsuarioDTO obj)
    {
        try
        {
            string salt = GerarSalt();
            Conexao conexao = new();
            SqlCommand cmd = new();

            if (obj.idUsuario == 0)
            {
                cmd.CommandText = "INSERT INTO usuario(nomeUsuario, idade, salt, loginUsuario, senhaUsuario, tipoUsuario) " +
                                  "VALUES (@nome, @idade, @salt, @login, @senha, @tipo);";
                cmd.Parameters.AddWithValue("@salt", salt);
                cmd.Parameters.AddWithValue("@senha", HashPassword(salt, obj.senhaUsuario));
                cmd.Parameters.AddWithValue("@tipo", obj.tipoUsuario);
            }
            else if (!string.IsNullOrEmpty(obj.senhaUsuario))
            {
                cmd.CommandText = "UPDATE usuario SET nomeUsuario = @nome, idade = @idade, loginUsuario = @login, " +
                                  "senhaUsuario = @senha, salt = @salt WHERE idUsuario = @id;";
                cmd.Parameters.AddWithValue("@id", obj.idUsuario);
                cmd.Parameters.AddWithValue("@senha", HashPassword(salt, obj.senhaUsuario));
                cmd.Parameters.AddWithValue("@salt", salt);
            }
            else
            {
                cmd.CommandText = "UPDATE usuario SET nomeUsuario = @nome, idade = @idade, loginUsuario = @login WHERE idUsuario = @id;";
                cmd.Parameters.AddWithValue("@id", obj.idUsuario);
            }

            cmd.Parameters.AddWithValue("@nome", obj.nomeUsuario);
            cmd.Parameters.AddWithValue("@idade", obj.idade);
            cmd.Parameters.AddWithValue("@login", obj.loginUsuario);

            await conexao.ExecuteQuery(cmd);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro na DAO ao salvar usuário! Erro: " + ex.Message);
            return false;
        }
    }

    public async Task<List<UsuarioDTO>> Listar(string busca)
    {
        try
        {
            Conexao conexao = new();
            SqlCommand cmd = new();

            if (string.IsNullOrEmpty(busca))
            {
                cmd.CommandText = "SELECT * FROM usuario;";
            }
            else
            {
                cmd.CommandText = "SELECT * FROM usuario WHERE nomeUsuario LIKE @busca;";
                cmd.Parameters.AddWithValue("@busca", $"%{busca}%");
            }

            return await conexao.ExecuteQueryToListAsync(cmd, reader => new UsuarioDTO
            {
                idUsuario = Convert.ToInt32(reader["idUsuario"]),
                nomeUsuario = reader["nomeUsuario"].ToString(),
                idade = Convert.ToInt16(reader["idade"]),
                loginUsuario = reader["loginUsuario"].ToString(),
                tipoUsuario = Convert.ToInt16(reader["tipoUsuario"])
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro na DAO ao listar Usuario! Erro: " + ex.Message);
            return null;
        }
    }

    public async Task<UsuarioDTO> Carregar(int idObject)
    {
        try
        {
            Conexao conexao = new();
            SqlCommand cmd = new()
            {
                CommandText = "SELECT * FROM usuario WHERE idUsuario = @id;"
            };
            cmd.Parameters.AddWithValue("@id", idObject);

            var lista = await conexao.ExecuteQueryToListAsync(cmd, reader => new UsuarioDTO
            {
                idUsuario = Convert.ToInt32(reader["idUsuario"]),
                nomeUsuario = reader["nomeUsuario"].ToString(),
                idade = Convert.ToInt16(reader["idade"]),
                loginUsuario = reader["loginUsuario"].ToString(),
                tipoUsuario = Convert.ToInt16(reader["tipoUsuario"])
            });

            return lista.FirstOrDefault();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro na DAO ao carregar Usuario! Erro: " + ex.Message);
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
                CommandText = "DELETE FROM usuario WHERE idUsuario = @id;"
            };
            cmd.Parameters.AddWithValue("@id", idObject);

            await conexao.ExecuteQuery(cmd);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro na DAO ao excluir Usuario! Erro: " + ex.Message);
            return false;
        }
    }

    public async Task<UsuarioDTO> Logar(string login, string senha)
    {
        try
        {
            Conexao conexao = new();
            SqlCommand cmd = new()
            {
                CommandText = "SELECT * FROM usuario WHERE loginUsuario = @login;"
            };
            cmd.Parameters.AddWithValue("@login", login);

            var lista = await conexao.ExecuteQueryToListAsync(cmd, reader =>
            {
                string salt = reader["salt"].ToString();
                string senhaBanco = reader["senhaUsuario"].ToString();
                if (HashPassword(salt, senha) != senhaBanco)
                    return null;

                return new UsuarioDTO
                {
                    idUsuario = Convert.ToInt32(reader["idUsuario"]),
                    nomeUsuario = reader["nomeUsuario"].ToString(),
                    loginUsuario = reader["loginUsuario"].ToString(),
                    tipoUsuario = Convert.ToInt16(reader["tipoUsuario"])
                };
            });

            // Filtra nulls caso senha esteja errada
            return lista.FirstOrDefault(u => u != null);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro na DAO Usuario ao logar no sistema! Erro: " + ex.Message);
            return null;
        }
    }

    private string GerarSalt() 
    {
        var saltBytes = new byte[80];
        RandomNumberGenerator.Fill(saltBytes); // Preenche com bytes aleatórios seguros
        return Convert.ToBase64String(saltBytes);
    }

    private string HashPassword(string salt, string senha)
    {
        int iteration = 90000;
        int nHash = 80;
        var saltBytes = Convert.FromBase64String(salt);

        using var rfc289DeriveBytes = new Rfc2898DeriveBytes(
            senha,
            saltBytes,
            iteration,
            HashAlgorithmName.SHA256 // ou SHA512, conforme seu critério
        );
        return Convert.ToBase64String(rfc289DeriveBytes.GetBytes(nHash));
    }

    public async Task<Int32?> GravarOutrosUsuarios(UsuarioDTO obj, int? tipo)
    {
        Int32? resul = null;
        try
        {
            Conexao Conexao = new();
            String salt = GerarSalt();
            SqlCommand cmd = new SqlCommand();

            if (obj.idUsuario == 0)
            {
                cmd.CommandText = "insert into usuario(nomeUsuario, idade, salt, loginUsuario, senhaUsuario, tipoUsuario) " +
                    "values(@nome, @idade, @salt, @login, @senha, @tipo); select SCOPE_IDENTITY()";
                cmd.Parameters.AddWithValue("@salt", salt);
                cmd.Parameters.AddWithValue("@senha", HashPassword(salt, obj.senhaUsuario));
                cmd.Parameters.AddWithValue("@tipo", tipo);
                cmd.Parameters.AddWithValue("@nome", obj.nomeUsuario);
                cmd.Parameters.AddWithValue("@idade", obj.idade);
                cmd.Parameters.AddWithValue("@login", obj.loginUsuario);
                resul = Convert.ToInt32(await Conexao.ExecuteScalarAsync(cmd));
                return resul;
            }
            else if (string.IsNullOrEmpty(obj.senhaUsuario))
            {
                cmd.CommandText = "update usuario set nomeUsuario = @nome, idade = @idade, loginUsuario = @login " +
                    "where idUsuario = @id";
                cmd.Parameters.AddWithValue("@nome", obj.nomeUsuario);
                cmd.Parameters.AddWithValue("@idade", obj.idade);
                cmd.Parameters.AddWithValue("@login", obj.loginUsuario);
                cmd.Parameters.AddWithValue("@id", obj.idUsuario);
                await Conexao.ExecuteQuery(cmd);
                return null;
            }
            else
            {
                cmd.CommandText = "update usuario set senhaUsuario = @senha, salt = @salt where idUsuario = @id";
                cmd.Parameters.AddWithValue("@senha", HashPassword(salt, obj.senhaUsuario));
                cmd.Parameters.AddWithValue("@salt", salt);
                cmd.Parameters.AddWithValue("@id", obj.idUsuario);
                await Conexao.ExecuteQuery(cmd);
                return null;
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro na DAO ao salvar usuário! Erro: " + ex.Message);
            return null;
        }
    }
}