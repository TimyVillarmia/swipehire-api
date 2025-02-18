using Microsoft.AspNetCore.Mvc;
using api.Dtos;
using api.Entities;
using api.Data;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternEducationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InternEducationController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/InternEducation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InternEducationDto>>> GetInternEducations()
        {
            var internEducations = await _context.InternEducations
                .Select(ie => new InternEducationDto
                {
                    Id = ie.Id,
                    School = ie.School,
                    Degree = ie.Degree,
                    StartDate = ie.StartDate,
                    EndDate = ie.EndDate
                })
                .ToListAsync();
            return Ok(internEducations);
        }

        // GET: api/InternEducation/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<InternEducationDto>> GetInternEducation(int id)
        {
            var internEducation = await _context.InternEducations.FindAsync(id);
            if (internEducation == null)
            {
                return NotFound();
            }

            return Ok(new InternEducationDto
            {
                Id = internEducation.Id,
                School = internEducation.School,
                Degree = internEducation.Degree,
                StartDate = internEducation.StartDate,
                EndDate = internEducation.EndDate
            });
        }

        // POST: api/InternEducation
        [HttpPost]
        public async Task<ActionResult<InternEducationDto>> CreateInternEducation(InternEducationDto internEducationDto)
        {
            var internEducation = new InternEducation
            {
                School = internEducationDto.School,
                Degree = internEducationDto.Degree,
                StartDate = internEducationDto.StartDate,
                EndDate = internEducationDto.EndDate
            };

            _context.InternEducations.Add(internEducation);
            await _context.SaveChangesAsync();

            internEducationDto.Id = internEducation.Id;
            return CreatedAtAction(nameof(GetInternEducation), new { id = internEducation.Id }, internEducationDto);
        }

        // PUT: api/InternEducation/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInternEducation(int id, InternEducationDto internEducationDto)
        {
            if (id != internEducationDto.Id)
            {
                return BadRequest();
            }

            var internEducation = await _context.InternEducations.FindAsync(id);
            if (internEducation == null)
            {
                return NotFound();
            }

            internEducation.School = internEducationDto.School;
            internEducation.Degree = internEducationDto.Degree;
            internEducation.StartDate = internEducationDto.StartDate;
            internEducation.EndDate = internEducationDto.EndDate;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/InternEducation/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInternEducation(int id)
        {
            var internEducation = await _context.InternEducations.FindAsync(id);
            if (internEducation == null)
            {
                return NotFound();
            }

            _context.InternEducations.Remove(internEducation);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
