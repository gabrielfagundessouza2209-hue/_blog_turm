# Blog da Turma

Aplicação de console desenvolvida em **C#** com **.NET 8**, organizada
em camadas inspiradas no padrão **MVC** e integrada a um banco de dados
**MySQL**. O projeto permite cadastrar usuários, autenticar contas e
consultar publicações armazenadas no banco.

## Sobre o projeto

O Blog da Turma é um sistema acadêmico de gerenciamento de usuários e
publicações. A aplicação utiliza uma interface textual no terminal e
separa as responsabilidades entre modelos, visualizações, controladores,
serviços e acesso a dados.

## Funcionalidades implementadas

-   Cadastro de usuários com nome, e-mail e senha.
-   Armazenamento de senhas utilizando hash com **BCrypt**.
-   Login com verificação da senha informada.
-   Controle de sessão do usuário autenticado.
-   Encerramento da sessão (logout).
-   Listagem de publicações ordenadas da mais recente para a mais
    antiga.
-   Solicitação de título e conteúdo para uma nova publicação na camada
    de visualização.

> **Estado atual do código:** a opção de criar publicação aparece no
> menu, mas não está conectada ao controlador no `Program.cs`. Além
> disso, o método `PublicacaoService.Criar` possui um bloco condicional
> que impede a execução normal da inserção quando recebe uma publicação
> válida. Portanto, a criação de publicações precisa de correção antes
> de ser considerada funcional de ponta a ponta.

## Tecnologias utilizadas

  Tecnologia              Utilização
  ----------------------- -----------------------------------------
  C#                      Linguagem de programação
  .NET 8                  Plataforma de execução
  MySQL                   Banco de dados relacional
  MySql.Data 26.10.0      Conector do C# com MySQL
  BCrypt.Net-Next 4.2.0   Geração e verificação de hash de senhas

## Organização do projeto

A aplicação principal está localizada em `_blog_turm/BlogDaTurma`.

``` text
BlogDaTurma/
├── controllers/
│   ├── AutenticacaoController.cs
│   └── PublicacaoController.cs
├── data/
│   └── Conexao.cs
├── models/
│   ├── Comentario.cs
│   ├── Publicacao.cs
│   └── Usuario.cs
├── services/
│   ├── PublicacaoService.cs
│   ├── SenhaService.cs
│   ├── SessaoUsuario.cs
│   └── UsuarioService.cs
├── views/
│   ├── AutenticacaoView.cs
│   ├── MenuView.cs
│   └── PublicacaoView.cs
├── Program.cs
└── BlogDaTurma.csproj
```

Também existe a pasta `app_console`, que contém um projeto de console
separado. O ponto de entrada e a implementação principal do Blog da
Turma estão em `_blog_turm/BlogDaTurma`.

## Arquitetura e responsabilidades

O código separa as responsabilidades em camadas:

-   **Models:** representam as entidades do sistema. `Usuario` armazena
    os dados do usuário, `Publicacao` representa uma publicação e
    `Comentario` representa um comentário.
-   **Views:** cuidam da interação pelo terminal, recebendo dados e
    exibindo menus, publicações e mensagens.
-   **Controllers:** coordenam as ações solicitadas pelo usuário e
    chamam os serviços correspondentes.
-   **Services:** concentram regras e operações da aplicação, como
    cadastro, autenticação, hash de senha, sessão e operações de
    publicação.
-   **Data:** contém a classe `Conexao`, responsável por criar conexões
    com o MySQL.

O `Program.cs` instancia as dependências e inicia o menu da aplicação.

## Banco de dados

A aplicação espera encontrar um banco MySQL. Pelas consultas presentes
no código, são utilizadas as tabelas `usuario` e `publicacao`.

### Estrutura mínima esperada

A estrutura abaixo é uma referência baseada nos campos utilizados pelas
consultas do projeto. Ela não foi encontrada como script SQL dentro dos
arquivos enviados; revise-a conforme as regras e relacionamentos
desejados para o banco.

``` sql
CREATE DATABASE IF NOT EXISTS BlogDaTurmaDB;
USE BlogDaTurmaDB;

CREATE TABLE usuario (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(150) NOT NULL,
    email VARCHAR(150) NOT NULL UNIQUE,
    senha_hash VARCHAR(255) NOT NULL
);

CREATE TABLE publicacao (
    id INT AUTO_INCREMENT PRIMARY KEY,
    titulo VARCHAR(200) NOT NULL,
    texto TEXT NOT NULL,
    data_criacao DATETIME NOT NULL,
    usuario_id INT NOT NULL,
    CONSTRAINT fk_publicacao_usuario
        FOREIGN KEY (usuario_id) REFERENCES usuario(id)
);
```

A classe `Comentario` existe no código, mas não há operações de
comentários nem consulta à tabela correspondente implementadas nos
serviços apresentados.

## Configuração da conexão

A classe `Conexao.cs` lê as configurações pelas seguintes variáveis de
ambiente:

  Variável        Valor padrão no código
  --------------- ------------------------
  `DB_HOST`       `localhost`
  `DB_PORT`       `3306`
  `DB_NAME`       `BlogDaTurmaDB`
  `DB_USER`       `root`
  `DB_PASSWORD`   `root`

Configure essas variáveis de acordo com seu ambiente. Exemplo para
PowerShell:

``` powershell
$env:DB_HOST="localhost"
$env:DB_PORT="3306"
$env:DB_NAME="BlogDaTurmaDB"
$env:DB_USER="root"
$env:DB_PASSWORD="sua_senha"
```

Altere os valores para corresponder à configuração local do MySQL. Evite
publicar senhas reais ou credenciais no GitHub.

## Como executar

### Pré-requisitos

-   .NET SDK 8.0 ou compatível com o projeto.
-   MySQL instalado e em execução.
-   Banco de dados e tabelas criados conforme a estrutura esperada.
-   Visual Studio ou Visual Studio Code (opcional, para editar o
    código).

### Passos

1.  Clone o repositório:

    ``` bash
    git clone URL_DO_SEU_REPOSITORIO
    ```

2.  Entre na pasta do projeto:

    ``` bash
    cd ATV_3/_blog_turm/BlogDaTurma
    ```

3.  Configure as variáveis de ambiente de conexão com o MySQL.

4.  Restaure as dependências:

    ``` bash
    dotnet restore
    ```

5.  Execute a aplicação:

    ``` bash
    dotnet run
    ```

## Segurança

As senhas não são armazenadas diretamente. A classe `SenhaService`
utiliza BCrypt para gerar um hash no cadastro e verificar a senha
durante o login. A autenticação consulta o usuário pelo e-mail e compara
a senha informada com o hash armazenado.

As consultas SQL utilizam parâmetros (`@nome`, `@email`, `@hash` etc.),
evitando concatenar diretamente os valores informados pelo usuário nas
instruções SQL.

## Melhorias futuras

-   Corrigir e integrar o fluxo de criação de publicações.
-   Implementar edição e exclusão de publicações.
-   Implementar cadastro, listagem e gerenciamento de comentários.
-   Adicionar validações de formato de e-mail e regras de senha.
-   Melhorar o tratamento de configurações e credenciais do banco de
    dados.
-   Criar testes automatizados para serviços e controladores.

## Autor

**Gabriel Fagundes**

Projeto desenvolvido para fins de estudo e prática de programação em C#,
organização em camadas, acesso a banco de dados e autenticação de
usuários.
