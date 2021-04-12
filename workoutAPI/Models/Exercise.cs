using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutAPI
{
    [Table("Exercise")]
    public class Exercise
    {
        [Required][Key]
        public int Exercise_Id { get; set; }

        public string Ex_Name { get; set; }

        public int MusclegroupId { get; set; }

        [ForeignKey("MusclegroupId")]
        public Musclegroup Musclegroup { get; set; }
        
    }
}
