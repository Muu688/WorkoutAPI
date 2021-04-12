using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutAPI.Models
{
    public class WorkoutDTO
    {
        [Required]
        public int Id { get; set; }

        public DateTime WorkoutDate { get; set; }

        public int Exercise_Id { get; set; }

        [ForeignKey("Exercise_Id")]
        public Exercise Exercise { get; set; }

        public int Sets { get; set; }

        public int Reps { get; set; }

        public decimal Weight { get; set; }

        public string Note { get; set; }

        public string UserId
        {
            get; set;

        }
    }
}
