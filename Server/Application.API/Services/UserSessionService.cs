namespace Application.API.Services;

using Business.Core.Domains.ApplicationUsers.Session;

/// <inheritdoc cref="IUserSessionService"/>
public class UserSessionService : IUserSessionService
{
  /// <inheritdoc cref="IUserSessionService.SetSessionToSystemUser"/>
  public void SetSessionToSystemUser()
  {
    throw new NotImplementedException();
  }

  /// <inheritdoc cref="IUserSessionService.GetSessionUserId"/>
  /// <remarks>Gets the session user's Id from the token claims.</remarks>
  public int? GetSessionUserId()
  {
    throw new NotImplementedException();
  }
}
