using System;
using System.Collections.Generic;
using BlogDaTurma.Models;

namespace BlogDaTurma.Views
{
    public class PublicacaoView
    {



        public void ExibirPublicacoes(List<Publicacao> publicacoes)
        {
            Console.WriteLine("\n--- PUBLICAÇÕES RECENTES ---");

            if (publicacoes == null || publicacoes.Count == 0)
            {
                Console.WriteLine("Nenhuma publicação encontrada.");
                return;
            }

            foreach (var pub in publicacoes)
            {
                Console.WriteLine($"\n[ID: {pub.Id}] {pub.Titulo}");
                Console.WriteLine($"Data: {pub.DataCriacao:dd/MM/yyyy HH:mm}");
                Console.WriteLine($"Conteúdo: {pub.Texto}");
                Console.WriteLine(new string('-', 30));
            }
        }

        public void ExibirMensagem(string mensagem)
        {
            Console.WriteLine($"\n[Aviso]: {mensagem}");
        }

        
        public Publicacao SolicitarNovaPublicacao()
        {
            Console.WriteLine("\n----- Criar Publicação-----");

            Console.Write("Título");
            string Titulo = Console.ReadLine() ?? "";

            Console.WriteLine("Digite o conteúdo:");
            string texto = Console.ReadLine() ?? "";

            return new Publicacao
            {
                Titulo = Titulo,
                Texto = texto
            };
            
            
        }




    }
}