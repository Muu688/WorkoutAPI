using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutAPI
{
    public class ConfigurationService : IConfigurationService {
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _webHostEnvironment;
    
    public ConfigurationService(IConfiguration configuration, IWebHostEnvironment webHostEnvironment) {
        _configuration = configuration;
        _webHostEnvironment = webHostEnvironment;
    }

    private string GetHerokuConnectionString() {
        // Get the connection string from the ENV variables
        string connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

        // parse the connection string
        var databaseUri = new Uri(connectionUrl);

        string db = databaseUri.LocalPath.TrimStart('/');
        string[] userInfo = databaseUri.UserInfo.Split(':', StringSplitOptions.RemoveEmptyEntries);

        return $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};Port={databaseUri.Port};Database={db};Pooling=true;SSL Mode=Require;Trust Server Certificate=True;";
    }

    public string DatabaseConnectionString =>
        _webHostEnvironment.IsDevelopment()
            ? _configuration.GetConnectionString("DefaultConnection")
            : GetHerokuConnectionString();
}

    public interface IConfigurationService
    {
    }
}
