namespace Business.Tests.Services.ApplicationUsers;

using Business.Core.Interfaces.Services.ApplicationUsers;
using Business.Core.Models.Entities;
using Business.Core.Services.ApplicationUsers;
using Business.Infrastructure.Contexts;
using Common.Shared.Constants;

/// <summary>
/// Unit tests for the <see cref="IUserResolverService"/>.
/// </summary>
public class UserResolverServiceUnitTests
{
  /// <summary>
  /// Tests that <see cref="UserResolverService.GetSystemUser"/> returns the expected system user.
  /// </summary>
  /// <remarks>
  /// This test verifies that the system user is correctly retrieved from the database context
  /// and that its properties match the expected values defined in <see cref="SeedData.GetApplicationSystemUser"/>.
  /// </remarks>
  [Fact]
  public async Task GetSystemUser_ShouldReturnSystemUser()
  {
    // Arrange
    using AppDbContext dbContext = await TestHelpers // TODO: will change how this is done.
      .CreateInMemoryDbContext()
      .AddTestSeedDataAsync();
    UserResolverService userResolverService = new(dbContext);

    // Act
    ApplicationUser systemUser = Assert.IsType<ApplicationUser>(userResolverService.GetSystemUser());

    // Assert
    Assert.Equal(1, systemUser.UserId);
    Assert.Equal(ApplicationConstants.SystemUserName, systemUser.UserName);
  }

  /// <summary>
  /// Tests the error handling of <see cref="UserResolverService.GetSystemUser"/> when the system user is not configured.
  /// </summary>
  /// <remarks>This test verifies that there is sufficient error handling should the service not find a configured system user.</remarks>
  [Fact]
  public async Task GetSystemUser_WhenUserNotConfigured_ShouldThrowException()
  {
    // Arrange
    string expectedError = string.Format(FriendlyErrorConstants.UserNotFound, ApplicationConstants.SystemUserName);
    using AppDbContext dbContext = TestHelpers.CreateInMemoryDbContext(); // TODO: will change how this is done
    UserResolverService userResolverService = new(dbContext);

    // Act
    InvalidOperationException actualError = Assert.Throws<InvalidOperationException>(userResolverService.GetSystemUser);

    // Assert
    Assert.Equal(expectedError, actualError.Message);
  }
}