
public class CreateAccountDto
{
    public IFormFile? InternPicture { get; set; }
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int AccountTypeId { get; set; }
}
