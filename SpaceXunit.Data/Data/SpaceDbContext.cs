using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SpaceXunit.Data.Entities;
using System.Collections.Generic;
using System.Data;

namespace SpaceXunit.Data.Data;

public class SpaceDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    private readonly string? _connectionString;

    public SpaceDbContext(IConfiguration configuration)
    {
        var isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString(isDevelopment ? "SpaceDevDatabase" : "SpaceDatabase");
    }

    public SpaceDbContext(DbContextOptions options) : base(options)
    {
    }

    public static SpaceDbContext CreateInMemoryDatabase()
    {
        var options = new DbContextOptionsBuilder<SpaceDbContext>()
            .UseInMemoryDatabase(databaseName: "SpaceDbMemory")
            .Options;

        return new SpaceDbContext(options);
    }

    public IDbConnection CreateConnection()
        => new SqlConnection(_connectionString);

    public virtual DbSet<User> Users { get; set; }
}
