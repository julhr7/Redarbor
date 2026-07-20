using FluentValidation.TestHelper;
using Redarbor.Application.Employees;
using Redarbor.Domain;
using Xunit;

namespace Redarbor.UnitTests.Employees;

public class EmployeeValidatorTests
{
    private readonly EmployeeValidator _validator;

    public EmployeeValidatorTests()
    {
        _validator = new EmployeeValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Required_Fields_Are_Empty_Or_Invalid()
    {
        // Arrange (Preparar un objeto inválido sin campos obligatorios)
        var employee = new Employee
        {
            CompanyId = 0, // Inválido
            Email = "",    // Inválido
            Password = "", // Inválido
            PortalId = 0,  // Inválido
            RoleId = 0,    // Inválido
            StatusId = 0,  // Inválido
            Username = ""  // Inválido
        };

        // Act (Ejecutar la validación)
        var result = _validator.TestValidate(employee);

        // Assert (Verificar que cada regla obligatoria falle)
        result.ShouldHaveValidationErrorFor(x => x.CompanyId);
        result.ShouldHaveValidationErrorFor(x => x.Email);
        result.ShouldHaveValidationErrorFor(x => x.Password);
        result.ShouldHaveValidationErrorFor(x => x.PortalId);
        result.ShouldHaveValidationErrorFor(x => x.RoleId);
        result.ShouldHaveValidationErrorFor(x => x.StatusId);
        result.ShouldHaveValidationErrorFor(x => x.Username);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Employee_Is_Valid()
    {
        // Arrange (Preparar un objeto con todos sus campos obligatorios requeridos por la prueba)
        var employee = new Employee
        {
            CompanyId = 1,
            Email = "test1@test.test.tmp",
            Password = "test",
            PortalId = 1,
            RoleId = 1,
            StatusId = 1,
            Username = "test1"
        };

        // Act
        var result = _validator.TestValidate(employee);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}