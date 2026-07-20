using Dapper;
using MediatR;
using Redarbor.Application.Common.Interfaces;

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
    private readonly IDbConnectionFactory _connectionFactory;

    public UpdateEmployeeCommandHandler(IDbConnectionFactory connectionFactory)
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