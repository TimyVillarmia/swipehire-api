using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api.Entities;

public class Intern
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Firstname { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Lastname { get; set; }

    [Required]
    [Phone]
    [MaxLength(15)] // Ensuring phone number is stored as a string
    public required string ContactNumber { get; set; }

    [Required]
    [MaxLength(255)]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Specialization { get; set; }

    // Removed single 'Field' property to avoid confusion
    public List<Field> Fields { get; set; } = new();

    public string? Description { get; set; }

    // Foreign Key for Account (Many-to-One)
    [Required]
    [ForeignKey("AccountId")]
    public int AccountId { get; set; }

    [JsonIgnore]
    [ForeignKey("AccountId")]
    public Account Account { get; set; } = null!;

    // Nullable Foreign Key for Education
    public int? EducationId { get; set; }

    public List<InternEducation> InternEducations { get; set; } = new();

    public List<InternWorkExperience> WorkExperiences { get; set; } = new();

    [DefaultValue(false)]
    public bool HasProfile { get; set; }
}
