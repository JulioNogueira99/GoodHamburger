# Good Hamburger - Sistema de Pedidos 🍔

Este é o projeto desenvolvido para o Desafio Técnico de Desenvolvedor .NET da STgenetics. A API foi construída para gerenciar o cardápio e o fluxo de pedidos da "Good Hamburger", aplicando regras de negócio para cálculo de totais e descontos promocionais.

## 🚀 Tecnologias Utilizadas

* **.NET 10** / ASP.NET Core Web API
* **Entity Framework Core** com SQLite (Para facilitar a execução)
* **MediatR** (Padrão CQRS para orquestração de casos de uso)
* **FluentValidation** (Validação de entrada de dados)
* **xUnit & FluentAssertions** (Testes automatizados das regras de negócio)

## ⚙️ Como Executar o Projeto

Pensando na melhor Experiência do Desenvolvedor (DX), o projeto foi configurado para rodar sem a necessidade de instalar bancos de dados complexos ou containers.

1. Clone o repositório.
2. Navegue até a raiz do projeto.
3. Execute o comando: `dotnet run --project src/GoodHamburger.API`
4. O Swagger abrirá automaticamente no seu navegador (via HTTP).

**Nota sobre o Banco de Dados:** O sistema utiliza SQLite. Ao iniciar a API pela primeira vez, as *Migrations* são aplicadas automaticamente e o banco é populado com o cardápio inicial exigido pelo desafio (X Burger, Batata Frita, etc.).

## 🏗️ Decisões de Arquitetura

O projeto foi estruturado utilizando conceitos de **Clean Architecture** combinados com **Vertical Slice Architecture** na camada de Aplicação.

* **Domain-Driven Design (DDD):** As regras de negócio mais críticas (como a restrição de apenas um item por categoria e o cálculo progressivo de descontos) foram encapsuladas dentro da Raiz de Agregação `Order`. Isso impede que a entidade assuma um estado inválido e centraliza as regras financeiras, facilitando muito os testes de unidade.
* **CQRS com MediatR:** A camada de Application foi separada em *Commands* (operações de escrita que alteram o estado) e *Queries* (operações de leitura). Isso isola os casos de uso em *Handlers* específicos, garantindo que cada classe tenha apenas uma única responsabilidade (SRP).
* **Tratamento de Exceções Global:** Utilização da interface `IExceptionHandler` do ASP.NET Core para interceptar `DomainExceptions` e retornar *Bad Requests* (HTTP 400) claros e padronizados quando uma regra de negócio é violada (ex: tentativa de adicionar itens duplicados).

## 🚧 O Que Deixei de Fora (Trade-offs)

Para focar na entrega de valor e nas regras de negócio dentro do prazo sugerido, algumas abordagens comuns em ambientes de produção corporativos foram simplificadas:

* **Docker & Contêineres:** Optei por não incluir um `docker-compose` com um banco robusto (como PostgreSQL ou SQL Server) para não adicionar atrito na hora da avaliação local.
* **Autenticação/Autorização:** Não foi implementado um fluxo de JWT ou Identity, pois o foco do desafio é a modelagem do problema e estruturação do código.
* **Migrations Automáticas no Startup:** Em um ambiente de produção real e escalável, rodar `dbContext.Database.Migrate()` no `Program.cs` não é recomendado devido a problemas de concorrência. Aqui, foi utilizado puramente para facilitar a execução local. Em produção, este passo estaria numa esteira de CI/CD.