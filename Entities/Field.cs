using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entities;

public class Field
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    // ✅ Many-to-Many Relationship: Field ↔ Intern
    public List<Intern> Interns { get; set; } = new List<Intern>();
}
