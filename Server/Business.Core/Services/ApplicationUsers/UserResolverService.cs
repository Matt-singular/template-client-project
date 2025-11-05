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
      ?? throw new InvalidOperationException($"{ApplicationConstants.SystemUserName} user not found in the database.");

    return systemUser;
  }
}