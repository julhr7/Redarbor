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
```

### 🛠️ Tecnologías y Decisiones Técnicas

* **.NET 6.0 WebAPI:** Framework base de alto rendimiento.
* **CQRS + MediatR:** Separación limpia de operaciones de lectura (*Queries*) y escritura (*Commands*).
* **Persistencia Híbrida:**
  * **Entity Framework Core (Queries):** Consultas LINQ optimizadas mediante `.AsNoTracking()`.
  * **Dapper (Commands):** Ejecución directa y ultrarrápida de consultas SQL (`INSERT`, `UPDATE`, `DELETE`).
* **FluentValidation:** Validación de reglas de negocio y campos requeridos (`Email`, `Password`, `CompanyId`, `PortalId`, `RoleId`, `StatusId`, `Username`) antes de la persistencia.
* **Testing:** Cobertura de pruebas unitarias para validadores y casos de uso con **xUnit** y **Moq**.
* **Docker:** Multi-stage `Dockerfile` para empaquetamiento y ejecución en contenedores.

---

## 📋 Endpoints de la API

**Base Path:** `/api/redarbor`

| Método | Endpoint | Descripción | Body Request | Respuesta |
| :--- | :--- | :--- | :--- | :--- |
| **GET** | `/api/redarbor` | Obtener todos los empleados | Ninguno | 200 OK (Array de Employee) |
| **GET** | `/api/redarbor/{id}` | Obtener empleado por ID | Ninguno | 200 OK / 404 Not Found |
| **POST** | `/api/redarbor` | Insertar un nuevo empleado | Objeto Employee | 201 Created (Empleado con ID) |
| **PUT** | `/api/redarbor/{id}` | Actualizar un empleado existente | Objeto Employee | 204 No Content / 404 Not Found |
| **DELETE** | `/api/redarbor/{id}` | Eliminar un empleado | Ninguno | 204 No Content / 404 Not Found |

---

## ⚙️ Requisitos Previos

* .NET 6.0 SDK
* SQL Server Express / LocalDB o Docker Desktop

---

## 🚀 Instalación y Ejecución Local

**1. Clonar el repositorio**
```bash
git clone [https://github.com/tu-usuario/redarbor-backend.git](https://github.com/tu-usuario/redarbor-backend.git)
cd redarbor-backend
```

**2. Restaurar dependencias y compilar**
```bash
dotnet restore
dotnet build
```

**3. Ejecutar la WebAPI**
```bash
dotnet run --project Redarbor.Api
```

Una vez iniciada, puedes acceder a la interfaz interactiva de Swagger UI en:
`https://localhost:<PUERTO>/swagger`

---

## 🧪 Ejecución de Pruebas Unitarias

Para correr el conjunto de pruebas unitarias con xUnit:
```bash
dotnet test
```

---

## 🐳 Ejecución con Docker

**1. Construir la imagen de Docker**
```bash
docker build -t redarbor-api .
```

**2. Ejecutar el contenedor**
```bash
docker run -d -p 8080:80 --name redarbor-app redarbor-api
```

La API estará disponible en `http://localhost:8080/swagger`.

---

## 📝 Ejemplo de Payload (POST / PUT)

```json
{
  "companyId": 1,
  "createdOn": "2000-01-01T00:00:00",
  "deletedOn": "2000-01-01T00:00:00",
  "email": "test1@test.test.tmp",
  "fax": "000.000.000",
  "name": "test1",
  "lastlogin": "2000-01-01T00:00:00",
  "password": "test",
  "portalId": 1,
  "roleId": 1,
  "statusId": 1,
  "telephone": "000.000.000",
  "updatedOn": "2000-01-01T00:00:00",
  "username": "test1"
}
```
