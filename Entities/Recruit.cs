using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entities;

public class Recruit
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
    [MaxLength(100)]
    public required string Position { get; set; }

    [Required]
    [MaxLength(150)]
    public required string Company { get; set; }

    // ✅ Foreign Key for Field (Many-to-One)
    [ForeignKey("Field")]
    public int? FieldId { get; set; }

    // ✅ Navigation Property for Field
    public Field? Field { get; set; }

    [Required]
    [MaxLength(255)]
    public required string Address { get; set; }

    [Required]
    [Phone]
    [MaxLength(15)]
    public required string PhoneNumber { get; set; }

    // Foreign Key for Account (Many-to-One)
    [Required]
    [ForeignKey("Account")]
    public int AccountId { get; set; }

    [Required]
    public required Account Account { get; set; }

    // One-to-Many Relationship with Swipes
    public List<Swipe> Swipes { get; set; } = new List<Swipe>();
}
