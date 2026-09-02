# SalesGoalsManager

Sistema para cadastro e gerenciamento de metas de vendas, desenvolvido em C#/.NET com WPF, seguindo uma estrutura separada entre interface, regras de negócio e API.

## Sobre o projeto

O Sales Goals Manager permite o cadastro de metas comerciais associadas a vendedores e produtos.

Uma meta possui informações como:

Vendedor;
Produto;
Tipo de meta;
Valor;
Periodicidade.

Os tipos de meta contemplados são:

R$ — Valor monetário
L — Litros
UN — Unidades

A aplicação também considera diferentes categorias de produtos e aplica regras específicas de acordo com o tipo de meta selecionado.

## Arquitetura

Uma das principais preocupações do projeto é manter as regras de negócio separadas da interface gráfica.

Entre as validações implementadas estão:

Campos obrigatórios;
Validação do valor informado;
Compatibilidade entre o tipo de meta e o produto selecionado;
Regras específicas para metas baseadas em litros;
Validação das informações antes da persistência.

A ideia é evitar que essas regras fiquem diretamente acopladas aos eventos da interface WPF.

## Aplicações

A interface foi desenvolvida utilizando WPF, com XAML para definição das interfaces e C# para a implementação da aplicação.

A estrutura da aplicação desktop contém componentes específicos para:

Interface;
Regras de negócio;
Classes comuns;
Extensões;
Recursos visuais.

O projeto utiliza uma abordagem orientada à separação de responsabilidades, buscando reduzir o acoplamento entre a interface e as regras do domínio.

## API

O projeto também possui uma aplicação ASP.NET Core Web API, responsável pela camada de serviços.

A API possui uma estrutura organizada em:

Controllers;
Data;
Models;
Domain.

Isso permite que a aplicação desktop não seja o único consumidor possível das funcionalidades do sistema, criando uma base para futuras aplicações ou integrações.

## Tecnologias

Desktop:
 - C#
 - .NET
 - WPF
 - XAML
   
Backend:

 - ASP.NET Core
 - Web API
 - C#

Arquitetura e desenvolvimento:

 - Orientação a Objetos
 - Separação de responsabilidades 
 - DTOs
 - Regras de negócio
 - REST

Ferramentas:

 - Visual Studio
 - Git
 - GitHub

## Testes

Será executado testes unitários utilizando XUnit

## Autor

Douglas Menchon

Desenvolvedor .NET com experiência profissional em desenvolvimento e manutenção de sistemas corporativos.

Principais tecnologias:

C# · .NET · WPF · XAML · ASP.NET Core · REST APIs · Oracle/PL-SQL · SQL · Entity Framework · NUnit · Git

LinkedIn · GitHub
