using System;
using BlogDaTurma.Data;
using BlogDaTurma.Services;
using BlogDaTurma.Views;
using BlogDaTurma.Controllers;

Conexao conexao = new Conexao();
SessaoUsuario sessaoUsuario = new SessaoUsuario();

PublicacaoService publicacaoService = new PublicacaoService(conexao);
UsuarioService usuarioService = new UsuarioService(conexao);

MenuView menuView = new MenuView();
PublicacaoView publicacaoView = new PublicacaoView();
AutenticacaoView autenticacaoView = new AutenticacaoView();
PublicacaoController publicacaoController = new PublicacaoController(publicacaoView, publicacaoService,sessaoUsuario);
AutenticacaoController autenticacaoController = new AutenticacaoController(autenticacaoView, usuarioService, sessaoUsuario);

bool executando = true;

autenticacaoController.Entrar();

while (executando)
{
    if (sessaoUsuario.EstaAutenticado)
    {
        Console.WriteLine($"\n[ Status: Conectado como '{sessaoUsuario.NomeUsuario}' (ID: {sessaoUsuario.UsuarioId}) ]");
    }
    else
    {
        Console.WriteLine("\n[ Status: Não autenticado ]");
    }

    int opcao = menuView.ExibirMenu();

    switch (opcao)
    {
        case 1:
            publicacaoController.Listar();
            break;
        case 2:
            autenticacaoController.Cadastrar();
            break;
        case 3:
            autenticacaoController.Entrar();
            break;
        case 4:
            autenticacaoController.Sair();
            break;
        case 0:
            Console.WriteLine("\nEncerrando o Blog da Turma. Até logo!");
            executando = false;
            break;
        default:
            publicacaoView.ExibirMensagem("Opção inválida! Escolha um número válido do menu.");
            break;
    }
}