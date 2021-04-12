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
                new Exercise{Ex_Name="Squats",MusclegroupId=2},
                new Exercise{Ex_Name="Hack Squats",MusclegroupId=2},
                new Exercise{Ex_Name="Goblet Squats",MusclegroupId=2},
                new Exercise{Ex_Name="Kettlebell Swings",MusclegroupId=3},
                new Exercise{Ex_Name="Sit-ups",MusclegroupId=8},
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