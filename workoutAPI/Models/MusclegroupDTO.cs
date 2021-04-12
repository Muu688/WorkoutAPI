using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorkoutAPI
{
    public class MusclegroupDTO
    {
        [Required]
        public int MusclegroupId { get; set; }

        public string MuscleGroup { get; set; }

        public List<Exercise> Exercises { get; set; }
    }
}
