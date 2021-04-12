using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutAPI
{
    public class Workout
    {
        [Required][Key]
        public int Id { get; set; }

        public DateTime WorkoutDate { get; set; }
        
        public int Exercise_Id { get; set; }

        [ForeignKey("Exercise_Id")]
        public Exercise Exercise { get; set; }

        public int Sets { get; set; }

        public int Reps { get; set; }

        public decimal Weight { get; set; }

        public string Note { get; set; }

        public string UserId { get; set;
        }
        //public string Secret { get; set; }

    }
}
