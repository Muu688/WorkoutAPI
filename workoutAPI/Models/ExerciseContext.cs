using Microsoft.EntityFrameworkCore;
using System;
// The database context is the main class that coordinates Entity Framework functionality for a data model. This class is created by deriving from the Microsoft.EntityFrameworkCore.DbContext class.

namespace WorkoutAPI.Models
{
    public class ExerciseContext : DbContext
    {

        public ExerciseContext(DbContextOptions<ExerciseContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseNpgsql("Host=localhost;Database=workout;Username=postgres;Password=", optionsBuilder => optionsBuilder.SetPostgresVersion(new Version(9, 6)));

        public DbSet<Exercise> exercise { get; set; }
    }
}