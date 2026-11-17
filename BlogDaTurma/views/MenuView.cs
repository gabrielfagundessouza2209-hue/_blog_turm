using System;

namespace BlogDaTurma.Views
{
    public class MenuView
    {
        public int ExibirMenu()
        {
            Console.WriteLine("\n=== BLOG DA TURMA ===");
            Console.WriteLine("1 - Listar publicações");
            Console.WriteLine("2 - Cadastrar-se");
            Console.WriteLine("3 - Entrar (Login)");
            Console.WriteLine("4 - Sair da conta (Logout)");
            Console.WriteLine("5 - Fazer publicação");
            Console.WriteLine("0 - Encerrar Programa");
            Console.Write("Escolha uma opção: ");

            string? entrada = Console.ReadLine();
            if (int.TryParse(entrada, out int opcao))
            {
                return opcao;
            }

            return -1;
        }
    }
}