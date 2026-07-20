using FluentValidation;
using Redarbor.Domain;

namespace Redarbor.Application.Employees;

public class EmployeeValidator : AbstractValidator<Employee>
{
    public EmployeeValidator()
    {
        // Validaciones para campos obligatorios requeridos por la prueba
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("CompanyId es obligatorio y debe ser mayor a 0.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El Email es obligatorio.")
            .EmailAddress().WithMessage("El formato del Email no es válido.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("El Password es obligatorio.");

        RuleFor(x => x.PortalId)
            .GreaterThan(0).WithMessage("PortalId es obligatorio y debe ser mayor a 0.");

        RuleFor(x => x.RoleId)
            .GreaterThan(0).WithMessage("RoleId es obligatorio y debe ser mayor a 0.");

        RuleFor(x => x.StatusId)
            .GreaterThan(0).WithMessage("StatusId es obligatorio y debe ser mayor a 0.");

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("El Username es obligatorio.");
    }
}