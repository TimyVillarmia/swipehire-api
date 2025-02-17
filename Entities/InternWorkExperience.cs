using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entities;

public class InternWorkExperience
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Company { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? CompanyLocation { get; set; }

    [Required]
    [MaxLength(150)]
    public string Position { get; set; } = string.Empty;

    [Required]
    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    // Foreign Key for Intern (Many-to-One Relationship)
    [Required]
    public int InternId { get; set; }

    public required Intern Intern { get; set; } // Navigation property
}
