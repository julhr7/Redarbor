# 🚀 Redarbor WebAPI - Technical Test

![.NET 6.0](https://img.shields.io/badge/.NET-6.0-512BD4?logo=dotnet)
![Architecture](https://img.shields.io/badge/Architecture-Clean%20Architecture%20%2B%20CQRS-blue)
![Database](https://img.shields.io/badge/Database-SQL%20Server%20%2F%20LocalDB-red?logo=microsoftsqlserver)
![Testing](https://img.shields.io/badge/Testing-xUnit%20%2B%20Moq-green)
![Docker](https://img.shields.io/badge/Docker-Supported-2496ED?logo=docker)

Solución desarrollada para la prueba técnica de **Redarbor**, construida sobre **.NET 6.0** utilizando **Clean Architecture**, **CQRS** (Command Query Responsibility Segregation), persistencia híbrida (**Entity Framework Core** para lecturas y **Dapper** para escrituras), validaciones con **FluentValidation**, pruebas unitarias y soporte para **Docker**.

---

## 🏛️ Arquitectura de la Solución

El proyecto está organizado en 4 capas desacopladas siguiendo los principios de **Clean Architecture** y **SOLID**:

```text
┌─────────────────────────────────────────────────────────┐
│                    Redarbor.Api                         │
│   (Controllers REST, OpenAPI/Swagger, Program Config)   │
└───────────────────────────┬─────────────────────────────┘
                            │
                            ▼
┌─────────────────────────────────────────────────────────┐
│                Redarbor.Infrastructure                  │
│    (EF Core DbContext, Dapper Connection Factory)       │
└───────────────────────────┬─────────────────────────────┘
                            │
                            ▼
┌─────────────────────────────────────────────────────────┐
│                 Redarbor.Application                    │
│   (CQRS Handlers, MediatR, FluentValidation Rules)      │
└───────────────────────────┬─────────────────────────────┘
                            │
                            ▼
┌─────────────────────────────────────────────────────────┐
│                  Redarbor.Domain                        │
│             (Entidad Employee e Interfaces)             │
└─────────────────────────────────────────────────────────┘
