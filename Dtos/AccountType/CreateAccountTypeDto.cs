using System.ComponentModel.DataAnnotations;

namespace api.Dtos.AccountType
{
    public class CreateAccountTypeDto
    {
        [Required, MaxLength(50)]
        public required string TypeName { get; set; }
    }
}
