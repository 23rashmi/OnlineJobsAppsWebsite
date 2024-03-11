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
    public class JobNatureTablesController : ControllerBase
    {
        private readonly JobsPortalDbContext _context;

        public JobNatureTablesController(JobsPortalDbContext context)
        {
            _context = context;
        }

        // GET: api/JobNatureTables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobNatureTable>>> GetJobNatureTables()
        {
          if (_context.JobNatureTables == null)
          {
              return NotFound();
          }
            return await _context.JobNatureTables.ToListAsync();
        }

        // GET: api/JobNatureTables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobNatureTable>> GetJobNatureTable(int id)
        {
          if (_context.JobNatureTables == null)
          {
              return NotFound();
          }
            var jobNatureTable = await _context.JobNatureTables.FindAsync(id);

            if (jobNatureTable == null)
            {
                return NotFound();
            }

            return jobNatureTable;
        }

        // PUT: api/JobNatureTables/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobNatureTable(int id, JobNatureTable jobNatureTable)
        {
            if (id != jobNatureTable.JobNatureId)
            {
                return BadRequest();
            }

            _context.Entry(jobNatureTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobNatureTableExists(id))
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

        // POST: api/JobNatureTables
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JobNatureTable>> PostJobNatureTable(JobNatureTable jobNatureTable)
        {
          if (_context.JobNatureTables == null)
          {
              return Problem("Entity set 'JobsPortalDbContext.JobNatureTables'  is null.");
          }
            _context.JobNatureTables.Add(jobNatureTable);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJobNatureTable", new { id = jobNatureTable.JobNatureId }, jobNatureTable);
        }

        // DELETE: api/JobNatureTables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobNatureTable(int id)
        {
            if (_context.JobNatureTables == null)
            {
                return NotFound();
            }
            var jobNatureTable = await _context.JobNatureTables.FindAsync(id);
            if (jobNatureTable == null)
            {
                return NotFound();
            }

            _context.JobNatureTables.Remove(jobNatureTable);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobNatureTableExists(int id)
        {
            return (_context.JobNatureTables?.Any(e => e.JobNatureId == id)).GetValueOrDefault();
        }
    }
}
