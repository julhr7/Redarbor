using MediatR;
using Microsoft.EntityFrameworkCore;
using Redarbor.Domain;
using Redarbor.Application.Common.Interfaces;

namespace Redarbor.Application.Employees.Queries;

public record GetEmployeesQuery() : IRequest<List<Employee>>;

public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, List<Employee>>
{
    private readonly IRedarborDbContext _context;

    public GetEmployeesQueryHandler(IRedarborDbContext context)
    {
        _context = context;
    }

    public async Task<List<Employee>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Employees
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}