using System;

namespace BlogDaTurma.Views
{
    public class AutenticacaoView
    {
        public (string Nome, string Email, string Senha) SolicitarDadosCadastro()
        {
            Console.WriteLine("\n--- CADASTRO DE USUÁRIO ---");
            Console.Write("Nome: ");
            string nome = Console.ReadLine() ?? string.Empty;

            Console.Write("E-mail: ");
            string email = Console.ReadLine() ?? string.Empty;

            Console.Write("Senha: ");
            string senha = LerSenhaOculta();

            return (nome, email, senha);
        }

        public (string Email, string Senha) SolicitarCredenciais()
        {
            Console.WriteLine("\n--- AUTENTICAÇÃO ---");
            Console.Write("E-mail: ");
            string email = Console.ReadLine() ?? string.Empty;

            Console.Write("Senha: ");
            string senha = LerSenhaOculta();

            return (email, senha);
        }

        public void ExibirMensagem(string mensagem)
        {
            Console.WriteLine($"\n[Aviso]: {mensagem}");
        }

        private string LerSenhaOculta()
        {
            string senha = "";
            while (true)
            {
                var key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                if (key.Key == ConsoleKey.Backspace && senha.Length > 0)
                {
                    senha = senha[..^1];
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    senha += key.KeyChar;
                    Console.Write("*");
                }
            }
            return senha;
        }
    }
}