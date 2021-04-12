using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutAPI
{
    [Table("Musclegroup")]
    public class Musclegroup
    {
        [Required]
        public int MusclegroupId { get; set; }
        
        public string MuscleGroup { get; set; }

        public List<Exercise> Exercises { get; set; }
    }
}
