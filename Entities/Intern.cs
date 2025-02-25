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

    // Foreign Key for Account (Many-to-One)
    [Required]
    [ForeignKey("AccountId")]
    public int AccountId { get; set; }

    [JsonIgnore]
    [ForeignKey("AccountId")]
    public Account Account { get; set; } = null!;

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

    public string? Skills {get;set;}

    // ✅ Foreign Key for Field (Many-to-One)
    [ForeignKey("FieldId")]
    public int? FieldId { get; set; }

    public Field? Field { get; set; } // ✅ Navigation property

    public string? Description { get; set; }
    // Nullable Foreign Key for Education
    public string School { get; set; } = string.Empty;
    public string Degree { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    [Required]
    [MaxLength(200)]
    public string Company { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? CompanyLocation { get; set; }

    [Required]
    [MaxLength(150)]
    public string Position { get; set; } = string.Empty;

    [Required]
    public DateTime? StartWorkDate { get; set; }

    public DateTime? EndWorkDate { get; set; }


    [DefaultValue(false)]
    public bool HasProfile { get; set; }
}
