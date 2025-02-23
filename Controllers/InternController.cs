using api.Data;
using api.Dtos.Intern;
using api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InternController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InternController(AppDbContext context)
        {
            _context = context;
        }

        // CREATE: api/intern (POST)
        [HttpPost]
        public async Task<IActionResult> CreateIntern([FromBody] InternDto internDto)
        {
            if (internDto == null)
            {
                return BadRequest("Invalid intern data.");
            }

            var account = await _context.Accounts.FindAsync(internDto.AccountId);
            if (account == null)
            {
                return NotFound("Account not found.");
            }

            var intern = new Intern
            {
                Firstname = internDto.Firstname,
                Lastname = internDto.Lastname,
                ContactNumber = internDto.ContactNumber,
                Email = internDto.Email,
                Specialization = internDto.Specialization,
                Skills = internDto.Skills,
                Description = internDto.Description,
                AccountId = internDto.AccountId,
                Account = account,
                HasProfile = false
            };

            _context.Interns.Add(intern);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInternById), new { id = intern.Id }, intern);
        }

        // READ (Single): api/intern/{id} (GET)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInternById(int id)
        {
            var intern = await _context.Interns
                .Include(i => i.Account)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (intern == null)
            {
                return NotFound();
            }

            return Ok(intern);
        }

        // READ (All): api/intern (GET)
        [HttpGet]
        public async Task<IActionResult> GetAllInterns()
        {
            var interns = await _context.Interns
                .Include(i => i.Account)
                .ToListAsync();

            return Ok(interns);
        }

        // UPDATE: api/intern/{id} (PUT)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIntern(int id, [FromBody] InternDto internDto)
        {
            if (internDto == null)
            {
                return BadRequest("Invalid intern data.");
            }

            var intern = await _context.Interns.FindAsync(id);
            if (intern == null)
            {
                return NotFound("Intern not found.");
            }

            intern.Firstname = internDto.Firstname;
            intern.Lastname = internDto.Lastname;
            intern.ContactNumber = internDto.ContactNumber;
            intern.Email = internDto.Email;
            intern.Specialization = internDto.Specialization;
            intern.Skills = internDto.Skills;
            intern.Description = internDto.Description;
            
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/intern/{id} (DELETE)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIntern(int id)
        {
            var intern = await _context.Interns.FindAsync(id);
            if (intern == null)
            {
                return NotFound("Intern not found.");
            }

            _context.Interns.Remove(intern);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
