namespace Common.Shared.Constants;

/// <summary>
/// Constants used for path configuration.
/// </summary>
public static class PathConstants
{
  private static readonly string SolutionRoot = Path.Combine(Directory.GetCurrentDirectory(), "..");

  /// <summary>
  /// The path to the Application.API project.
  /// </summary>
  public static readonly string ApplicationAPI = Path.Combine(SolutionRoot, "Application.API");

  /// <summary>
  /// The path to the Common.Shared project.
  /// </summary>
  public static readonly string CommonShared = Path.Combine(SolutionRoot, "Common.Shared");
}
