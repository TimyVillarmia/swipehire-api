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

            // Validate AccountId
            var account = await _context.Accounts.FindAsync(internDto.AccountId);
            if (account == null)
            {
                return NotFound("Account not found.");
            }

            // Validate FieldId if provided
            Field? field = null;
            if (internDto.FieldId.HasValue)
            {
                field = await _context.Fields.FindAsync(internDto.FieldId.Value);
                if (field == null)
                {
                    return BadRequest("Invalid FieldId provided.");
                }

                var existingIntern = await _context.Interns.FirstOrDefaultAsync(i => i.FieldId == internDto.FieldId.Value);
                if (existingIntern != null)
                {
                    return BadRequest("This Field is already assigned to another Intern.");
                }
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
                FieldId = internDto.FieldId,
                Field = field,
                HasProfile = false, // Default to false

                // New Education Fields
                School = internDto.School,
                Degree = internDto.Degree,
                StartDate = internDto.StartDate,
                EndDate = internDto.EndDate,

                // New Work Experience Fields
                Company = internDto.Company,
                CompanyLocation = internDto.CompanyLocation,
                Position = internDto.Position,
                StartWorkDate = internDto.StartWorkDate,
                EndWorkDate = internDto.EndWorkDate
            };

            _context.Interns.Add(intern);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInternByAccountId), new { accountId = intern.AccountId }, intern);
        }





        // READ (Single) by AccountId: api/intern/account/{accountId} (GET)
        [HttpGet("account/{accountId}")]
        public async Task<IActionResult> GetInternByAccountId(int accountId)
        {
            var intern = await _context.Interns
                .Include(i => i.Account)
                .Include(i => i.Field)
                .FirstOrDefaultAsync(i => i.AccountId == accountId);
            if (intern == null)
            {
                return NotFound("No intern found with this AccountId.");
            }

            return Ok(intern);
        }


        // READ (All): api/intern (GET)
        [HttpGet]
        public async Task<IActionResult> GetAllInterns()
        {
            var interns = await _context.Interns
                .Include(i => i.Account)
                .Include(i => i.Field)
                .ToListAsync();

            return Ok(interns);
        }

        [HttpPut("account/{accountId}")]
        public async Task<IActionResult> UpdateInternByAccountId(int accountId, [FromBody] InternDto internDto)
        {
            if (internDto == null)
            {
                return BadRequest("Invalid intern data.");
            }

            var intern = await _context.Interns
                .Include(i => i.Field)
                .FirstOrDefaultAsync(i => i.AccountId == accountId);

            if (intern == null)
            {
                return NotFound("Intern not found for the given AccountId.");
            }

            // Update intern details
            intern.Firstname = internDto.Firstname;
            intern.Lastname = internDto.Lastname;
            intern.ContactNumber = internDto.ContactNumber;
            intern.Email = internDto.Email;
            intern.Specialization = internDto.Specialization;
            intern.Skills = internDto.Skills;
            intern.Description = internDto.Description;

            // Update Education Fields
            intern.School = internDto.School;
            intern.Degree = internDto.Degree;
            intern.StartDate = internDto.StartDate;
            intern.EndDate = internDto.EndDate;

            // Validate that EndDate is not before StartDate
            if (intern.EndDate.HasValue && intern.EndDate < intern.StartDate)
            {
                return BadRequest("End date cannot be before start date.");
            }

            // Update Work Experience Fields
            intern.Company = internDto.Company;
            intern.CompanyLocation = internDto.CompanyLocation;
            intern.Position = internDto.Position;
            intern.StartWorkDate = internDto.StartWorkDate;
            intern.EndWorkDate = internDto.EndWorkDate;

            // Validate that EndWorkDate is not before StartWorkDate
            if (intern.EndWorkDate.HasValue && intern.EndWorkDate < intern.StartWorkDate)
            {
                return BadRequest("End work date cannot be before start work date.");
            }

            // Update Field (if applicable)
            if (internDto.FieldId.HasValue)
            {
                var field = await _context.Fields.FindAsync(internDto.FieldId.Value);
                if (field == null)
                {
                    return BadRequest("Invalid FieldId provided.");
                }

                var existingInternWithField = await _context.Interns.FirstOrDefaultAsync(i => i.FieldId == internDto.FieldId.Value);
                if (existingInternWithField != null && existingInternWithField.Id != intern.Id)
                {
                    return BadRequest("This Field is already assigned to another Intern.");
                }

                intern.FieldId = internDto.FieldId.Value;
                intern.Field = field;
            }
            else
            {
                intern.FieldId = null;
                intern.Field = null;
            }

            await _context.SaveChangesAsync();
            return Ok(intern);
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
