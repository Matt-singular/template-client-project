namespace Common.Shared.Constants;

/// <summary>
/// Constants used for database configuration.
/// </summary>
public static class DatabaseConstants
{
  /// <summary>
  /// The name of the main database connection string.
  /// </summary>
  public const string MainDatabaseConnectionStringName = "Main";

  /// <summary>
  /// The collation used for the main database.
  /// <list type="bullet">
  ///   <item><description>Latin1_General: Specifies the character set and language rules (Western European languages).</description></item>
  ///   <item><description>CP1: Code page 1252, which defines the character encoding.</description></item>
  ///   <item><description>CI: Case-insensitive.</description></item>
  ///   <item><description>AS: Accent-sensitive.</description></item>
  /// </list>
  /// </summary>
  public const string MainDatabaseCollation = "SQL_Latin1_General_CP1_CI_AS";
}