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
        public string? Skills { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool HasProfile { get; set; }
        public int AccountId { get; set; } // Foreign key reference
        public int? FieldId { get; set; }

        // Education Details
        public string School { get; set; } = string.Empty;
        public string Degree { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // Work Experience Details
        public string Company { get; set; } = string.Empty;
        public string? CompanyLocation { get; set; }
        public string Position { get; set; } = string.Empty;
        public DateTime? StartWorkDate { get; set; }
        public DateTime? EndWorkDate { get; set; }
    }
}
