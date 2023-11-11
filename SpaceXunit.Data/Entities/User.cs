namespace SpaceXunit.Data.Entities;

public class User : BaseEntity
{
    [Required]
    public int LoginType { get; set; } = 1; // 1: Space, 2: Google, 3: Github

    [Required]
    [StringLength(50)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [StringLength(25)]
    public string? MiddleName { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    public int? Gender { get; set; }

    public DateOnly? BirthDate { get; set; }

    [StringLength(2)]
    public char? CountryCode { get; set; }

    [Required]
    public DateTime RegisterDate { get; set; } = DateTime.UtcNow;

    [StringLength(50)]
    public string RegisterIpAddress { get; set; } = string.Empty;

    [Required]
    public int LoginCount { get; set; } = 0;

    [Required]
    public int Status { get; set; }

    [Required]
    public bool IsAdmin { get; set; } = false;
}
