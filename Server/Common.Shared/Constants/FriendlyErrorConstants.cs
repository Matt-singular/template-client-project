namespace Common.Shared.Constants;

/// <summary>
/// Constants used for friendly (client-facing) error messages in the application.
/// </summary>
/// <remarks>Error messages should be standardised, consistent, and not leak any unnecessary information.</remarks>
public static class FriendlyErrorConstants
{
  /// <summary>
  /// The error for when a user cannot be found in the database.
  /// </summary>
  /// <remarks>Expects a user name to be supplied.</remarks>
  public const string UserNotFound = "{0} user not found in the database.";
}