using System;
using System.Collections.Generic;
using BlogDaTurma.Models;
using BlogDaTurma.Services;
using BlogDaTurma.Views;

namespace BlogDaTurma.Controllers
{
    public class PublicacaoController

    {
        private PublicacaoView view;
        private PublicacaoService service;
        private readonly SessaoUsuario sessao;

        public PublicacaoController(PublicacaoView view, PublicacaoService service, SessaoUsuario sessao)

        {
            this.sessao = sessao;
            this.view = view;
            this.service = service;
        }

        public void Listar()
        {
            try
            {
                List<Publicacao> publicacoes = service.ListarRecentes();
                view.ExibirPublicacoes(publicacoes);
            }
            catch (Exception ex)
            {
                view.ExibirMensagem($"Erro ao carregar publicações: {ex.Message}");
            }

        }
            public void Criar()
            {
                if(!sessao.EstaAutenticado)
                {
                    view.ExibirMensagem($"Entre na sua conta para publicar.");
                    return;
                }

                try 
                {
                    Publicacao publicacao = view.SolicitarNovaPublicacao();

                  publicacao.UsuarioId = sessao.UsuarioId!.Value;

                    service.Criar(publicacao);

                    view.ExibirMensagem($"Publicaação concluída com sucesso.");

                }

                    catch (ArgumentException ex)
                    {
                        view.ExibirMensagem(ex.Message);
                    }

                    catch(Exception ex)
                    {
                        view.ExibirMensagem($"Erro ao criar publicação. {ex.Message}");
                    }
            }
    }
}