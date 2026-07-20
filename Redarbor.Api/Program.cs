using System.Reflection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Redarbor.Application.Employees;
using Redarbor.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// 1. Agregar servicios al contenedor DI
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. Registrar DbContext para lecturas con EF Core
builder.Services.AddDbContext<RedarborDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") 
        ?? "Server=(localdb)\\mssqllocaldb;Database=RedarborDb;Trusted_Connection=True;MultipleActiveResultSets=true"));

// 3. Registrar DbConnectionFactory para escrituras con Dapper
builder.Services.AddSingleton<DbConnectionFactory>();

// 4. Registrar MediatR desde la capa de Application
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(EmployeeValidator).Assembly));

// 5. Registrar Validadores de FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<EmployeeValidator>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "RedarborIssuer",
            ValidAudience = "RedarborAudience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperSecretKey_Redarbor_2026_SecureKey"))
        };
    });

var app = builder.Build();

// Configuración de la canalización HTTP (Middleware)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Asegurar creación de la Base de Datos al iniciar
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<RedarborDbContext>();
    dbContext.Database.EnsureCreated();
}

app.Run();