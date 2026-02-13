# ECommercePedidos

Sistema de processamento de pedidos desenvolvido com .NET 8 utilizando arquitetura limpa e processamento assíncrono com RabbitMQ.

---

## Arquitetura

O projeto foi estruturado utilizando princípios de Clean Architecture:

- **ECommercePedidos.Domain** → Entidades e regras de negócio
- **ECommercePedidos.Application** → Casos de uso e contratos
- **ECommercePedidos.Infrastructure** → Implementações técnicas (EF Core, RabbitMQ, MongoDB)
- **ECommercePedidos.Api** → API REST
- **ECommercePedidos.Worker** → Processamento assíncrono em segundo plano

---

## Fluxo do Sistema

1. A API recebe um pedido de compra.
2. O pedido é salvo no SQL Server com status inicial **"Recebido"**.
3. Um evento é publicado no RabbitMQ.
4. O Worker consome o evento.
5. O pedido é processado:
   - Status alterado para **"Processando"**
   - Status alterado para **"Concluído"**
---

## Tecnologias Utilizadas

- .NET 8
- ASP.NET Core
- Entity Framework Core
- SQL Server
- RabbitMQ
- MongoDB
- Clean Architecture

---

# Como Executar o Projeto

---

## Aplique a Migration

```dotnet ef database update --project ECommercePedidos.Infrastructure --startup-project ECommercePedidos.Api```

## Rode os Conatiners

```docker-compose up -d```

## Rodar a API

Abrir a solution `ECommercePedidos.sln`

Configurar múltiplos projetos de inicialização:

ECommercePedidos.Api → Start

ECommercePedidos.Worker → Start

Executar
