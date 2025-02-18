using Microsoft.AspNetCore.Mvc;
using api.Dtos;
using api.Entities;
using api.Data;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FieldController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Field
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FieldDto>>> GetFields()
        {
            var fields = await _context.Fields
                .Select(f => new FieldDto { Id = f.Id, Name = f.Name })
                .ToListAsync();
            return Ok(fields);
        }

        // GET: api/Field/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<FieldDto>> GetField(int id)
        {
            var field = await _context.Fields.FindAsync(id);
            if (field == null)
            {
                return NotFound();
            }
            return Ok(new FieldDto { Id = field.Id, Name = field.Name });
        }

        // POST: api/Field
        [HttpPost]
        public async Task<ActionResult<FieldDto>> CreateField(FieldDto fieldDto)
        {
            var field = new Field { Name = fieldDto.Name };
            _context.Fields.Add(field);
            await _context.SaveChangesAsync();

            fieldDto.Id = field.Id;
            return CreatedAtAction(nameof(GetField), new { id = field.Id }, fieldDto);
        }

        // PUT: api/Field/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateField(int id, FieldDto fieldDto)
        {
            if (id != fieldDto.Id)
            {
                return BadRequest();
            }

            var field = await _context.Fields.FindAsync(id);
            if (field == null)
            {
                return NotFound();
            }

            field.Name = fieldDto.Name;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Field/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteField(int id)
        {
            var field = await _context.Fields.FindAsync(id);
            if (field == null)
            {
                return NotFound();
            }

            _context.Fields.Remove(field);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}