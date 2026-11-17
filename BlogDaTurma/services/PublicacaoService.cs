using System;
using System.Collections.Generic;
using BlogDaTurma.Data;
using BlogDaTurma.Models;
using MySql.Data.MySqlClient;

namespace BlogDaTurma.Services
{
    public class PublicacaoService
    {
        private Conexao conexao;

        public PublicacaoService(Conexao conexao)
        {
            this.conexao = conexao;
        }

        public List<Publicacao> ListarRecentes()
        {
            var publicacoes = new List<Publicacao>();

            using (var conn = conexao.CriarConexao())
            {
                conn.Open();
                string query = "SELECT id, titulo, texto, data_criacao, usuario_id FROM publicacao ORDER BY data_criacao DESC";

                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        publicacoes.Add(new Publicacao
                        {
                            Id = reader.GetInt32("id"),
                            Titulo = reader.GetString("titulo"),
                            Texto = reader.GetString("texto"),
                            DataCriacao = reader.GetDateTime("data_criacao"),
                            UsuarioId = reader.GetInt32("usuario_id")
                        });
                    }
                }
            }
            return publicacoes;
        }

        public void Criar(Publicacao publicacao)
        {
            if (publicacao == null)
            {
                if (publicacao == null) 
                         throw new ArgumentNullException (nameof(publicacao));

                if(string.IsNullOrWhiteSpace(publicacao.Titulo))
                throw new ArgumentException ("informe o título.");

                if (string.IsNullOrWhiteSpace(publicacao.Texto))
                throw new ArgumentException ("informe o conteúdo.");

                if(publicacao.UsuarioId <= 0)
                throw new ArgumentException("usuário inválido.");

                using(var conn = conexao.CriarConexao())
                {
                    conn.Open();


                    string sql =  @"
            INSERT INTO publicacao
                (titulo, texto, data_criacao, usuario_id)
            VALUES
                (@titulo, @texto, NOW(), @usuario_id)";

                using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue(
                        "@titulo", publicacao.Titulo.Trim());

                        cmd.Parameters.AddWithValue(
                        "@texto", publicacao.Texto.Trim());

                        cmd.Parameters.AddWithValue(
                          "@usuario_id", publicacao.UsuarioId);

                          cmd.ExecuteNonQuery();
                    }
;             
                }


               
                
            }

        }

            

    }
}