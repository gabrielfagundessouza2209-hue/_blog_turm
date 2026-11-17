using System;
using MySql.Data.MySqlClient;

namespace BlogDaTurma.Data
{
    public class Conexao
    {
        private string connectionString;

        public Conexao()
        {
            string host = LerVariavel("DB_HOST", "localhost");
            string port = LerVariavel("DB_PORT", "3306");
            string database = LerVariavel("DB_NAME", "BlogDaTurmaDB");
            string user = LerVariavel("DB_USER", "root");
            string password = LerVariavel("DB_PASSWORD", "root");

            connectionString = $"Server={host};Port={port};Database={database};Uid={user};Pwd={password};";
        }

        public MySqlConnection CriarConexao()
        {
            return new MySqlConnection(connectionString);
        }

        private string LerVariavel(string nome, string valorPadrao)
        {
            string? valor = Environment.GetEnvironmentVariable(nome);
            return string.IsNullOrWhiteSpace(valor) ? valorPadrao : valor;
        }
    }
}