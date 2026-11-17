using System;
using BlogDaTurma.Data;
using BlogDaTurma.Models;
using MySql.Data.MySqlClient;

namespace BlogDaTurma.Services
{
    public class UsuarioService
    {
        private Conexao conexao;
        private SenhaService senhaService;

        public UsuarioService(Conexao conexao)
        {
            this.conexao = conexao;
            this.senhaService = new SenhaService();
        }

        public void Cadastrar(string nome, string email, string senha)
        {
            string hash = senhaService.GerarHash(senha);

            using (var conn = conexao.CriarConexao())
            {
                conn.Open();
                string query = "INSERT INTO usuario (nome, email, senha_hash) VALUES (@nome, @email, @hash)";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@hash", hash);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Usuario? Autenticar(string email, string senha)
        {
            using (var conn = conexao.CriarConexao())
            {
                conn.Open();
                string query = "SELECT id, nome, email, senha_hash FROM usuario WHERE email = @email LIMIT 1";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string hashBanco = reader.GetString("senha_hash");

                            if (senhaService.Verificar(senha, hashBanco))
                            {
                                return new Usuario
                                {
                                    Id = reader.GetInt32("id"),
                                    Nome = reader.GetString("nome"),
                                    Email = reader.GetString("email"),
                                    SenhaHash = hashBanco
                                };
                            }
                        }
                    }
                }
            }
            return null;
        }
    }
}