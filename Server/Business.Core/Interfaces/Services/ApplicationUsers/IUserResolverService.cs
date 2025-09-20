namespace Business.Core.Interfaces.Services.ApplicationUsers;

using Business.Core.Models.Entities;

/// <summary>
/// Provides methods for resolving application users from the database context.
/// </summary>
public interface IUserResolverService
{
  /// <summary>
  /// Retrieves the system user from the application users collection.
  /// </summary>
  /// <returns>The <see cref="ApplicationUser"/> representing the system user.</returns>
  public ApplicationUser GetSystemUser();
}