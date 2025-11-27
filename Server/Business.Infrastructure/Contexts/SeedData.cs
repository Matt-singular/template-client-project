namespace Business.Infrastructure.Contexts;

using Business.Core.Models.Entities;
using Common.Shared.Constants;

/// <summary>
/// Provides seed data for initializing the application's database context.
/// </summary>
public static class SeedData
{
  /// <summary>
  /// Gets the system application user for seeding.
  /// </summary>
  /// <returns>The system application user.</returns>
  public static ApplicationUser GetApplicationSystemUser()
  {
    return new ApplicationUser()
    {
      UserId = 1,
      FirstName = ApplicationConstants.SystemUserName,
      Surname = ApplicationConstants.SystemUserName,
      Username = ApplicationConstants.SystemUserName,
      Email = $"{ApplicationConstants.SystemUserName}@Application.com",
      CreatedBy = 1,
      UpdatedBy = 1,
      CreatedOn = new DateTime(2025, 9, 20),
      UpdatedOn = new DateTime(2025, 9, 20),
    };
  }
}