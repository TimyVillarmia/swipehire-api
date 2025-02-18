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

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ GET: api/account
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
                    Interns = a.Interns.Select(i => new InternDto
                    {
                        Id = i.Id,
                        Firstname = i.Firstname,
                        Lastname = i.Lastname,
                        Email = i.Email,
                        ContactNumber = i.ContactNumber
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

        // ✅ GET: api/account/{id}
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
                    Interns = a.Interns.Select(i => new InternDto
                    {
                        Id = i.Id,
                        Firstname = i.Firstname,
                        Lastname = i.Lastname,
                        Email = i.Email,
                        ContactNumber = i.ContactNumber
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

        // ✅ POST: api/account (Registration)
        [HttpPost]
        public async Task<ActionResult<AccountDto>> CreateAccount(CreateAccountDto dto)
        {
            var accountType = await _context.AccountTypes.FindAsync(dto.AccountTypeId);
            if (accountType == null) return BadRequest("Invalid AccountTypeId");

            var hashedPassword = HashPassword(dto.Password);

            var account = new Account
            {
                Firstname = dto.Firstname,
                Lastname = dto.Lastname,
                Email = dto.Email,
                Password = hashedPassword,
                AccountTypeId = dto.AccountTypeId,
                AccountType = accountType
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
                AccountType = new AccountTypeDto
                {
                    Id = account.AccountType.Id,
                    TypeName = account.AccountType.TypeName
                }
            };

            return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, accountDto);
        }

        // ✅ PUT: api/account/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, UpdateAccountDto dto)
        {
            var account = await _context.Accounts
                .Include(a => a.AccountType)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (account == null) return NotFound();

            var accountType = await _context.AccountTypes.FindAsync(dto.AccountTypeId);
            if (accountType == null) return BadRequest("Invalid AccountTypeId");

            account.Firstname = dto.Firstname;
            account.Lastname = dto.Lastname;
            account.Email = dto.Email;
            if (!string.IsNullOrEmpty(dto.Password))
            {
                account.Password = HashPassword(dto.Password);
            }
            account.AccountTypeId = dto.AccountTypeId;

            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

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
