# Sales Goals Manager

Sistema para cadastro e gerenciamento de metas de vendas desenvolvido em **C#/.NET**, com arquitetura em camadas, aplicação desktop WPF, API REST e testes unitários.

O objetivo do projeto é permitir o gerenciamento de metas comerciais associadas a vendedores e produtos, aplicando regras de negócio específicas de acordo com o tipo de meta e categoria do produto.

---

# Funcionalidades

✅ Cadastro, edição e exclusão de vendedores

✅ Cadastro, edição e exclusão de produtos

✅ Cadastro, edição e exclusão de metas de vendas

✅ Consulta e busca de metas cadastradas

✅ Validação de regras de negócio

✅ Persistência de dados em banco de dados SQL Server

✅ API REST completa (CRUD) para Vendedor, Produto e Meta

✅ Testes unitários utilizando xUnit

---

# Regras de Negócio

O sistema possui validações para garantir a consistência das informações cadastradas.

Algumas regras implementadas:

- Não permitir cadastro de metas sem vendedor.
- Não permitir cadastro de metas sem produto.
- O valor da meta deve ser maior que zero.
- Validação de campos obrigatórios.
- Compatibilidade entre o tipo da meta e o produto selecionado.
- Aplicação de regras específicas para metas baseadas em litros (só produtos líquidos).
- Não permitir duas metas idênticas para o mesmo vendedor, produto e periodicidade.
- Não permitir cadastro de vendedor sem nome.
- Não permitir cadastro de produto sem nome.
- Não permitir vendedores ou produtos duplicados (mesmo nome, ignorando maiúsculas/minúsculas).
- Validação das informações antes da persistência dos dados.

### Tipos de Meta

| Tipo | Descrição |
|--------|-----------|
| R$ | Valor monetário |
| L | Litros |
| UN | Unidades |

### Categoria de Produto

| Categoria | Descrição |
|--------|-----------|
| Liquido | Barris, garrafas e latas |
| Diversos | Acessórios e demais produtos |

### Periodicidade

As metas podem ser configuradas como **Diária**, **Semanal** ou **Mensal**.

---

# Arquitetura da Solução

A solução foi desenvolvida seguindo o princípio de **separação de responsabilidades**, mantendo a interface desacoplada das regras de negócio. Tanto a aplicação desktop quanto a API consomem a mesma camada de regra de negócio, que concentra o acesso a dados via Entity Framework Core.

```text
SalesGoalsManager
│
├── SalesGoalsManager.RegraDeNegocio
├── SalesGoalsManager.RegraDeNegocio.Testes
├── SalesGoalsManager.WPF
└── SalesGoalsManager.Api
```

```text
   SalesGoalsManager.WPF          SalesGoalsManager.Api
            │                              │
            └──────────────┬───────────────┘
                            ▼
            SalesGoalsManager.RegraDeNegocio
       (Entidades, DTOs, Repositórios, Cadastros,
              Validações, Contexto EF Core)
                            │
                            ▼
                      SQL Server
```

## SalesGoalsManager.RegraDeNegocio

Camada responsável pela lógica de domínio da aplicação. É a única camada que conhece o Entity Framework Core e o banco de dados — tanto o WPF quanto a API a consomem diretamente, sem comunicação HTTP entre si.

Contém:

- `Dto/` — objetos de transferência de dados compartilhados entre WPF e API
- `Entidades/` — entidades mapeadas pelo Entity Framework Core
- `Interfaces/` — contratos dos repositórios (`IMetaRepositorio`, `IProdutoRepositorio`, `IVendedorRepositorio`)
- `Repositorios/` — implementação de acesso a dados via EF Core
- `Cadastro/` — classes de orquestração de salvar/editar/excluir (`MetaCadastro`, `ProdutoCadastro`, `VendedorCadastro`)
- `Validacoes/` — regras de validação de negócio para cada entidade
- `Contexto/` — `DbContext` e configuração do Entity Framework Core
- `Migrations/` — histórico de migrations do banco de dados
- `Extensoes/` — métodos de extensão (string, enum) e exceções customizadas

