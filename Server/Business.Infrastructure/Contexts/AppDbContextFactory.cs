namespace Business.Infrastructure.Contexts;

using System.Diagnostics.CodeAnalysis;
using Common.Shared.Constants;
using Common.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

/// <summary>
/// Factory for creating <see cref="AppDbContext"/> instances at design time.
/// </summary>
/// <remarks>Used by Entity Framework tooling for migrations and scaffolding.</remarks>
[ExcludeFromCodeCoverage(Justification = "This logic is used exclusively during local development (design time)")]
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
  /// <summary>
  /// Creates a new instance of <see cref="AppDbContext"/> using the configured connection string.
  /// </summary>
  /// <param name="args">Command-line arguments (not used).</param>
  /// <returns>A configured <see cref="AppDbContext"/> instance.</returns>
  public AppDbContext CreateDbContext(string[] args)
  {
    DbContextOptionsBuilder<AppDbContext> optionsBuilder = new();
    optionsBuilder.UseSqlServer(GetConnectionString());

    return new AppDbContext(optionsBuilder.Options);
  }

  /// <summary>
  /// Retrieves the main database connection string from configuration files and environment variables.
  /// </summary>
  /// <returns>The connection string for the main database.</returns>
  private static string GetConnectionString()
  {
    IConfigurationRoot configuration = new ConfigurationBuilder()
      .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "Application.API"))
      .AddJsonFile("appsettings.json", optional: true)
      .AddJsonFile("appsettings.Development.json", optional: true)
      .AddEnvironmentVariables()
      .Build();

    return configuration.TryGetConnectionString(ApplicationConstants.MainDatabaseConnectionStringName);
  }
}