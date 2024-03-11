using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobprtalsWebAPI.Models;

namespace JobprtalsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypeTablesController : ControllerBase
    {
        private readonly JobsPortalDbContext _context;

        public UserTypeTablesController(JobsPortalDbContext context)
        {
            _context = context;
        }

        // GET: api/UserTypeTables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTypeTable>>> GetUserTypeTables()
        {
          if (_context.UserTypeTables == null)
          {
              return NotFound();
          }
            return await _context.UserTypeTables.ToListAsync();
        }

        // GET: api/UserTypeTables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserTypeTable>> GetUserTypeTable(int id)
        {
          if (_context.UserTypeTables == null)
          {
              return NotFound();
          }
            var userTypeTable = await _context.UserTypeTables.FindAsync(id);

            if (userTypeTable == null)
            {
                return NotFound();
            }

            return userTypeTable;
        }

        // PUT: api/UserTypeTables/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserTypeTable(int id, UserTypeTable userTypeTable)
        {
            if (id != userTypeTable.UserTypeId)
            {
                return BadRequest();
            }

            _context.Entry(userTypeTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTypeTableExists(id))
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

        // POST: api/UserTypeTables
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserTypeTable>> PostUserTypeTable(UserTypeTable userTypeTable)
        {
          if (_context.UserTypeTables == null)
          {
              return Problem("Entity set 'JobsPortalDbContext.UserTypeTables'  is null.");
          }
            _context.UserTypeTables.Add(userTypeTable);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserTypeTable", new { id = userTypeTable.UserTypeId }, userTypeTable);
        }

        // DELETE: api/UserTypeTables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserTypeTable(int id)
        {
            if (_context.UserTypeTables == null)
            {
                return NotFound();
            }
            var userTypeTable = await _context.UserTypeTables.FindAsync(id);
            if (userTypeTable == null)
            {
                return NotFound();
            }

            _context.UserTypeTables.Remove(userTypeTable);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserTypeTableExists(int id)
        {
            return (_context.UserTypeTables?.Any(e => e.UserTypeId == id)).GetValueOrDefault();
        }
    }
}
