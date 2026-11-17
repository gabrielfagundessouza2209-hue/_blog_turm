using System;
using BlogDaTurma.Models;
using BlogDaTurma.Services;
using BlogDaTurma.Views;

namespace BlogDaTurma.Controllers
{
    public class AutenticacaoController
    {
        private AutenticacaoView view;
        private UsuarioService service;
        private SessaoUsuario sessao;

        public AutenticacaoController(AutenticacaoView view, UsuarioService service, SessaoUsuario sessao)
        {
            this.view = view;
            this.service = service;
            this.sessao = sessao;
        }

        public void Cadastrar()
        {
            try
            {
                var (nome, email, senha) = view.SolicitarDadosCadastro();

                if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
                {
                    view.ExibirMensagem("Todos os campos são obrigatórios.");
                    return;
                }

                service.Cadastrar(nome, email, senha);
                view.ExibirMensagem("Usuário cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                view.ExibirMensagem($"Erro ao cadastrar usuário: {ex.Message}");
            }
        }

        public void Entrar()
        {
            try
            {
                if (sessao.EstaAutenticado)
                {
                    view.ExibirMensagem($"Você já está autenticado como '{sessao.NomeUsuario}'. Encerre a sessão atual primeiro.");
                    return;
                }

                var (email, senha) = view.SolicitarCredenciais();
                Usuario? usuario = service.Autenticar(email, senha);

                if (usuario != null)
                {
                    sessao.Iniciar(usuario);
                    view.ExibirMensagem($"Bem-vindo(a), {usuario.Nome}! Login realizado com sucesso.");
                }
                else
                {
                    view.ExibirMensagem("E-mail ou senha inválidos.");
                }
            }
            catch (Exception ex)
            {
                view.ExibirMensagem($"Falha no serviço de autenticação: {ex.Message}");
            }
        }

        public void Sair()
        {
            if (!sessao.EstaAutenticado)
            {
                view.ExibirMensagem("Nenhuma sessão ativa no momento.");
                return;
            }

            sessao.Encerrar();
            view.ExibirMensagem("Sessão encerrada com sucesso.");
        }
    }
}