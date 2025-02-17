using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Intern
{
    public class UpdateInternDto
    {
        [Required, MaxLength(100)]
        public required string Firstname { get; set; }

        [Required, MaxLength(100)]
        public required string Lastname { get; set; }

        [Required]
        public required long ContactNumber { get; set; }

        [Required, MaxLength(255), EmailAddress]
        public required string Email { get; set; }

        [Required, MaxLength(100)]
        public required string Specialization { get; set; }

        [Required, MaxLength(100)]
        public required string Field { get; set; }

        public string? Description { get; set; }
        public bool HasProfile { get; set; }
    }
}
