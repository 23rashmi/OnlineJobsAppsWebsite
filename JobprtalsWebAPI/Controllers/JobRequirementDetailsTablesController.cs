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
    public class JobRequirementDetailsTablesController : ControllerBase
    {
        private readonly JobsPortalDbContext _context;

        public JobRequirementDetailsTablesController(JobsPortalDbContext context)
        {
            _context = context;
        }

        // GET: api/JobRequirementDetailsTables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobRequirementDetailsTable>>> GetJobRequirementDetailsTables()
        {
          if (_context.JobRequirementDetailsTables == null)
          {
              return NotFound();
          }
            return await _context.JobRequirementDetailsTables.ToListAsync();
        }

        // GET: api/JobRequirementDetailsTables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobRequirementDetailsTable>> GetJobRequirementDetailsTable(int id)
        {
          if (_context.JobRequirementDetailsTables == null)
          {
              return NotFound();
          }
            var jobRequirementDetailsTable = await _context.JobRequirementDetailsTables.FindAsync(id);

            if (jobRequirementDetailsTable == null)
            {
                return NotFound();
            }

            return jobRequirementDetailsTable;
        }

        // PUT: api/JobRequirementDetailsTables/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobRequirementDetailsTable(int id, JobRequirementDetailsTable jobRequirementDetailsTable)
        {
            if (id != jobRequirementDetailsTable.JobRequirementDetailsId)
            {
                return BadRequest();
            }

            _context.Entry(jobRequirementDetailsTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobRequirementDetailsTableExists(id))
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

        // POST: api/JobRequirementDetailsTables
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JobRequirementDetailsTable>> PostJobRequirementDetailsTable(JobRequirementDetailsTable jobRequirementDetailsTable)
        {
          if (_context.JobRequirementDetailsTables == null)
          {
              return Problem("Entity set 'JobsPortalDbContext.JobRequirementDetailsTables'  is null.");
          }
            _context.JobRequirementDetailsTables.Add(jobRequirementDetailsTable);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJobRequirementDetailsTable", new { id = jobRequirementDetailsTable.JobRequirementDetailsId }, jobRequirementDetailsTable);
        }

        // DELETE: api/JobRequirementDetailsTables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobRequirementDetailsTable(int id)
        {
            if (_context.JobRequirementDetailsTables == null)
            {
                return NotFound();
            }
            var jobRequirementDetailsTable = await _context.JobRequirementDetailsTables.FindAsync(id);
            if (jobRequirementDetailsTable == null)
            {
                return NotFound();
            }

            _context.JobRequirementDetailsTables.Remove(jobRequirementDetailsTable);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobRequirementDetailsTableExists(int id)
        {
            return (_context.JobRequirementDetailsTables?.Any(e => e.JobRequirementDetailsId == id)).GetValueOrDefault();
        }
    }
}
