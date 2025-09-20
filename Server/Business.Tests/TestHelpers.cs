namespace Business.Tests;

using Business.Infrastructure.Contexts;
using Business.Tests._TestHelpers.SeedData;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Provides helper methods for test setup.
/// </summary>
public static class TestHelpers
{
  /// <summary>
  /// Creates an in-memory <see cref="AppDbContext"/> for testing.
  /// </summary>
  /// <returns>A new instance of <see cref="AppDbContext"/> using the in-memory database provider.</returns>
  public static AppDbContext CreateInMemoryDbContext()
  {
    DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
      .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
      .Options;

    return new AppDbContext(options);
  }

  /// <summary>
  /// Adds test seed data to the specified <see cref="AppDbContext"/> instance.
  /// </summary>
  /// <param name="dbContext">The <see cref="AppDbContext"/> to seed with test data.</param>
  /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
  public static async Task<AppDbContext> AddTestSeedDataAsync(this AppDbContext dbContext)
  {
    List<Task> tasks =
    [
      dbContext.ApplicationUsers.AddRangeAsync(ApplicationUserTestData.GetTestUsers()),
    ];

    await Task.WhenAll(tasks).ConfigureAwait(false);
    await dbContext.SaveChangesAsync().ConfigureAwait(false);

    return dbContext;
  }
}