using Microsoft.EntityFrameworkCore;
using System;
// The database context is the main class that coordinates Entity Framework functionality for a data model. This class is created by deriving from the Microsoft.EntityFrameworkCore.DbContext class.

namespace WorkoutAPI.Models
{
    public class WorkoutContext : DbContext
    {

        public WorkoutContext(DbContextOptions<WorkoutContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //=> optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("")));
    => optionsBuilder.UseNpgsql("Host=localhost;Database=workout;Username=postgres;Password=", optionsBuilder => optionsBuilder.SetPostgresVersion(new Version(9,6)));

        public DbSet<Workout> workout { get; set; }
        public DbSet<Exercise> exercise { get; set; }
        public DbSet<Musclegroup> musclegroup { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Console.WriteLine("Creating Exercise");
            modelBuilder.Entity<Exercise>().ToTable("Exercise");
            Console.WriteLine("Creating Workout");
            modelBuilder.Entity<Workout>().ToTable("Workout");
            Console.WriteLine("Creating MuscleGroup");
            modelBuilder.Entity<Musclegroup>().ToTable("Musclegroup");
        }

    }
}