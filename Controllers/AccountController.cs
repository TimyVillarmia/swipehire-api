using api.Data;
using api.Dtos.Account;
using api.Dtos.AccountType;
using api.Dtos.Intern;
using api.Dtos.Recruit;
using api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using Microsoft.CodeAnalysis.Scripting;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly AzureBlobService _azureBlobService;

        public AccountController(AppDbContext context, AzureBlobService azureBlobService)
        {
            _context = context;
            _azureBlobService = azureBlobService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDto>>> GetAccounts()
        {
            var accounts = await _context.Accounts
                .Include(a => a.AccountType)
                .Include(a => a.Interns)
                .Include(a => a.Recruits)
                .Select(a => new AccountDto
                {
                    Id = a.Id,
                    Firstname = a.Firstname,
                    Lastname = a.Lastname,
                    Email = a.Email,
                    CreatedAt = a.CreatedAt,
                    AccountType = a.AccountType != null
                        ? new AccountTypeDto
                        {
                            Id = a.AccountType.Id,
                            TypeName = a.AccountType.TypeName
                        }
                        : null,
                    InternPictureUrl = !string.IsNullOrEmpty(a.InternPicture) 
                        ? _azureBlobService.GetFileUrl(a.InternPicture) 
                        : null,
                    Interns = a.Interns.Select(i => new InternDto
                    {
                        Id = i.Id,
                        Firstname = i.Firstname,
                        Lastname = i.Lastname,
                        Email = i.Email,
                        ContactNumber = i.ContactNumber,
                        Specialization = i.Specialization,
                        Skills = i.Skills,
                        Description = i.Description,
                        School = i.School,
                        Degree = i.Degree,
                        StartDate = i.StartDate,
                        EndDate = i.EndDate,
                        Company = i.Company,
                        CompanyLocation = i.CompanyLocation,
                        Position = i.Position,
                        StartWorkDate = i.StartWorkDate,
                        EndWorkDate = i.EndWorkDate,
                        HasProfile = i.HasProfile
                    }).ToList(),
                    Recruits = a.Recruits.Select(r => new RecruitDto
                    {
                        Id = r.Id,
                        Firstname = r.Firstname,
                        Lastname = r.Lastname,
                        Position = r.Position,
                        Company = r.Company
                    }).ToList()
                })
                .ToListAsync();

            return Ok(accounts);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDto>> GetAccount(int id)
        {
            var account = await _context.Accounts
                .Include(a => a.AccountType)
                .Include(a => a.Interns)
                .Include(a => a.Recruits)
                .Where(a => a.Id == id)
                .Select(a => new AccountDto
                {
                    Id = a.Id,
                    Firstname = a.Firstname,
                    Lastname = a.Lastname,
                    Email = a.Email,
                    CreatedAt = a.CreatedAt,
                    AccountType = a.AccountType != null
                        ? new AccountTypeDto
                        {
                            Id = a.AccountType.Id,
                            TypeName = a.AccountType.TypeName
                        }
                        : null,
                    InternPictureUrl = !string.IsNullOrEmpty(a.InternPicture) 
                        ? _azureBlobService.GetFileUrl(a.InternPicture) 
                        : null,
                    Interns = a.Interns.Select(i => new InternDto
                    {
                        Id = i.Id,
                        Firstname = i.Firstname,
                        Lastname = i.Lastname,
                        Email = i.Email,
                        ContactNumber = i.ContactNumber,
                        Specialization = i.Specialization,
                        Skills = i.Skills,
                        Description = i.Description,
                        School = i.School,
                        Degree = i.Degree,
                        StartDate = i.StartDate,
                        EndDate = i.EndDate,
                        Company = i.Company,
                        CompanyLocation = i.CompanyLocation,
                        Position = i.Position,
                        StartWorkDate = i.StartWorkDate,
                        EndWorkDate = i.EndWorkDate,
                        HasProfile = i.HasProfile
                    }).ToList(),
                    Recruits = a.Recruits.Select(r => new RecruitDto
                    {
                        Id = r.Id,
                        Firstname = r.Firstname,
                        Lastname = r.Lastname,
                        Position = r.Position,
                        Company = r.Company
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (account == null) return NotFound();

            return Ok(account);
        }



        // ✅ POST: api/account (Registration with Image Upload)
        [HttpPost]
        public async Task<ActionResult<AccountDto>> CreateAccount([FromForm] CreateAccountDto dto)
        {
            var accountType = await _context.AccountTypes.FindAsync(dto.AccountTypeId);
            if (accountType == null) return BadRequest("Invalid AccountTypeId");

            var hashedPassword = HashPassword(dto.Password);

            string fileName = string.Empty;
            if (dto.InternPicture != null && dto.InternPicture.Length > 0)
            {
                // Upload file to Azure Blob Storage
                fileName = await _azureBlobService.UploadFileAsync(dto.InternPicture);
            }

            var account = new Account
            {
                Firstname = dto.Firstname,
                Lastname = dto.Lastname,
                Email = dto.Email,
                Password = hashedPassword,
                AccountTypeId = dto.AccountTypeId,
                AccountType = accountType,
                InternPicture = fileName // Store only the file name in the database
            };

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            var accountDto = new AccountDto
            {
                Id = account.Id,
                Firstname = account.Firstname,
                Lastname = account.Lastname,
                Email = account.Email,
                CreatedAt = account.CreatedAt,
                InternPicture = dto.InternPicture, // Include image in response
                AccountType = new AccountTypeDto
                {
                    Id = account.AccountType.Id,
                    TypeName = account.AccountType.TypeName
                }
            };

            return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, accountDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, [FromForm] UpdateAccountDto dto, IFormFile? internPicture)
        {
            var account = await _context.Accounts
                .Include(a => a.AccountType)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (account == null) return NotFound();

            // ✅ Ensure AccountTypeId exists in the database
            var accountType = await _context.AccountTypes.FindAsync(dto.AccountTypeId);
            if (accountType == null) return BadRequest("Invalid AccountTypeId");

            // ✅ Update account fields only if values are provided
            account.Firstname = !string.IsNullOrEmpty(dto.Firstname) ? dto.Firstname : account.Firstname;
            account.Lastname = !string.IsNullOrEmpty(dto.Lastname) ? dto.Lastname : account.Lastname;
            account.Email = !string.IsNullOrEmpty(dto.Email) ? dto.Email : account.Email;
            account.AccountTypeId = dto.AccountTypeId; // Since it's required, always update it.

            if (!string.IsNullOrEmpty(dto.Password))
            {
                account.Password = HashPassword(dto.Password);
            }

            // ✅ Handle Image Upload
            if (internPicture != null)
            {
                if (!string.IsNullOrEmpty(account.InternPicture))
                {
                    await _azureBlobService.DeleteFileAsync(account.InternPicture);
                }
                var newFileName = await _azureBlobService.UploadFileAsync(internPicture);
                account.InternPicture = newFileName;
            }

            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // [Consumes("multipart/form-data")]
        // [HttpPut("{id}/intern-picture")]
        // public async Task<IActionResult> UpdateInternPicture(int id, [FromForm] IFormFile internPicture)
        // {
        //     var account = await _context.Accounts.FindAsync(id);
        //     if (account == null) return NotFound();

        //     if (internPicture == null || internPicture.Length == 0)
        //     {
        //         return BadRequest("No image file provided.");
        //     }

        //     // ✅ Delete old picture if it exists
        //     if (!string.IsNullOrEmpty(account.InternPicture))
        //     {
        //         await _azureBlobService.DeleteFileAsync(account.InternPicture);
        //     }

        //     // ✅ Upload new picture
        //     var newFileName = await _azureBlobService.UploadFileAsync(internPicture);
        //     account.InternPicture = newFileName;

        //     _context.Accounts.Update(account);
        //     await _context.SaveChangesAsync();

        //     return NoContent();
        // }



        // ✅ DELETE: api/account/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var account = await _context.Accounts
                .Include(a => a.Interns)
                .Include(a => a.Recruits)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (account == null) return NotFound();

            _context.Interns.RemoveRange(account.Interns);
            _context.Recruits.RemoveRange(account.Recruits);
            _context.Accounts.Remove(account);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ✅ Secure Password Hashing
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
