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
    public class ExerciseController : ControllerBase
    {
        private readonly ExerciseContext _context;

        public ExerciseController(ExerciseContext context)
        {
            _context = context;
        }

        // GET: api/Exercises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseDTO>>> GetExercises()
        {
            return await _context.exercise
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<IEnumerable<ExerciseDTO>>> GetExercise(int id)
        //{
        //    return await _context.exercise.Where(x => x.Exercise_Id == id).Select(x => ItemToDTO(x)).ToListAsync();
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ExerciseDTO>>> GetExercise(int id)
        {
            return await _context.exercise.Where(x => x.MusclegroupId == id).Select(x => ItemToDTO(x)).ToListAsync();
        }

        [HttpGet("/api/Exercise/GetByMG/{MuscleGroupId}")]
        public async Task<ActionResult<IEnumerable<ExerciseDTO>>> GetExerciseByMGID(int MuscleGroupId)
        {
            return await _context.exercise.Where(x => x.MusclegroupId == MuscleGroupId).Select(x => ItemToDTO(x)).ToListAsync();

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExercise(int id, ExerciseDTO ExerciseDTO)
        {
            if (id != ExerciseDTO.Exercise_Id)
            {
                return BadRequest();
            }

            var Exercise = await _context.exercise.FindAsync(id);
            if (Exercise == null)
            {
                return NotFound();
            }

            Exercise.Exercise_Id = ExerciseDTO.Exercise_Id;
            Exercise.Ex_Name = ExerciseDTO.Ex_Name;
            Exercise.Musclegroup = ExerciseDTO.Musclegroup;
            Exercise.MusclegroupId = ExerciseDTO.MusclegroupId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ExerciseExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ExerciseDTO>> CreateExercise(ExerciseDTO ExerciseDTO)
        {
            var Exercise = new Exercise
            {
                Exercise_Id = ExerciseDTO.Exercise_Id,
                Ex_Name = ExerciseDTO.Ex_Name,
                MusclegroupId = ExerciseDTO.MusclegroupId,
                Musclegroup = ExerciseDTO.Musclegroup,
            };

            _context.exercise.Add(Exercise);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetExercise),
                new { id = Exercise.Exercise_Id },
                ItemToDTO(Exercise));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercise(int id)
        {
            var Exercise = await _context.exercise.FindAsync(id);

            if (Exercise == null)
            {
                return NotFound();
            }

            _context.exercise.Remove(Exercise);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExerciseExists(int id) =>
             _context.exercise.Any(e => e.Exercise_Id == id);

        private static ExerciseDTO ItemToDTO(Exercise Exercise) =>
            new ExerciseDTO
            {
                Exercise_Id = Exercise.Exercise_Id,
                Ex_Name = Exercise.Ex_Name,
                MusclegroupId = Exercise.MusclegroupId,
                Musclegroup = Exercise.Musclegroup,
            };

    }
}
