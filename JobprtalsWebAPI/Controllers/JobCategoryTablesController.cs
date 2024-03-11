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
    public class JobCategoryTablesController : ControllerBase
    {
        private readonly JobsPortalDbContext _context;

        public JobCategoryTablesController(JobsPortalDbContext context)
        {
            _context = context;
        }

        // GET: api/JobCategoryTables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobCategoryTable>>> GetJobCategoryTables()
        {
          if (_context.JobCategoryTables == null)
          {
              return NotFound();
          }
            return await _context.JobCategoryTables.ToListAsync();
        }

        // GET: api/JobCategoryTables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobCategoryTable>> GetJobCategoryTable(int id)
        {
          if (_context.JobCategoryTables == null)
          {
              return NotFound();
          }
            var jobCategoryTable = await _context.JobCategoryTables.FindAsync(id);

            if (jobCategoryTable == null)
            {
                return NotFound();
            }

            return jobCategoryTable;
        }

        // PUT: api/JobCategoryTables/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobCategoryTable(int id, JobCategoryTable jobCategoryTable)
        {
            if (id != jobCategoryTable.JobCategoryId)
            {
                return BadRequest();
            }

            _context.Entry(jobCategoryTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobCategoryTableExists(id))
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

        // POST: api/JobCategoryTables
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JobCategoryTable>> PostJobCategoryTable(JobCategoryTable jobCategoryTable)
        {
          if (_context.JobCategoryTables == null)
          {
              return Problem("Entity set 'JobsPortalDbContext.JobCategoryTables'  is null.");
          }
            _context.JobCategoryTables.Add(jobCategoryTable);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJobCategoryTable", new { id = jobCategoryTable.JobCategoryId }, jobCategoryTable);
        }

        // DELETE: api/JobCategoryTables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobCategoryTable(int id)
        {
            if (_context.JobCategoryTables == null)
            {
                return NotFound();
            }
            var jobCategoryTable = await _context.JobCategoryTables.FindAsync(id);
            if (jobCategoryTable == null)
            {
                return NotFound();
            }

            _context.JobCategoryTables.Remove(jobCategoryTable);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobCategoryTableExists(int id)
        {
            return (_context.JobCategoryTables?.Any(e => e.JobCategoryId == id)).GetValueOrDefault();
        }
    }
}
