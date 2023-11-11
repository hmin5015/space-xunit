namespace SpaceXunit.Data.Entities;

public class BaseEntity
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [JsonIgnore]
    [StringLength(50)]
    public string CreatedBy { get; set; } = "System";

    [Required]
    [JsonIgnore]
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    [JsonIgnore]
    [StringLength(50)]
    public string? UpdatedBy { get; set; }

    [JsonIgnore]
    public DateTime? UpdatedOn { get; set; }
}
