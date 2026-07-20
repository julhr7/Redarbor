using MediatR;
using Microsoft.EntityFrameworkCore;
using Redarbor.Domain;
using Redarbor.Application.Common.Interfaces;

namespace Redarbor.Application.Employees.Queries;

public record GetEmployeeByIdQuery(int Id) : IRequest<Employee?>;

public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Employee?>
{
    private readonly IRedarborDbContext _context;

    public GetEmployeeByIdQueryHandler(IRedarborDbContext context)
    {
        _context = context;
    }

    public async Task<Employee?> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
    }
}