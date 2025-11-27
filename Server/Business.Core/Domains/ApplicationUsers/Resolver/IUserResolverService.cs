namespace Business.Core.Domains.ApplicationUsers.Resolver;

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

  /// <summary>
  /// Retrieves the user for the current session and then uses this detail to perform a database lookup.
  /// </summary>
  /// <returns>The <see cref="ApplicationUser"/> representing the session user.</returns>
  public ApplicationUser GetSessionUser();

  /// <summary>
  /// Validates whether an application user exists with the specified <paramref name="userId"/>.
  /// </summary>
  /// <param name="userId">The application user's Id to validate.</param>
  /// <returns><see langword="true"/> if the user exists; otherwise, <see langword="false"/>.</returns>
  public Task<bool> ValidateApplicationUserExistsAsync(int userId);

  /// <summary>
  /// Validates whether an application user exists with the specified <paramref name="username"/>.
  /// </summary>
  /// <param name="username">The application user's userName to validate.</param>
  /// <returns><see langword="true"/> if the user exists; otherwise, <see langword="false"/>.</returns>
  public Task<bool> ValidateApplicationUserExistsAsync(string username);
}