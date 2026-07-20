namespace Redarbor.Domain;

public class Employee
{
    public int Id { get; set; }
    
    // Campos Obligatorios (NOT NULL)
    public int CompanyId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int PortalId { get; set; }
    public int RoleId { get; set; }
    public int StatusId { get; set; }
    public string Username { get; set; } = string.Empty;

    // Campos Opcionales / Auditoría
    public string? Name { get; set; }
    public string? Telephone { get; set; }
    public string? Fax { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public DateTime? Lastlogin { get; set; }
}