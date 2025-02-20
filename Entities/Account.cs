using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entities
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        // ✅ One-to-Many Relationship (Many Accounts belong to one AccountType)
        [Required]
        public int AccountTypeId { get; set; } // Foreign Key

        public required AccountType AccountType { get; set; } // Many-to-One Navigation Property

        public string? InternPicture { get; set; }


        [Required]
        [MaxLength(100)]
        public required string Firstname { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Lastname { get; set; }

        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<Intern> Interns { get; set; } = new List<Intern>();
        public List<Recruit> Recruits { get; set; } = new List<Recruit>();
    }
}
