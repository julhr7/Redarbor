using MediatR;
using Microsoft.EntityFrameworkCore;
using Redarbor.Domain;
using Redarbor.Infrastructure.Persistence; // Acceso al DbContext para lectura

namespace Redarbor.Application.Employees.Queries;

// Request del Query
public record GetEmployeesQuery() : IRequest<List<Employee>>;

// Handler que procesa la consulta usando EF Core
public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, List<Employee>>
{
    private readonly RedarborDbContext _context;

    public GetEmployeesQueryHandler(RedarborDbContext context)
    {
        _context = context;
    }

    public async Task<List<Employee>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        // AsNoTracking() optimiza el rendimiento desactivando el seguimiento de cambios
        return await _context.Employees
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}