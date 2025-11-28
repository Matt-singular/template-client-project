namespace Application.API.Services;

using System.Security.Claims;
using Business.Core.Domains.ApplicationUsers.Session;
using Common.Shared.Exceptions;

/// <inheritdoc cref="IUserSessionService"/>
/// <param name="httpContextAccessor">Used to access the <see cref="HttpContext"/> for the current request.</param>
public class UserSessionService(IHttpContextAccessor httpContextAccessor) : IUserSessionService
{
  private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;

  /// <inheritdoc cref="IUserSessionService.SetSessionToSystemUser"/>
  public void SetSessionToSystemUser()
  {
    throw new NotImplementedException();
  }

  /// <inheritdoc cref="IUserSessionService.GetSessionUserId"/>
  /// <remarks>Gets the session user's Id from the token claims.</remarks>
  public int? GetSessionUserId()
  {
    // Get the current user from the HttpContext
    ClaimsPrincipal? user = this.httpContextAccessor.HttpContext?.User;
    InvalidOperationException.ThrowIfNull(user);

    // Retrieve the NameIdentifier claim which holds the user Id
    Claim? userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
    InvalidOperationException.ThrowIfNull(userIdClaim, "User Id claim is missing in the token.");

    // Parse and return the user Id as an integer
    return ConvertToInt(userIdClaim.Value) ?? throw new InvalidOperationException("User Id claim is not a valid integer.");
  }

  private static int? ConvertToInt(string value)
  {
    if (int.TryParse(value, out int result))
    {
      return result;
    }

    return null;
  }
}