### Principais entidades

- Meta
- Produto
- Vendedor

---

## SalesGoalsManager.Api

Projeto ASP.NET Core responsável por disponibilizar os serviços da aplicação via HTTP, consumindo diretamente as classes de `RegraDeNegocio`.

### Controllers

| Controller | Endpoints |
|---|---|
| `MetaController` | `GET /Meta`, `GET /Meta/{id}`, `POST /Meta`, `PUT /Meta/{id}`, `DELETE /Meta/{id}` |
| `ProdutoController` | `GET /Produto`, `GET /Produto/{id}`, `POST /Produto`, `PUT /Produto/{id}`, `DELETE /Produto/{id}` |
| `VendedorController` | `GET /Vendedor`, `GET /Vendedor/{id}`, `POST /Vendedor`, `PUT /Vendedor/{id}`, `DELETE /Vendedor/{id}` |

Todas as operações de escrita (`POST`/`PUT`) passam pelas classes de `Cadastro`, que aplicam as validações de negócio antes de persistir — erros de validação retornam `400 Bad Request` com a mensagem correspondente.

A API permite que outras aplicações (por exemplo, um front-end web) possam consumir as funcionalidades do sistema além da aplicação desktop.

---

## SalesGoalsManager.WPF

Aplicação Desktop desenvolvida utilizando WPF e padrão MVVM. Consome a camada `RegraDeNegocio` diretamente (sem HTTP), através de uma fábrica de serviços.

### Telas

- **Tela Inicial** — listagem, busca e exclusão de metas cadastradas
- **Cadastro de Meta** — cadastro e edição de metas, com seleção de vendedor, produto, tipo de meta e periodicidade
- **Cadastro de Vendedor** — cadastro e edição de vendedores
- **Cadastro de Produto** — cadastro e edição de produtos, com seleção de categoria

### Estrutura

- Views
- ViewModels
- Commands
- Converters
- Recursos visuais

O objetivo é manter a interface desacoplada das regras de negócio, facilitando manutenção e evolução do sistema.

---

## SalesGoalsManager.RegraDeNegocio.Testes

Projeto responsável pelos testes unitários utilizando xUnit.

O foco dos testes é garantir a integridade das validações e regras de negócio da aplicação, com classes de teste dedicadas para cada entidade:

- `MetaVendedorValidacaoTestes`
- `CadastroProdutoValidacaoTestes`
- `CadastroVendedorValidacaoTestes`

---

# Padrões e Conceitos Utilizados

- MVVM (Model-View-ViewModel)
- Repository Pattern
- DTO Pattern
- Entity Framework Core (Code First)
- Injeção de Dependência (ASP.NET Core DI Container)
- REST API
- Orientação a Objetos
- Separação de Responsabilidades
- Validações de Domínio

---

# Persistência de Dados

O projeto utiliza:

- SQL Server (LocalDB por padrão em ambiente de desenvolvimento)
- Entity Framework Core
- Migrations (Code First)

As tabelas e estruturas do banco de dados são gerenciadas através das migrations do Entity Framework, localizadas no projeto `SalesGoalsManager.RegraDeNegocio`.

---

# API

A aplicação disponibiliza endpoints REST para manipulação completa (CRUD) das três entidades principais do sistema.

### Vendedores

```http
GET    /Vendedor
GET    /Vendedor/{id}
POST   /Vendedor
PUT    /Vendedor/{id}
DELETE /Vendedor/{id}
```

### Produtos

```http
GET    /Produto
GET    /Produto/{id}
POST   /Produto
PUT    /Produto/{id}
DELETE /Produto/{id}
```

### Metas

```http
GET    /Meta
GET    /Meta/{id}
POST   /Meta
PUT    /Meta/{id}
DELETE /Meta/{id}
```

