using System.ComponentModel.DataAnnotations;

namespace api.Dtos;

public class CreateInternDto
{
    [Required]
    [MaxLength(100)]
    public required string Firstname { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Lastname { get; set; }

    [Required]
    [Phone]
    [MaxLength(15)]
    public required string ContactNumber { get; set; }

    [Required]
    [MaxLength(255)]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Specialization { get; set; }

    public string? Description { get; set; }

    [Required]
    public int AccountId { get; set; }
}
