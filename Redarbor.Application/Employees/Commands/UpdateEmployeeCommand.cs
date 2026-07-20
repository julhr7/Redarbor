using Dapper;
using MediatR;
using Redarbor.Infrastructure.Persistence;

namespace Redarbor.Application.Employees.Commands;

public record UpdateEmployeeCommand(
    int Id,
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
) : IRequest<bool>;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, bool>
{
    private readonly DbConnectionFactory _connectionFactory;

    public UpdateEmployeeCommandHandler(DbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<bool> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        const string sql = @"
            UPDATE Employees 
            SET CompanyId = @CompanyId,
                Email = @Email,
                Password = @Password,
                PortalId = @PortalId,
                RoleId = @RoleId,
                StatusId = @StatusId,
                Username = @Username,
                Name = @Name,
                Telephone = @Telephone,
                Fax = @Fax,
                CreatedOn = @CreatedOn,
                Lastlogin = @Lastlogin,
                UpdatedOn = @UpdatedOn,
                DeletedOn = @DeletedOn
            WHERE Id = @Id;";

        using var connection = _connectionFactory.CreateConnection();
        var rowsAffected = await connection.ExecuteAsync(sql, request);

        return rowsAffected > 0;
    }
}