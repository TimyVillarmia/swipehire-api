using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entities
{
    public class AccountType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public required string TypeName { get; set; }

        // ✅ One-to-Many Navigation Property (One AccountType can have many Accounts)
        public List<Account> Accounts { get; set; } = new List<Account>();
    }
}
