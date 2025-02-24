using api.Data;
using api.Dtos;
using api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SwipeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SwipeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/swipe
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SwipeDto>>> GetSwipes()
        {
            var swipes = await _context.Swipes
                .Select(s => new SwipeDto
                {
                    Id = s.Id,
                    RecruitId = s.RecruitId,
                    InternId = s.InternId,
                    SwipeDate = s.SwipeDate,
                    Status = s.Status ?? "Pending" // Ensures a default value when retrieving
                })
                .ToListAsync();

            return Ok(swipes);
        }

        // GET: api/swipe/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SwipeDto>> GetSwipeById(int id)
        {
            var swipe = await _context.Swipes.FindAsync(id);
            if (swipe == null) return NotFound();

            return new SwipeDto
            {
                Id = swipe.Id,
                RecruitId = swipe.RecruitId,
                InternId = swipe.InternId,
                SwipeDate = swipe.SwipeDate,
                Status = swipe.Status ?? "Pending" // Ensures a default value when retrieving
            };
        }

        // GET: api/swipe/byRecruit/{recruitId}
        [HttpGet("byRecruit/{recruitId}")]
        public async Task<ActionResult<IEnumerable<SwipeDto>>> GetSwipesByRecruitId(int recruitId)
        {
            var swipes = await _context.Swipes
                .Where(s => s.RecruitId == recruitId)
                .Select(s => new SwipeDto
                {
                    Id = s.Id,
                    RecruitId = s.RecruitId,
                    InternId = s.InternId,
                    SwipeDate = s.SwipeDate,
                    Status = s.Status ?? "Pending"
                })
                .ToListAsync();

            if (swipes.Count == 0) return NotFound($"No swipes found for RecruitId {recruitId}.");

            return Ok(swipes);
        }

        // GET: api/swipe/byIntern/{internId}
        [HttpGet("byIntern/{internId}")]
        public async Task<ActionResult<IEnumerable<SwipeDto>>> GetSwipesByInternId(int internId)
        {
            var swipes = await _context.Swipes
                .Where(s => s.InternId == internId)
                .Select(s => new SwipeDto
                {
                    Id = s.Id,
                    RecruitId = s.RecruitId,
                    InternId = s.InternId,
                    SwipeDate = s.SwipeDate,
                    Status = s.Status ?? "Pending"
                })
                .ToListAsync();

            if (swipes.Count == 0) return NotFound($"No swipes found for InternId {internId}.");

            return Ok(swipes);
        }


        // POST: api/swipe
        [HttpPost]
        public async Task<ActionResult<SwipeDto>> CreateSwipe(SwipeDto swipeDto)
        {
            // Validate if Recruit and Intern exist
            var recruit = await _context.Recruits.FindAsync(swipeDto.RecruitId);
            var intern = await _context.Interns.FindAsync(swipeDto.InternId);
            if (recruit == null || intern == null)
                return BadRequest("Recruit or Intern not found.");

            var swipe = new Swipe
            {
                RecruitId = swipeDto.RecruitId,
                InternId = swipeDto.InternId,
                Recruit = recruit,
                Intern = intern,
                SwipeDate = swipeDto.SwipeDate,
                Status = swipeDto.Status ?? "Pending" // ✅ Default to "Pending" if null
            };

            _context.Swipes.Add(swipe);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSwipeById), new { id = swipe.Id }, new SwipeDto
            {
                Id = swipe.Id,
                RecruitId = swipe.RecruitId,
                InternId = swipe.InternId,
                SwipeDate = swipe.SwipeDate,
                Status = swipe.Status
            });
        }

        // PUT: api/swipe/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSwipe(int id, SwipeDto swipeDto)
        {
            var swipe = await _context.Swipes.FindAsync(id);
            if (swipe == null) return NotFound();

            swipe.Status = swipeDto.Status ?? swipe.Status; // ✅ Keeps existing status if null
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/swipe/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSwipe(int id)
        {
            var swipe = await _context.Swipes.FindAsync(id);
            if (swipe == null) return NotFound();

            _context.Swipes.Remove(swipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
