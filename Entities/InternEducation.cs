using System;
using System.ComponentModel.DataAnnotations;

namespace api.Entities;

public class InternEducation
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string School { get; set; } = string.Empty;  // School name (required, max length 200)

    [Required]
    [MaxLength(150)]
    public string Degree { get; set; } = string.Empty;  // Degree (required, max length 150)

    [Required]
    public DateTime StartDate { get; set; }  // Start date (required)

    public DateTime? EndDate { get; set; }  // End date (nullable, if ongoing)
    public List<Intern> Interns { get; set; } = new();
}
