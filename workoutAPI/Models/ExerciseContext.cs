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

        private string GetHerokuConnectionString()
        {
            // Get the connection string from the ENV variables
            string connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            // parse the connection string
            var databaseUri = new Uri(connectionUrl);

            string db = databaseUri.LocalPath.TrimStart('/');
            string[] userInfo = databaseUri.UserInfo.Split(':', StringSplitOptions.RemoveEmptyEntries);

            return $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};Port={databaseUri.Port};Database={db};Pooling=true;SSL Mode=Require;Trust Server Certificate=True;";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseNpgsql(GetHerokuConnectionString(), optionsBuilder => optionsBuilder.SetPostgresVersion(new Version(9, 6)));

        public DbSet<Exercise> exercise { get; set; }
    }
}