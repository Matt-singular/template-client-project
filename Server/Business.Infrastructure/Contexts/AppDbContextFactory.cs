namespace Business.Infrastructure.Contexts;

using Common.Shared.Constants;
using Common.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
  public AppDbContext CreateDbContext(string[] args)
  {
    DbContextOptionsBuilder<AppDbContext> optionsBuilder = new();
    optionsBuilder.UseSqlServer(GetConnectionString());

    return new AppDbContext(optionsBuilder.Options);
  }

  private static string GetConnectionString()
  {
    IConfigurationRoot configuration = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.Development.json")
      .Build();

    return configuration.TryGetConnectionString(ApplicationConstants.MainDatabaseConnectionStringName);
  }
}