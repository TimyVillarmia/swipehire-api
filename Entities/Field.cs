using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api.Entities;

public class Field
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    // ✅ Many-to-Many Relationship: Field ↔ Intern
    [JsonIgnore]
    public ICollection<Intern> Interns { get; set; } = new List<Intern>();
}
