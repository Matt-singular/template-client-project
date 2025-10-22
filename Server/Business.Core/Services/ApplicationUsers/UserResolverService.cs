namespace Business.Core.Services.ApplicationUsers;

using Business.Core.Interfaces;
using Business.Core.Interfaces.Services.ApplicationUsers;
using Business.Core.Models.Entities;
using Common.Shared.Constants;

/// <inheritdoc cref="IUserResolverService"/>
public class UserResolverService(IAppDbContext dbContext) : IUserResolverService
{
  /// <inheritdoc/>
  /// <exception cref="InvalidOperationException">Thrown if the system user does not exist in the database.</exception>
  public ApplicationUser GetSystemUser()
  {
    ApplicationUser? systemUser = dbContext.ApplicationUsers.FirstOrDefault(user => user.UserName.Equals(ApplicationConstants.SystemUserName))
      ?? throw new InvalidOperationException(string.Format(FriendlyErrorConstants.UsernameNotFound, ApplicationConstants.SystemUserName));

    return systemUser;
  }

  /// <inheritdoc/>
  /// <exception cref="InvalidOperationException">Thrown if the user does not exist in the database.</exception>
  public ApplicationUser GetUserById(int userId)
  {
    // TODO: reconsider the exception type (perhaps add a custom db exception to better identify these issues in logs
    ApplicationUser? user = dbContext.ApplicationUsers.FirstOrDefault(user => user.UserId == userId)
      ?? throw new InvalidOperationException(FriendlyErrorConstants.UserNotFound);

    return user;
  }
}