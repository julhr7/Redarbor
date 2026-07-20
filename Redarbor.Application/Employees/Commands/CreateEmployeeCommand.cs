using Dapper;
using MediatR;
using Redarbor.Domain;
using Redarbor.Application.Common.Interfaces;

namespace Redarbor.Application.Employees.Commands;

public record CreateEmployeeCommand(
    int CompanyId,
    string Email,
    string Password,
    int PortalId,
    int RoleId,
    int StatusId,
    string Username,
    string? Name,
    string? Telephone,
    string? Fax,
    DateTime? CreatedOn,
    DateTime? Lastlogin,
    DateTime? UpdatedOn,
    DateTime? DeletedOn
) : IRequest<Employee>;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Employee>
{
    private readonly IDbConnectionFactory _connectionFactory;

    public CreateEmployeeCommandHandler(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Employee> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        const string sql = @"
            INSERT INTO Employees (CompanyId, Email, Password, PortalId, RoleId, StatusId, Username, Name, Telephone, Fax, CreatedOn, Lastlogin, UpdatedOn, DeletedOn)
            OUTPUT INSERTED.Id
            VALUES (@CompanyId, @Email, @Password, @PortalId, @RoleId, @StatusId, @Username, @Name, @Telephone, @Fax, @CreatedOn, @Lastlogin, @UpdatedOn, @DeletedOn);";

        using var connection = _connectionFactory.CreateConnection();
        
        var newId = await connection.ExecuteScalarAsync<int>(sql, request);

        return new Employee
        {
            Id = newId,
            CompanyId = request.CompanyId,
            Email = request.Email,
            Password = request.Password,
            PortalId = request.PortalId,
            RoleId = request.RoleId,
            StatusId = request.StatusId,
            Username = request.Username,
            Name = request.Name,
            Telephone = request.Telephone,
            Fax = request.Fax,
            CreatedOn = request.CreatedOn,
            Lastlogin = request.Lastlogin,
            UpdatedOn = request.UpdatedOn,
            DeletedOn = request.DeletedOn
        };
    }
}