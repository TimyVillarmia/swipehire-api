using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Dtos.Recruit;
using api.Entities;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecruitController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RecruitController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Recruit
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecruitDto>>> GetRecruits()
        {
            var recruits = await _context.Recruits
                .Select(r => new RecruitDto
                {
                    Id = r.Id,
                    Firstname = r.Firstname,
                    Lastname = r.Lastname,
                    Position = r.Position,
                    Company = r.Company,
                    Field = r.Field,
                    Address = r.Address,
                    PhoneNumber = r.PhoneNumber,
                    AccountId = r.AccountId
                })
                .ToListAsync();

            return Ok(recruits);
        }

        // GET: api/Recruit/account/{accountId}
        [HttpGet("account/{accountId}")]
        public async Task<ActionResult<RecruitDto>> GetRecruitByAccountId(int accountId)
        {
            var recruit = await _context.Recruits
                .FirstOrDefaultAsync(r => r.AccountId == accountId);

            if (recruit == null)
            {
                return NotFound("Recruit not found for the given AccountId.");
            }

            return Ok(new RecruitDto
            {
                Id = recruit.Id,
                Firstname = recruit.Firstname,
                Lastname = recruit.Lastname,
                Position = recruit.Position,
                Company = recruit.Company,
                Field = recruit.Field,
                Address = recruit.Address,
                PhoneNumber = recruit.PhoneNumber,
                AccountId = recruit.AccountId
            });
        }


        // POST: api/Recruit
        [HttpPost]
        public async Task<ActionResult<RecruitDto>> CreateRecruit(RecruitDto recruitDto)
        {
            // Fetch the Account from the database using the AccountId
            var account = await _context.Accounts.FindAsync(recruitDto.AccountId);
            if (account == null)
            {
                return NotFound("Account not found.");
            }
            var recruit = new Recruit
            {
                Firstname = recruitDto.Firstname,
                Lastname = recruitDto.Lastname,
                Position = recruitDto.Position,
                Company = recruitDto.Company,
                Field = recruitDto.Field,
                Address = recruitDto.Address,
                PhoneNumber = recruitDto.PhoneNumber,
                AccountId = recruitDto.AccountId,
                Account = account // Set navigation property
            };

            _context.Recruits.Add(recruit);
            await _context.SaveChangesAsync();

            recruitDto.Id = recruit.Id;
            return CreatedAtAction(nameof(GetRecruitByAccountId), new { id = recruit.Id }, recruitDto);
        }

        // PUT: api/Recruit/ByAccount/{accountId}
        [HttpPut("ByAccount/{accountId}")]
        public async Task<IActionResult> UpdateRecruitByAccountId(int accountId, RecruitDto recruitDto)
        {
            var recruit = await _context.Recruits
                .FirstOrDefaultAsync(r => r.AccountId == accountId);

            if (recruit == null)
            {
                return NotFound($"No recruit found for AccountId {accountId}.");
            }

            // ✅ Update only provided fields while keeping existing values
            recruit.Firstname = !string.IsNullOrEmpty(recruitDto.Firstname) ? recruitDto.Firstname : recruit.Firstname;
            recruit.Lastname = !string.IsNullOrEmpty(recruitDto.Lastname) ? recruitDto.Lastname : recruit.Lastname;
            recruit.Position = !string.IsNullOrEmpty(recruitDto.Position) ? recruitDto.Position : recruit.Position;
            recruit.Company = !string.IsNullOrEmpty(recruitDto.Company) ? recruitDto.Company : recruit.Company;
            recruit.Field = !string.IsNullOrEmpty(recruitDto.Field) ? recruitDto.Field : recruit.Field;
            recruit.Address = !string.IsNullOrEmpty(recruitDto.Address) ? recruitDto.Address : recruit.Address;
            recruit.PhoneNumber = !string.IsNullOrEmpty(recruitDto.PhoneNumber) ? recruitDto.PhoneNumber : recruit.PhoneNumber;

            await _context.SaveChangesAsync();
            return NoContent();
        }


        // DELETE: api/Recruit/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecruit(int id)
        {
            var recruit = await _context.Recruits.FindAsync(id);
            if (recruit == null)
            {
                return NotFound();
            }

            _context.Recruits.Remove(recruit);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
