using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutAPI;
using WorkoutAPI.Models;

namespace WorkoutAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        private readonly WorkoutContext _context;

        public WorkoutController(WorkoutContext context)
        {
            _context = context;
        }

        // GET: api/Workouts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutDTO>>> GetWorkouts()
        {
            return await _context.workout
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutDTO>> GetWorkout(int id)
        {
            var workout = await _context.workout.FindAsync(id);

            if (workout == null)
            {
                return NotFound();
            }

            return ItemToDTO(workout);
        }

        [HttpGet("/api/Workout/user/{userId}")]
        public async Task<ActionResult<IEnumerable<WorkoutDTO>>> GetWorkoutByUID(string userId)
        {
            //return await _context.workout.Where(x => x.UserId == userId).Select(x => ItemToDTO(x)).ToListAsync();
            return await _context.workout.Include(x => x.Exercise).Where(x => x.UserId == userId).Select(x => ItemToDTO(x)).ToListAsync();
            //return await _context.workout.Where(x => x.UserId == userId)..Select(x => ItemToDTO(x)).ToListAsync();
        }

        [HttpGet("{date}/{userId}")]
        public async Task<ActionResult<IEnumerable<WorkoutDTO>>> GetWorkoutByDate(DateTime date, string userId)
        {
            return await _context.workout.Include(x => x.Exercise).Where(x => x.UserId == userId).Where(x => x.WorkoutDate == date).Select(x => ItemToDTO(x)).ToListAsync();// FindAsync(date);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkout(int id, WorkoutDTO WorkoutDTO)
        {
            if (id != WorkoutDTO.Id)
            {
                return BadRequest();
            }

            var Workout = await _context.workout.FindAsync(id);
            if (Workout == null)
            {
                return NotFound();
            }

            Workout.Id = WorkoutDTO.Id;
            Workout.WorkoutDate = WorkoutDTO.WorkoutDate;
            Workout.Exercise_Id = WorkoutDTO.Exercise_Id;
            Workout.Sets = WorkoutDTO.Sets;
            Workout.Reps = WorkoutDTO.Reps;
            Workout.Weight = WorkoutDTO.Weight;
            Workout.Note = WorkoutDTO.Note;
            Workout.UserId = WorkoutDTO.UserId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!WorkoutExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<WorkoutDTO>> CreateWorkout(WorkoutDTO WorkoutDTO)
        {
            var Workout = new Workout
            {
                Id = WorkoutDTO.Id,
                WorkoutDate = WorkoutDTO.WorkoutDate,
                Exercise_Id = WorkoutDTO.Exercise_Id,
                Sets = WorkoutDTO.Sets,
                Reps = WorkoutDTO.Reps,
                Weight = WorkoutDTO.Weight,
                Note = WorkoutDTO.Note,
                UserId = WorkoutDTO.UserId,
            };

            _context.workout.Add(Workout);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetWorkout),
                new { id = Workout.Id },
                ItemToDTO(Workout));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkout(int id)
        {
            var Workout = await _context.workout.FindAsync(id);

            if (Workout == null)
            {
                return NotFound();
            }

            _context.workout.Remove(Workout);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkoutExists(int id) =>
             _context.workout.Any(e => e.Id == id);

        private static WorkoutDTO ItemToDTO(Workout Workout) =>
            new WorkoutDTO
            {
                Exercise_Id = Workout.Exercise_Id,
                WorkoutDate = Workout.WorkoutDate,
                Exercise = Workout.Exercise,
                Sets = Workout.Sets,
                Reps = Workout.Reps,
                Weight = Workout.Weight,
                Note = Workout.Note,
                UserId = Workout.UserId,
            };

    }
}
