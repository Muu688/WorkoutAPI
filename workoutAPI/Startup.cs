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

        #region stuff

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

        #endregion stuff

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString;
            string environment = Environment.GetEnvironmentVariable("Environment") ?? "";

            if (environment.ToLower() == "production")
                connectionString = GetHerokuConnectionString();
            else
                connectionString = Configuration.GetConnectionString("WORKOUT");

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
                        string[] allowedHosts = { "https://localhost:4200", "https://workout-angular-jvb.herokuapp.com" };  //builder.WithOrigins("https://localhost:4200").AllowAnyHeader();
                        builder.WithOrigins(allowedHosts).AllowAnyHeader();
                    });
            });
            // https://devcenter.heroku.com/articles/connecting-to-heroku-postgres-databases-from-outside-of-heroku
            // https://n1ghtmare.github.io/2020-09-28/deploying-a-dockerized-aspnet-core-app-using-a-postgresql-db-to-heroku/
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
