using Microsoft.AspNetCore.Mvc;
using api.Dtos;
using api.Entities;
using api.Data;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternWorkExperienceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InternWorkExperienceController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/InternWorkExperience
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InternWorkExperienceDto>>> GetInternWorkExperiences()
        {
            var internWorkExperiences = await _context.InternWorkExperiences
                .Include(ie => ie.Intern) // Include the related intern (if needed)
                .Select(iwe => new InternWorkExperienceDto
                {
                    Id = iwe.Id,
                    Company = iwe.Company,
                    CompanyLocation = iwe.CompanyLocation,
                    Position = iwe.Position,
                    StartDate = iwe.StartDate,
                    EndDate = iwe.EndDate,
                    InternId = iwe.InternId
                })
                .ToListAsync();
            return Ok(internWorkExperiences);
        }

        // GET: api/InternWorkExperience/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<InternWorkExperienceDto>> GetInternWorkExperience(int id)
        {
            var internWorkExperience = await _context.InternWorkExperiences
                .Include(ie => ie.Intern) // Include the related intern (if needed)
                .FirstOrDefaultAsync(iwe => iwe.Id == id);
                
            if (internWorkExperience == null)
            {
                return NotFound();
            }

            return Ok(new InternWorkExperienceDto
            {
                Id = internWorkExperience.Id,
                Company = internWorkExperience.Company,
                CompanyLocation = internWorkExperience.CompanyLocation,
                Position = internWorkExperience.Position,
                StartDate = internWorkExperience.StartDate,
                EndDate = internWorkExperience.EndDate,
                InternId = internWorkExperience.InternId
            });
        }

        // POST: api/InternWorkExperience
        [HttpPost]
        public async Task<ActionResult<InternWorkExperienceDto>> CreateInternWorkExperience(InternWorkExperienceDto internWorkExperienceDto)
        {
            var intern = await _context.Interns.FindAsync(internWorkExperienceDto.InternId);
            if (intern == null)
            {
                return NotFound("Intern not found.");
            }
            var internWorkExperience = new InternWorkExperience
            {
                Company = internWorkExperienceDto.Company,
                CompanyLocation = internWorkExperienceDto.CompanyLocation,
                Position = internWorkExperienceDto.Position,
                StartDate = internWorkExperienceDto.StartDate,
                EndDate = internWorkExperienceDto.EndDate,
                InternId = internWorkExperienceDto.InternId,
                Intern = intern 
            };

            _context.InternWorkExperiences.Add(internWorkExperience);
            await _context.SaveChangesAsync();

            internWorkExperienceDto.Id = internWorkExperience.Id;
            return CreatedAtAction(nameof(GetInternWorkExperience), new { id = internWorkExperience.Id }, internWorkExperienceDto);
        }

        // PUT: api/InternWorkExperience/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInternWorkExperience(int id, InternWorkExperienceDto internWorkExperienceDto)
        {
            if (id != internWorkExperienceDto.Id)
            {
                return BadRequest();
            }

            var internWorkExperience = await _context.InternWorkExperiences.FindAsync(id);
            if (internWorkExperience == null)
            {
                return NotFound();
            }

            internWorkExperience.Company = internWorkExperienceDto.Company;
            internWorkExperience.CompanyLocation = internWorkExperienceDto.CompanyLocation;
            internWorkExperience.Position = internWorkExperienceDto.Position;
            internWorkExperience.StartDate = internWorkExperienceDto.StartDate;
            internWorkExperience.EndDate = internWorkExperienceDto.EndDate;
            internWorkExperience.InternId = internWorkExperienceDto.InternId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/InternWorkExperience/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInternWorkExperience(int id)
        {
            var internWorkExperience = await _context.InternWorkExperiences.FindAsync(id);
            if (internWorkExperience == null)
            {
                return NotFound();
            }

            _context.InternWorkExperiences.Remove(internWorkExperience);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
