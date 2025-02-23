using api.Entities;

namespace api.Dtos.Intern
{
    public class InternDto
    {
        public int Id { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public required string ContactNumber { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string? Skills {get;set;} = string.Empty;
        public string Field { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool HasProfile { get; set; }
        public int AccountId { get; set; } // Foreign key reference
    }
}
