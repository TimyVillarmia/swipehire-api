using api.Dtos.AccountType;
using api.Dtos.Intern;
using api.Dtos.Recruit;
using api.Entities;

namespace api.Dtos.Account
{
    public class AccountDto
    {
        public int Id { get; set; }
        public IFormFile? InternPicture { get; set; }
        // ✅ Return the full image URL in API responses
        public string? InternPictureUrl { get; set; }   
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int AccountTypeId { get; set; }
        public DateTime CreatedAt { get; internal set; }
        
        // Include related entities
        public List<InternDto>? Interns { get; set; }
        public List<RecruitDto>? Recruits { get; set; }

        // ✅ Make AccountType nullable for flexibility
        public AccountTypeDto? AccountType { get; set; } 
    }
}
