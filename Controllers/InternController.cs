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

            // Update only provided values, keep existing ones if null
            intern.Firstname = !string.IsNullOrWhiteSpace(internDto.Firstname) ? internDto.Firstname : intern.Firstname;
            intern.Lastname = !string.IsNullOrWhiteSpace(internDto.Lastname) ? internDto.Lastname : intern.Lastname;
            intern.ContactNumber = !string.IsNullOrWhiteSpace(internDto.ContactNumber) ? internDto.ContactNumber : intern.ContactNumber;
            intern.Email = !string.IsNullOrWhiteSpace(internDto.Email) ? internDto.Email : intern.Email;
            intern.Specialization = !string.IsNullOrWhiteSpace(internDto.Specialization) ? internDto.Specialization : intern.Specialization;
            intern.Skills = !string.IsNullOrWhiteSpace(internDto.Skills) ? internDto.Skills : intern.Skills;
            intern.Description = !string.IsNullOrWhiteSpace(internDto.Description) ? internDto.Description : intern.Description;

            // Update Education fields if provided
            intern.School = !string.IsNullOrWhiteSpace(internDto.School) ? internDto.School : intern.School;
            intern.Degree = !string.IsNullOrWhiteSpace(internDto.Degree) ? internDto.Degree : intern.Degree;
            intern.StartDate = internDto.StartDate ?? intern.StartDate;
            intern.EndDate = internDto.EndDate ?? intern.EndDate;

            if (intern.EndDate.HasValue && intern.EndDate < intern.StartDate)
            {
                return BadRequest("End date cannot be before start date.");
            }

            // Update Work Experience fields if provided
            intern.Company = !string.IsNullOrWhiteSpace(internDto.Company) ? internDto.Company : intern.Company;
            intern.CompanyLocation = !string.IsNullOrWhiteSpace(internDto.CompanyLocation) ? internDto.CompanyLocation : intern.CompanyLocation;
            intern.Position = !string.IsNullOrWhiteSpace(internDto.Position) ? internDto.Position : intern.Position;
            intern.StartWorkDate = internDto.StartWorkDate ?? intern.StartWorkDate;
            intern.EndWorkDate = internDto.EndWorkDate ?? intern.EndWorkDate;

            if (intern.EndWorkDate.HasValue && intern.EndWorkDate < intern.StartWorkDate)
            {
                return BadRequest("End work date cannot be before start work date.");
            }

            // Update Field (One-to-One)
            if (internDto.FieldId.HasValue)
            {
                var field = await _context.Fields.FindAsync(internDto.FieldId.Value);
                if (field == null)
                {
                    return BadRequest("Invalid FieldId provided.");
                }

                // Check if another intern is already assigned to this field
                var existingInternWithField = await _context.Interns.FirstOrDefaultAsync(i => i.FieldId == internDto.FieldId.Value);
                if (existingInternWithField != null && existingInternWithField.Id != intern.Id)
                {
                    return BadRequest("This Field is already assigned to another Intern.");
                }

                intern.FieldId = internDto.FieldId.Value;
                intern.Field = field;
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
