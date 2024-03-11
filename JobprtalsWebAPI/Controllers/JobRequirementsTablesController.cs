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
    public class JobRequirementsTablesController : ControllerBase
    {
        private readonly JobsPortalDbContext _context;

        public JobRequirementsTablesController(JobsPortalDbContext context)
        {
            _context = context;
        }

        // GET: api/JobRequirementsTables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobRequirementsTable>>> GetJobRequirementsTables()
        {
          if (_context.JobRequirementsTables == null)
          {
              return NotFound();
          }
            return await _context.JobRequirementsTables.ToListAsync();
        }

        // GET: api/JobRequirementsTables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobRequirementsTable>> GetJobRequirementsTable(int id)
        {
          if (_context.JobRequirementsTables == null)
          {
              return NotFound();
          }
            var jobRequirementsTable = await _context.JobRequirementsTables.FindAsync(id);

            if (jobRequirementsTable == null)
            {
                return NotFound();
            }

            return jobRequirementsTable;
        }

        // PUT: api/JobRequirementsTables/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobRequirementsTable(int id, JobRequirementsTable jobRequirementsTable)
        {
            if (id != jobRequirementsTable.JobRequirementId)
            {
                return BadRequest();
            }

            _context.Entry(jobRequirementsTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobRequirementsTableExists(id))
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

        // POST: api/JobRequirementsTables
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JobRequirementsTable>> PostJobRequirementsTable(JobRequirementsTable jobRequirementsTable)
        {
          if (_context.JobRequirementsTables == null)
          {
              return Problem("Entity set 'JobsPortalDbContext.JobRequirementsTables'  is null.");
          }
            _context.JobRequirementsTables.Add(jobRequirementsTable);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJobRequirementsTable", new { id = jobRequirementsTable.JobRequirementId }, jobRequirementsTable);
        }

        // DELETE: api/JobRequirementsTables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobRequirementsTable(int id)
        {
            if (_context.JobRequirementsTables == null)
            {
                return NotFound();
            }
            var jobRequirementsTable = await _context.JobRequirementsTables.FindAsync(id);
            if (jobRequirementsTable == null)
            {
                return NotFound();
            }

            _context.JobRequirementsTables.Remove(jobRequirementsTable);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobRequirementsTableExists(int id)
        {
            return (_context.JobRequirementsTables?.Any(e => e.JobRequirementId == id)).GetValueOrDefault();
        }
    }
}
