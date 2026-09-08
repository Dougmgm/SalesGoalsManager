# Sales Goals Manager

Sistema para cadastro e gerenciamento de metas de vendas desenvolvido em **C#/.NET**, com arquitetura em camadas, aplicação desktop WPF, API REST e testes unitários.

O objetivo do projeto é permitir o gerenciamento de metas comerciais associadas a vendedores e produtos, aplicando regras de negócio específicas de acordo com o tipo de meta e categoria do produto.

---

# Funcionalidades

✅ Cadastro de vendedores

✅ Cadastro de produtos

✅ Cadastro de metas de vendas

✅ Consulta de metas cadastradas

✅ Validação de regras de negócio

✅ Persistência de dados em banco de dados SQL Server

✅ API REST para integração com aplicações externas

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
- Aplicação de regras específicas para metas baseadas em litros.
- Validação das informações antes da persistência dos dados.

### Tipos de Meta

| Tipo | Descrição |
|--------|-----------|
| R$ | Valor monetário |
| L | Litros |
| UN | Unidades |

### Periodicidade

As metas podem ser configuradas de acordo com a periodicidade definida pelo negócio.

---

# Arquitetura da Solução

A solução foi desenvolvida seguindo o princípio de **separação de responsabilidades**, mantendo a interface desacoplada das regras de negócio.

```text
SalesGoalsManager
│
├── SalesGoalManager.RegraDeNegocio
├── SalesGoalManager.RegraDeNegocio.Testes
├── SalesGoalManger.WPF
└── SalesGoalsManager.Api
```

## SalesGoalManager.RegraDeNegocio

Camada responsável pela lógica de domínio da aplicação.

Contém:

- Entidades
- DTOs
- Interfaces
- Repositórios
- Consultas
- Validações
- Extensões
- Entity Framework Core
- Contexto de banco de dados

### Principais entidades

- Meta
- Produto
- Vendedor

---

## SalesGoalsManager.Api

Projeto ASP.NET Core responsável por disponibilizar os serviços da aplicação.

### Controllers

- MetaController
- ProdutoController
- VendedorController

A API permite que outras aplicações possam consumir as funcionalidades do sistema além da aplicação desktop.

---

## SalesGoalManger.WPF

Aplicação Desktop desenvolvida utilizando WPF e padrão MVVM.

### Estrutura

- Views
- ViewModels
- Commands
- Converters
- Recursos visuais

O objetivo é manter a interface desacoplada das regras de negócio, facilitando manutenção e evolução do sistema.

---

## SalesGoalManager.RegraDeNegocio.Testes

Projeto responsável pelos testes unitários utilizando xUnit.

O foco dos testes é garantir a integridade das validações e regras de negócio da aplicação.

---

# Padrões e Conceitos Utilizados

- MVVM (Model-View-ViewModel)
- Repository Pattern
- DTO Pattern
- Dependency Injection
- Entity Framework Core
- REST API
- Orientação a Objetos
- Separação de Responsabilidades
- Validações de Domínio

---

# Persistência de Dados

O projeto utiliza:

- SQL Server
- Entity Framework Core
- Migrations (Code First)

As tabelas e estruturas do banco de dados são gerenciadas através das migrations do Entity Framework.

---

# API

A aplicação disponibiliza endpoints REST para manipulação dos dados.

### Vendedores

```http
GET /api/vendedor

POST /api/vendedor
```

### Produtos

```http
GET /api/produto

POST /api/produto
```

### Metas

```http
GET /api/meta

POST /api/meta
```

A documentação completa dos endpoints pode ser acessada através do Swagger ao executar a API.

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

## 1. Clonar o Repositório

```bash
git clone https://github.com/Dougmgm/SalesGoalsManager.git
```

## 2. Restaurar Pacotes

```bash
dotnet restore
```

## 3. Atualizar o Banco de Dados

```bash
dotnet ef database update
```

## 4. Executar a API

```bash
dotnet run --project SalesGoalsManager.Api
```

## 5. Executar a Aplicação Desktop

Abra a solução no Visual Studio e defina:

```text
SalesGoalManger.WPF
```

como projeto de inicialização.

---

# Telas

## Tela Inicial



---

# Estrutura do Projeto

```text
SalesGoalManager.RegraDeNegocio
├── Cadastro
├── Consultas
├── DTO
├── Entidades
├── Interfaces
├── Repositorios
├── Validacoes
└── Migrations
```

---

# Melhorias Futuras

- [ ] Cadastro de vendedores
- [ ] Cadastro de produtos
- [ ] Exportação para Excel
- [ ] Autenticação e autorização
- [ ] Deploy da API em nuvem

---

# Testes

Os testes unitários podem ser executados através do comando:

```bash
dotnet test
```

Atualmente os testes validam regras de negócio críticas da aplicação.

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
