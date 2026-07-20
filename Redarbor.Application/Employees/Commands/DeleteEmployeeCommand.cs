using Dapper;
using MediatR;
using Redarbor.Application.Common.Interfaces;

namespace Redarbor.Application.Employees.Commands;

public record DeleteEmployeeCommand(int Id) : IRequest<bool>;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, bool>
{
    private readonly IDbConnectionFactory _connectionFactory;

    public DeleteEmployeeCommandHandler(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        const string sql = "DELETE FROM Employees WHERE Id = @Id;";

        using var connection = _connectionFactory.CreateConnection();
        var rowsAffected = await connection.ExecuteAsync(sql, new { Id = request.Id });

        return rowsAffected > 0;
    }
}