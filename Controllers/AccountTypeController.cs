using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Dtos;
using api.Entities;
using api.Dtos.AccountType;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountTypeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AccountTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/AccountType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountTypeDto>>> GetAccountTypes()
        {
            var accountTypes = await _context.AccountTypes
                .Select(at => new AccountTypeDto
                {
                    Id = at.Id,
                    TypeName = at.TypeName
                })
                .ToListAsync();

            return Ok(accountTypes);
        }

        // GET: api/AccountType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountTypeDto>> GetAccountType(int id)
        {
            var accountType = await _context.AccountTypes
                .Where(at => at.Id == id)
                .Select(at => new AccountTypeDto
                {
                    Id = at.Id,
                    TypeName = at.TypeName
                })
                .FirstOrDefaultAsync();

            if (accountType == null)
            {
                return NotFound();
            }

            return Ok(accountType);
        }

        // POST: api/AccountType
        [HttpPost]
        public async Task<ActionResult<AccountTypeDto>> PostAccountType(AccountTypeDto accountTypeDto)
        {
            var accountType = new AccountType
            {
                TypeName = accountTypeDto.TypeName
            };

            _context.AccountTypes.Add(accountType);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAccountType), new { id = accountType.Id }, accountTypeDto);
        }

        // PUT: api/AccountType/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccountType(int id, AccountTypeDto accountTypeDto)
        {
            if (id != accountTypeDto.Id)
            {
                return BadRequest();
            }

            var accountType = await _context.AccountTypes.FindAsync(id);
            if (accountType == null)
            {
                return NotFound();
            }

            accountType.TypeName = accountTypeDto.TypeName;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/AccountType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountType(int id)
        {
            var accountType = await _context.AccountTypes.FindAsync(id);
            if (accountType == null)
            {
                return NotFound();
            }

            _context.AccountTypes.Remove(accountType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountTypeExists(int id)
        {
            return _context.AccountTypes.Any(e => e.Id == id);
        }
    }
}
