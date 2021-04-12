using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using WorkoutAPI.Models;
using System;

namespace WorkoutAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // To-Do move to its own class...
            string environment = Environment.GetEnvironmentVariable("Environment") ?? "";
            string DATABASE_URL = Environment.GetEnvironmentVariable("DATABASE_URL") ?? "";
            string user = Environment.GetEnvironmentVariable("user") ?? "";
            string password = Environment.GetEnvironmentVariable("password") ?? "";
            string port = Environment.GetEnvironmentVariable("port") ?? "";
            string database = Environment.GetEnvironmentVariable("database") ?? "";

            string connectionString = Configuration.GetConnectionString("WORKOUT");
            if (environment.ToLower() == "production")
            {
                connectionString = $@"Host={DATABASE_URL};Port={port};Database={database};Username={user};Password={password}";
            }
                        
            System.Console.WriteLine(connectionString);
            System.Console.WriteLine(connectionString);
            System.Console.WriteLine(connectionString);
            System.Console.WriteLine(connectionString);
            System.Console.WriteLine(connectionString);
            System.Console.WriteLine(connectionString);
            services.AddEntityFrameworkNpgsql().AddDbContext<WorkoutContext>(options =>
                options.UseNpgsql(connectionString)
            );

            services.AddDbContext<ExerciseContext>(options =>
                options.UseNpgsql(connectionString)
            );

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:4200").AllowAnyHeader();
                    });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WorkoutAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WorkoutAPI v1"));
                app.UseCors();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
