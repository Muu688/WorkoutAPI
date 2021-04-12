using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutAPI;

namespace WorkoutAPI.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusclegroupsController : ControllerBase
    {
        private readonly WorkoutContext _context;

        public MusclegroupsController(WorkoutContext context)
        {
            _context = context;
        }

        // GET: api/Musclegroups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Musclegroup>>> Getmusclegroup()
        {
            return await _context.musclegroup.ToListAsync();
        }

        // GET: api/Musclegroups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Musclegroup>> GetMusclegroup(int id)
        {
            var musclegroup = await _context.musclegroup.FindAsync(id);

            if (musclegroup == null)
            {
                return NotFound();
            }

            return musclegroup;
        }

        // PUT: api/Musclegroups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMusclegroup(int id, Musclegroup musclegroup)
        {
            if (id != musclegroup.MusclegroupId)
            {
                return BadRequest();
            }

            _context.Entry(musclegroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MusclegroupExists(id))
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

        // POST: api/Musclegroups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Musclegroup>> PostMusclegroup(Musclegroup musclegroup)
        {
            _context.musclegroup.Add(musclegroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMusclegroup", new { id = musclegroup.MusclegroupId }, musclegroup);
        }

        // DELETE: api/Musclegroups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMusclegroup(int id)
        {
            var musclegroup = await _context.musclegroup.FindAsync(id);
            if (musclegroup == null)
            {
                return NotFound();
            }

            _context.musclegroup.Remove(musclegroup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MusclegroupExists(int id)
        {
            return _context.musclegroup.Any(e => e.MusclegroupId == id);
        }
    }
}
