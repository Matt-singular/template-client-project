namespace Business.Tests._TestHelpers.Mocks;

using Application.API.Services;
using Business.Core.Domains.ApplicationUsers.Session;

/// <inheritdoc cref="IUserSessionService"/>
/// <remarks>This mock implementation is used for testing purposes.</remarks>
public class MockUserSessionService : UserSessionService
{
  /// <inheritdoc/>
  public new int? GetSessionUserId()
  {
    return base.GetSessionUserId();
  }

  /// <inheritdoc/>
  public new void SetSessionToSystemUser()
  {
    base.SetSessionToSystemUser();
  }
}