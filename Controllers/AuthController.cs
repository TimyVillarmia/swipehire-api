using api.Data;
using api.Dtos.Account;
using api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Email == loginDto.Email);
            if (account == null)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            // Verify the password using BCrypt
            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, account.Password))
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            return Ok(new { message = "Login successful", accountId = account.Id });
        }
    }
}
