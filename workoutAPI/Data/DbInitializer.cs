using WorkoutAPI.Data;
using WorkoutAPI.Models;
using System;
using System.Linq;

namespace WorkoutAPI.Data
{
    public static class DbInitializer
    {
        public static void Initialize(WorkoutContext context)
        {
            context.Database.EnsureCreated();

            // Look for any workouts.
            if (context.workout.Any())
            {
                return;   // DB has been seeded
            }

            var musclegroups = new Musclegroup[]
            {
                new Musclegroup{MusclegroupId=1,MuscleGroup="Chest"},
                new Musclegroup{MusclegroupId=2,MuscleGroup="Quads"},
                new Musclegroup{MusclegroupId=3,MuscleGroup="Hamstrings"},
                new Musclegroup{MusclegroupId=4,MuscleGroup="Glutes"},
                new Musclegroup{MusclegroupId=5,MuscleGroup="Arms"},
                new Musclegroup{MusclegroupId=6,MuscleGroup="Back"},
                new Musclegroup{MusclegroupId=7,MuscleGroup="Shoulders"},
                new Musclegroup{MusclegroupId=8,MuscleGroup="Core"},
                new Musclegroup{MusclegroupId=9,MuscleGroup="Cardio"},
                new Musclegroup{MusclegroupId=10,MuscleGroup="Other"},
            };

            context.musclegroup.AddRange(musclegroups);
            context.SaveChanges();

            var exercises = new Exercise[]
            {
                new Exercise{Ex_Name="Bench Press",MusclegroupId=1},
                new Exercise{Ex_Name="Pushups",MusclegroupId=1},
                new Exercise{Ex_Name="Dumbbell Press",MusclegroupId=1},
                new Exercise{Ex_Name="Incline Press",MusclegroupId=1},
                new Exercise{Ex_Name="Decline Press",MusclegroupId=1},
                new Exercise{Ex_Name="Cable Flyes",MusclegroupId=1},
                new Exercise{Ex_Name="Dumbbell Flyes",MusclegroupId=1},
                new Exercise{Ex_Name="Machine Chest Press",MusclegroupId=1},
                new Exercise{Ex_Name="Squats",MusclegroupId=2},
                new Exercise{Ex_Name="Hack Squats",MusclegroupId=2},
                new Exercise{Ex_Name="Goblet Squats",MusclegroupId=2},
                new Exercise{Ex_Name="Lunges",MusclegroupId=2},
                new Exercise{Ex_Name="Leg Press",MusclegroupId=2},
                new Exercise{Ex_Name="Jump Squats",MusclegroupId=2},
                new Exercise{Ex_Name="Front Squats",MusclegroupId=2},
                new Exercise{Ex_Name="Zombie Squats",MusclegroupId=2},
                new Exercise{Ex_Name="Sissy Squats",MusclegroupId=2},
                new Exercise{Ex_Name="Leg Extensions",MusclegroupId=2},
                new Exercise{Ex_Name="Jump Lunges",MusclegroupId=2},
                new Exercise{Ex_Name="Box Squats",MusclegroupId=2},
                new Exercise{Ex_Name="Kettlebell Swings",MusclegroupId=3},
                new Exercise{Ex_Name="Hamstring Curls",MusclegroupId=3},
                new Exercise{Ex_Name="Nordic Hamstring Curls",MusclegroupId=3},
                new Exercise{Ex_Name="Single Leg Curls",MusclegroupId=3},
                new Exercise{Ex_Name="Good Mornings",MusclegroupId=3},
                new Exercise{Ex_Name="Kettlebell Sumo Deadlift",MusclegroupId=3},
                new Exercise{Ex_Name="Stiff-legged Deadlift",MusclegroupId=3},
                new Exercise{Ex_Name="Hip Raises",MusclegroupId=4},
                new Exercise{Ex_Name="Glute Bridges",MusclegroupId=4},
                new Exercise{Ex_Name="Barbell Curls",MusclegroupId=5},
                new Exercise{Ex_Name="Hammer Curls",MusclegroupId=5},
                new Exercise{Ex_Name="EZ-Bar Curl",MusclegroupId=5},
                new Exercise{Ex_Name="Zottman Curl",MusclegroupId=5},
                new Exercise{Ex_Name="Spider Curl",MusclegroupId=5},
                new Exercise{Ex_Name="Preacher Curl",MusclegroupId=5},
                new Exercise{Ex_Name="Tricep Rope Pushdown",MusclegroupId=5},
                new Exercise{Ex_Name="Skull Crusher",MusclegroupId=5},
                new Exercise{Ex_Name="Tricep Kickback",MusclegroupId=5},
                new Exercise{Ex_Name="Dips",MusclegroupId=5},
                new Exercise{Ex_Name="Cable Pushdowns",MusclegroupId=5},
                new Exercise{Ex_Name="Close-Grip Bench Press",MusclegroupId=5},
                new Exercise{Ex_Name="Chin Ups",MusclegroupId=6},
                new Exercise{Ex_Name="Ring Inverted Row",MusclegroupId=6},
                new Exercise{Ex_Name="Dumbbell Row",MusclegroupId=6},
                new Exercise{Ex_Name="Deadlift",MusclegroupId=6},
                new Exercise{Ex_Name="Trap Bar Deadlift",MusclegroupId=6},
                new Exercise{Ex_Name="Lat Pull down",MusclegroupId=6},
                new Exercise{Ex_Name="T-Bar Row",MusclegroupId=6},
                new Exercise{Ex_Name="Barbell Row",MusclegroupId=6},
                new Exercise{Ex_Name="Cable Row",MusclegroupId=6},
                new Exercise{Ex_Name="Rack Pull",MusclegroupId=6},
                new Exercise{Ex_Name="Ring Inverted Row",MusclegroupId=6},
                new Exercise{Ex_Name="Dumbell front raise",MusclegroupId=7},
                new Exercise{Ex_Name="Clean and Press",MusclegroupId=7},
                new Exercise{Ex_Name="Clean and Jerk",MusclegroupId=7},
                new Exercise{Ex_Name="Shoulder Press",MusclegroupId=7},
                new Exercise{Ex_Name="Lateral Raises",MusclegroupId=7},
                new Exercise{Ex_Name="Reverse Fly",MusclegroupId=7},
                new Exercise{Ex_Name="Barbell High Pull",MusclegroupId=7},
                new Exercise{Ex_Name="Kettlebell Overhead Press",MusclegroupId=7},
                new Exercise{Ex_Name="Arnold Press",MusclegroupId=7},
                new Exercise{Ex_Name="Plate Raise",MusclegroupId=7},
                new Exercise{Ex_Name="Bent-over dumbbell rear delt row",MusclegroupId=7},
                new Exercise{Ex_Name="Russian Twists",MusclegroupId=8},
                new Exercise{Ex_Name="Side Plank",MusclegroupId=8},
                new Exercise{Ex_Name="Renegade Row",MusclegroupId=8},
                new Exercise{Ex_Name="Plank (Commando)",MusclegroupId=8},
                new Exercise{Ex_Name="Plank",MusclegroupId=8},
                new Exercise{Ex_Name="Mountain Climb",MusclegroupId=8},
                new Exercise{Ex_Name="Sit-ups",MusclegroupId=8},
                new Exercise{Ex_Name="Cable Core Press",MusclegroupId=8},
                new Exercise{Ex_Name="Cable Twists",MusclegroupId=8},
                new Exercise{Ex_Name="Farmers Walk",MusclegroupId=9},
                new Exercise{Ex_Name="Interval Ropes",MusclegroupId=9},
                new Exercise{Ex_Name="Rower",MusclegroupId=9},
                new Exercise{Ex_Name="Wall Sits",MusclegroupId=9},
                new Exercise{Ex_Name="Treadmill",MusclegroupId=9},
                new Exercise{Ex_Name="Stairmaster",MusclegroupId=9},
                new Exercise{Ex_Name="Cross Trainer",MusclegroupId=9},
                new Exercise{Ex_Name="Burpees",MusclegroupId=9},
                new Exercise{Ex_Name="Sled Run",MusclegroupId=9},
                new Exercise{Ex_Name="Deadball Slam",MusclegroupId=9},
                new Exercise{Ex_Name="Run",MusclegroupId=9},
            };

            context.exercise.AddRange(exercises);
            context.SaveChanges();

            var workouts = new Workout[]
            {
                new Workout{WorkoutDate=DateTime.Parse("2021-03-25"),Exercise_Id=1,Sets=5,Reps=5,Weight=5,Note="Sets",UserId="Z2CUmwc2rGbOVPSNzKl1UUBfaDx1"},
                new Workout{WorkoutDate=DateTime.Parse("2021-03-25"),Exercise_Id=2,Sets=5,Reps=5,Weight=5,Note="Sets",UserId="Z2CUmwc2rGbOVPSNzKl1UUBfaDx1"},
            };

            context.workout.AddRange(workouts);
            context.SaveChanges();

        }
    }
}