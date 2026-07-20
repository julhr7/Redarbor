using Microsoft.EntityFrameworkCore;
using Redarbor.Domain;

namespace Redarbor.Application.Common.Interfaces;

public interface IRedarborDbContext
{
    DbSet<Employee> Employees { get; }
}