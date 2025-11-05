namespace Business.Core.Services.ApplicationUsers;

using Business.Core.Interfaces;
using Business.Core.Interfaces.Services.ApplicationUsers;
using Business.Core.Models.Entities;
using Common.Shared.Constants;
using Common.Shared.Models.Exceptions;

/// <inheritdoc cref="IUserResolverService"/>
public class UserResolverService(IAppDbContext dbContext) : IUserResolverService
{
  /// <inheritdoc/>
  /// <exception cref="SqlDataNotFoundException">Thrown if the system user does not exist in the database.</exception>
  public ApplicationUser GetSystemUser()
  {
    ApplicationUser? systemUser = dbContext.ApplicationUsers.FirstOrDefault(user => user.UserName.Equals(ApplicationConstants.SystemUserName))
      ?? throw new SqlDataNotFoundException(string.Format(FriendlyErrorConstants.UsernameNotFound, ApplicationConstants.SystemUserName));

    return systemUser;
  }

  /// <inheritdoc/>
  /// <exception cref="SqlDataNotFoundException">Thrown if the user does not exist in the database.</exception>
  public ApplicationUser GetUserById(int userId)
  {
    ApplicationUser? user = dbContext.ApplicationUsers.FirstOrDefault(user => user.UserId == userId)
      ?? throw new SqlDataNotFoundException(FriendlyErrorConstants.UserNotFound);

    return user;
  }
}