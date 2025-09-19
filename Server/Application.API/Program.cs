namespace Application.API;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Application entry point.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "Startup logic does not need to be tested")]
public static class Program
{
  /// <summary>
  /// Main method for the application. Configures services, builds the web application, and runs the middleware pipeline.
  /// </summary>
  /// <param name="args">Command-line arguments.</param>
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    // Dependency Injection
    builder.Configuration.AddApplicationSettings(builder.Environment);
    builder.Services.AddApplicationServices(builder.Configuration);

    // Build app
    var app = builder.Build();

    // Middleware
    app.UseApplicationPipeline();

    app.Run();
  }
}