// using System.ComponentModel.DataAnnotations;

// namespace api.Dtos.Account
// {
//     public class CreateAccountDto
//     {
//         public List<int>? AccountTypeIds { get; set; } // Optional for updates

//         [Required, MaxLength(100)]
//         public required string Firstname { get; set; }

//         [Required, MaxLength(100)]
//         public required string Lastname { get; set; }

//         [Required, MaxLength(255), EmailAddress]
//         public required string Email { get; set; }

//         public string? Password { get; set; } // Optional for updates
//     }
// }
public class CreateAccountDto
{
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int AccountTypeId { get; set; }
}