A documentação completa dos endpoints pode ser acessada através do Swagger ao executar a API (`/swagger`).

---

# Tecnologias Utilizadas

## Backend

- C#
- .NET 9
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server

## Desktop

- WPF
- XAML
- MVVM

## Testes

- xUnit

## Ferramentas

- Visual Studio
- Git
- GitHub

---

# Como Executar o Projeto

## Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- SQL Server LocalDB (instalado junto com o Visual Studio) ou outra instância SQL Server acessível
- Visual Studio 2022 (recomendado) ou `dotnet` CLI

## 1. Clonar o Repositório

```bash
git clone https://github.com/Dougmgm/SalesGoalsManager.git
```

## 2. Configurar a Connection String

Verifique o arquivo `appsettings.json` em `SalesGoalsManager.Api` e ajuste a connection string, se necessário, para apontar para sua instância SQL Server:

```json
"ConnectionStrings": {
  "SalesGoalsManagerConnectionString": "Server=(localdb)\\mssqllocaldb;Database=SalesGoalsManagerDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

## 3. Restaurar Pacotes

```bash
dotnet restore
```

## 4. Aplicar as Migrations

```bash
cd SalesGoalsManager.RegraDeNegocio
dotnet ef database update
```

## 5. Executar a API

```bash
dotnet run --project SalesGoalsManager.Api
```

A API estará disponível em `https://localhost:7109` (verifique a porta exata em `launchSettings.json`), com Swagger em `/swagger`.

## 6. Executar a Aplicação Desktop

Abra a solução no Visual Studio e defina:

```text
SalesGoalsManager.WPF
```

como projeto de inicialização, e execute (F5).

---

# Estrutura do Projeto

```text
SalesGoalsManager.RegraDeNegocio
├── Cadastro
│   ├── MetaCadastro.cs
│   ├── ProdutoCadastro.cs
│   └── VendedorCadastro.cs
├── Contexto
├── Dto
├── Entidades
├── Extensoes
├── Interfaces
├── Migrations
├── Repositorios
│   ├── MetaRepositorio.cs
│   ├── ProdutoRepositorio.cs
│   └── VendedorRepositorio.cs
└── Validacoes
    ├── MetaVendedorValidacao.cs
    ├── CadastroProdutoValidacao.cs
    └── CadastroVendedorValidacao.cs
```

---

# Melhorias Futuras

- [ ] Autenticação e autorização (JWT)
- [ ] Paginação nos endpoints de listagem
- [ ] Testes de integração da API
- [ ] Testes unitários para as classes de Cadastro (com mock de repositório)
- [ ] Pipeline de CI (build + testes a cada push)
- [ ] Exportação de metas para Excel
- [ ] Front-end web (Angular) consumindo a API
- [ ] Deploy da API em nuvem

---

# Testes

Os testes unitários podem ser executados através do comando:

```bash
dotnet test
```

Atualmente os testes validam as regras de negócio críticas da aplicação, cobrindo os validadores de Meta, Produto e Vendedor.

---

# Licença

Este projeto está licenciado sob a licença MIT — veja o arquivo [LICENSE](LICENSE) para mais detalhes.

---

# Autor

## Douglas Menchon

Desenvolvedor .NET com experiência em desenvolvimento e manutenção de sistemas corporativos.

### Tecnologias

- C#
- .NET
- WPF
- XAML
- ASP.NET Core
- Entity Framework Core
- SQL Server
- Oracle / PL-SQL
- APIs REST
- NUnit
- xUnit
- Git

### Contato

- LinkedIn: https://www.linkedin.com/in/douglas-menchon/
- GitHub: https://github.com/Dougmgm

---

⭐ Projeto desenvolvido para estudo, aplicação de conceitos de arquitetura de software, orientação a objetos e desenvolvimento de aplicações corporativas utilizando a plataforma .NET.
