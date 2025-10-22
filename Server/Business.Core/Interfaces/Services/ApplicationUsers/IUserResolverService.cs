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

  /// <summary>
  /// Retrieves the specified user from the application users collection.
  /// </summary>
  /// <param name="userId">The application user's Id to filter on.</param>
  /// <returns>The <see cref="ApplicationUser"/> representing the specified user.</returns>
  public ApplicationUser GetUserById(int userId);
}