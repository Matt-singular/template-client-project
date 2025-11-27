namespace Business.Core.Domains.ApplicationUsers.Resolver;

using Business.Core.Domains.ApplicationUsers.Session;
using Business.Core.Interfaces;
using Business.Core.Models.Entities;
using Common.Shared.Constants;
using Common.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

/// <inheritdoc cref="IUserResolverService"/>
/// <param name="dbContext">The application's database context.</param>
/// <param name="userSessionService">The user session service for resolving and overriding session details.</param>
public class UserResolverService(IAppDbContext dbContext, IUserSessionService userSessionService) : IUserResolverService
{
  private readonly IAppDbContext dbContext = dbContext;
  private readonly IUserSessionService userSessionService = userSessionService;

  /// <inheritdoc/>
  /// <exception cref="SqlDataNotFoundException">Thrown if the system user does not exist in the database.</exception>
  public ApplicationUser GetSystemUser()
  {
    ApplicationUser? systemUser = this.dbContext.ApplicationUsers.FirstOrDefault(user => user.Username.Equals(ApplicationConstants.SystemUserName))
      ?? throw new SqlDataNotFoundException(string.Format(FriendlyErrorConstants.UsernameNotFound, ApplicationConstants.SystemUserName));

    return systemUser;
  }

  /// <inheritdoc/>
  /// <exception cref="SqlDataNotFoundException">Thrown if the user does not exist in the database.</exception>
  public ApplicationUser GetUserById(int userId)
  {
    ApplicationUser? user = this.dbContext.ApplicationUsers.FirstOrDefault(user => user.UserId == userId)
      ?? throw new SqlDataNotFoundException(FriendlyErrorConstants.UserNotFound);

    return user;
  }

  /// <inheritdoc/>
  /// <exception cref="InvalidOperationException">Thrown if the user from the current session could not be found.</exception>
  /// <exception cref="SqlDataNotFoundException">Thrown if the user does not exist in the database.</exception>
  public ApplicationUser GetSessionUser()
  {
    int sessionUserId = this.userSessionService.GetSessionUserId() ?? throw new InvalidOperationException(FriendlyErrorConstants.UserSessionNotFound);

    return this.GetUserById(sessionUserId);
  }

  /// <inheritdoc/>
  public Task<bool> ValidateApplicationUserExistsAsync(int userId)
  {
    return this.dbContext.ApplicationUsers.AnyAsync(user => user.UserId == userId);
  }
  /// <inheritdoc/>
  public Task<bool> ValidateApplicationUserExistsAsync(string username)
  {
    return this.dbContext.ApplicationUsers.AnyAsync(user => user.Username == username);
  }
}