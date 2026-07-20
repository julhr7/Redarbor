using Microsoft.EntityFrameworkCore;
using Redarbor.Domain;

namespace Redarbor.Infrastructure.Persistence;

public class RedarborDbContext : DbContext
{
    public RedarborDbContext(DbContextOptions<RedarborDbContext> options) : base(options) { }

    public DbSet<Employee> Employees => Set<Employee>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Mapeo explicito de la entidad Employee
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employees");
            entity.HasKey(e => e.Id);

            // Campos obligatorios requeridos por la prueba
            entity.Property(e => e.CompanyId).IsRequired();
            entity.Property(e => e.Email).IsRequired().HasMaxLength(250);
            entity.Property(e => e.Password).IsRequired().HasMaxLength(250);
            entity.Property(e => e.PortalId).IsRequired();
            entity.Property(e => e.RoleId).IsRequired();
            entity.Property(e => e.StatusId).IsRequired();
            entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
        });
    }
}