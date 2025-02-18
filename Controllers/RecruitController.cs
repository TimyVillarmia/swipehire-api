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

        // GET: api/Recruit/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RecruitDto>> GetRecruit(int id)
        {
            var recruit = await _context.Recruits.FindAsync(id);

            if (recruit == null)
            {
                return NotFound();
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
            return CreatedAtAction(nameof(GetRecruit), new { id = recruit.Id }, recruitDto);
        }

        // PUT: api/Recruit/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecruit(int id, RecruitDto recruitDto)
        {
            if (id != recruitDto.Id)
            {
                return BadRequest();
            }

            var recruit = await _context.Recruits.FindAsync(id);
            if (recruit == null)
            {
                return NotFound();
            }

            recruit.Firstname = recruitDto.Firstname;
            recruit.Lastname = recruitDto.Lastname;
            recruit.Position = recruitDto.Position;
            recruit.Company = recruitDto.Company;
            recruit.Field = recruitDto.Field;
            recruit.Address = recruitDto.Address;
            recruit.PhoneNumber = recruitDto.PhoneNumber;
            recruit.AccountId = recruitDto.AccountId;

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
