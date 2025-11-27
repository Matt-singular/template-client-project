namespace Business.Tests._TestHelpers.SeedData;

using Business.Core.Models.Entities;
using Business.Infrastructure.Contexts;

/// <summary>
/// Test data for the <see cref="ApplicationUser"/> entity.
/// </summary>
public static class ApplicationUserTestData
{
  /// <summary>
  /// Gets a list of <see cref="ApplicationUser"/> instances for use in unit tests.
  /// </summary>
  /// <returns>The <see cref="ApplicationUser"/> entities used for testing.</returns>
  public static List<ApplicationUser> GetTestUsers()
  {
    return
    [
      SeedData.GetApplicationSystemUser(),
      new ApplicationUser
      {
        UserId = 2,
        FirstName = "Alan",
        Surname = "Turing",
        Username = "AlanTuring",
        Email = "AlanTuring@TestMail.com"
      }.SetAuditFields(),
      new ApplicationUser
      {
        UserId = 3,
        FirstName = "Megan",
        Surname = "Smith",
        Username = "MeganSmith",
        Email = "MeganSmith@TestMail.com"
      }.SetAuditFields(),
    ];
  }
}