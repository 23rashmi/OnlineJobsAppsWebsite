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
    public class JobStatusTablesController : ControllerBase
    {
        private readonly JobsPortalDbContext _context;

        public JobStatusTablesController(JobsPortalDbContext context)
        {
            _context = context;
        }

        // GET: api/JobStatusTables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobStatusTable>>> GetJobStatusTables()
        {
          if (_context.JobStatusTables == null)
          {
              return NotFound();
          }
            return await _context.JobStatusTables.ToListAsync();
        }

        // GET: api/JobStatusTables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobStatusTable>> GetJobStatusTable(int id)
        {
          if (_context.JobStatusTables == null)
          {
              return NotFound();
          }
            var jobStatusTable = await _context.JobStatusTables.FindAsync(id);

            if (jobStatusTable == null)
            {
                return NotFound();
            }

            return jobStatusTable;
        }

        // PUT: api/JobStatusTables/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobStatusTable(int id, JobStatusTable jobStatusTable)
        {
            if (id != jobStatusTable.JobStatusId)
            {
                return BadRequest();
            }

            _context.Entry(jobStatusTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobStatusTableExists(id))
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

        // POST: api/JobStatusTables
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JobStatusTable>> PostJobStatusTable(JobStatusTable jobStatusTable)
        {
          if (_context.JobStatusTables == null)
          {
              return Problem("Entity set 'JobsPortalDbContext.JobStatusTables'  is null.");
          }
            _context.JobStatusTables.Add(jobStatusTable);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJobStatusTable", new { id = jobStatusTable.JobStatusId }, jobStatusTable);
        }

        // DELETE: api/JobStatusTables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobStatusTable(int id)
        {
            if (_context.JobStatusTables == null)
            {
                return NotFound();
            }
            var jobStatusTable = await _context.JobStatusTables.FindAsync(id);
            if (jobStatusTable == null)
            {
                return NotFound();
            }

            _context.JobStatusTables.Remove(jobStatusTable);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobStatusTableExists(int id)
        {
            return (_context.JobStatusTables?.Any(e => e.JobStatusId == id)).GetValueOrDefault();
        }
    }
}
