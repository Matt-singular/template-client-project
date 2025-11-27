namespace Business.Core.Domains.ApplicationUsers.Session;

using Common.Shared.Constants;

/// <summary>
/// Provides methods for resolving and overriding the current user session details.
/// </summary>
public interface IUserSessionService
{
  /// <summary>
  /// Sets the current user session context to the system user, identified by <see cref="ApplicationConstants.SystemUserName"/>.
  /// </summary>
  /// <remarks>This is typically used for operations that require system-level privileges or background processing.</remarks>
  void SetSessionToSystemUser();

  /// <summary>
  /// Gets the current session user's Id.
  /// </summary>
  /// <returns>The user Id for the current session, or null if not found.</returns>
  int? GetSessionUserId();
}