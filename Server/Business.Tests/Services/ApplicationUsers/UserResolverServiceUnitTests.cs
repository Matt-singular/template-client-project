namespace Business.Tests.Services.ApplicationUsers;

using Business.Core.Domains.ApplicationUsers.Resolver;
using Business.Core.Models.Entities;
using Business.Infrastructure.Contexts;
using Business.Tests._TestHelpers.Mocks;
using Business.Tests._TestHelpers.SeedData;
using Common.Shared.Constants;
using Common.Shared.Exceptions;

/// <summary>
/// Unit tests for the <see cref="IUserResolverService"/>.
/// </summary>
public class UserResolverServiceUnitTests
{
  private static UserResolverService InstantiateUserResolverService(AppDbContext dbContext)
  {
    MockUserSessionService mockUserSessionService = new();
    UserResolverService userResolverService = new(dbContext, mockUserSessionService);

    return userResolverService;
  }

  /// <summary>
  /// Tests that <see cref="UserResolverService.GetSystemUser"/> returns the expected system user.
  /// </summary>
  /// <remarks>
  /// This test verifies that the system user is correctly retrieved from the database context
  /// and that its properties match the expected values defined in <see cref="SeedData.GetApplicationSystemUser"/>.
  /// </remarks>
  [Fact]
  public async Task GetSystemUser_WhenUserConfigured_ShouldReturnSystemUser()
  {
    // Arrange
    using AppDbContext dbContext = await TestHelpers.CreateInMemoryDbContext().AddTestSeedDataAsync();
    UserResolverService userResolverService = InstantiateUserResolverService(dbContext);

    // Act
    ApplicationUser systemUser = Assert.IsType<ApplicationUser>(userResolverService.GetSystemUser());

    // Assert
    Assert.Equal(1, systemUser.UserId);
    Assert.Equal(ApplicationConstants.SystemUserName, systemUser.Username);
  }

  /// <summary>
  /// Tests the error handling of <see cref="UserResolverService.GetSystemUser"/> when the system user is not configured.
  /// </summary>
  /// <remarks>This test verifies that there is sufficient error handling should the service not find a configured system user.</remarks>
  [Fact]
  public async Task GetSystemUser_WhenUserNotConfigured_ShouldThrowException()
  {
    // Arrange
    string expectedError = string.Format(FriendlyErrorConstants.UsernameNotFound, ApplicationConstants.SystemUserName);
    using AppDbContext dbContext = TestHelpers.CreateInMemoryDbContext();
    UserResolverService userResolverService = InstantiateUserResolverService(dbContext);

    // Act
    SqlDataNotFoundException actualError = Assert.Throws<SqlDataNotFoundException>(userResolverService.GetSystemUser);

    // Assert
    Assert.Equal(expectedError, actualError.Message);
  }


  /// <summary>
  /// Tests that <see cref="UserResolverService.GetUserById(int)"/> returns the specified user.
  /// </summary>
  /// <remarks>
  /// This test verifies that the specified user is correctly retrieved from the database context
  /// and that its properties match the expected values defined in <see cref="ApplicationUserTestData.GetTestUsers"/>.
  /// </remarks>
  [Fact]
  public async Task GetUserById_WhenUserExists_ShouldReturnSpecifiedUser()
  {
    // Arrange
    int expectedUserId = 2;
    string expectedUsername = "AlanTuring";
    using AppDbContext dbContext = await TestHelpers.CreateInMemoryDbContext().AddTestSeedDataAsync();
    UserResolverService userResolverService = InstantiateUserResolverService(dbContext);

    // Act
    ApplicationUser specifiedUser = Assert.IsType<ApplicationUser>(userResolverService.GetUserById(expectedUserId));

    // Assert
    Assert.Equal(expectedUserId, specifiedUser.UserId);
    Assert.Equal(expectedUsername, specifiedUser.Username);
  }

  /// <summary>
  /// Tests the error handling of <see cref="UserResolverService.GetUserById(int)"/> when the user does not exist.
  /// </summary>
  /// <remarks>This test verifies that there is sufficient error handling should the service not find the specified user.</remarks>
  [Fact]
  public async Task GetUserById_WhenUserDoesNotExist_ShouldThrowException()
  {
    // Arrange
    int nonExistentUserId = -1;
    string expectedError = FriendlyErrorConstants.UserNotFound;
    using AppDbContext dbContext = TestHelpers.CreateInMemoryDbContext();
    UserResolverService userResolverService = InstantiateUserResolverService(dbContext);

    // Act
    SqlDataNotFoundException actualError = Assert.Throws<SqlDataNotFoundException>(() => userResolverService.GetUserById(nonExistentUserId));

    // Assert
    Assert.Equal(expectedError, actualError.Message);
  }
}