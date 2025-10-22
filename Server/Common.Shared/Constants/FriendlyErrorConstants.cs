namespace Common.Shared.Constants;

/// <summary>
/// Constants used for friendly (client-facing) error messages in the application.
/// </summary>
/// <remarks>Error messages should be standardised, consistent, and not leak any unnecessary information.</remarks>
public static class FriendlyErrorConstants
{
  /// <summary>
  /// The error for when a specific user cannot be found in the database when searching by username.
  /// </summary>
  /// <remarks>Expects a username to be supplied.</remarks>
  public const string UsernameNotFound = "{0} user not found in the database.";

  /// <summary>
  /// The error when a specific user cannot be found in the database when searching by user id.
  /// </summary>
  public const string UserNotFound = "User not found in the database.";

}
// TODO: some ideas:
// 1. We could categorise errors so that certain error messages are replaced with a generic failure error (to not leak information)
// 2. Our logs would always include all relevant information to quickly identify failures