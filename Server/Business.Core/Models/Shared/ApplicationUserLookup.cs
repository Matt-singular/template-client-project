namespace Business.Core.Models.Shared;

using Business.Core.Models.Entities;

/// <summary>
/// The application user lookup.
/// </summary>
/// <remarks>Contains details mapped from the <see cref="ApplicationUser"/> entity.</remarks>
public class ApplicationUserLookup
{
  /// <inheritdoc cref="ApplicationUser.UserId"/>
  public int UserId { get; set; }

  /// <inheritdoc cref="ApplicationUser.FirstName"/>
  public required string FirstName { get; set; }

  /// <inheritdoc cref="ApplicationUser.Surname"/>
  public required string Surname { get; set; }

  /// <inheritdoc cref="ApplicationUser.Username"/>
  public required string UserName { get; set; }

  /// <inheritdoc cref="ApplicationUser.Email"/>
  public required string Email { get; set; }

  /// <inheritdoc cref="ApplicationUser.IsActive"/>
  public bool IsActive { get; set; }
}
