namespace Application.GUI;

using Common.Shared.Extensions;

/// <summary>
/// Startup logic for configuring the application pipeline.
/// </summary>
public static class MauiProgram
{
  /// <summary>
  /// Creates and configures the MAUI application.
  /// </summary>
  /// <returns>A configured <see cref="MauiApp"/> instance.</returns>
  public static MauiApp CreateMauiApp()
  {
    var builder = MauiApp.CreateBuilder();

    // Configurations
    string environmentName = GetEnvironmentName();
    builder.Configuration.AddCommonAppSettings(environmentName);
    builder.Services.ConfigureCommonSettings(builder.Configuration);

    // Dependency Injection
    builder.Services.AddMauiPages();
    builder.Services.AddCommonSharedServices();
    builder.Services.AddCommonLogging(builder.Configuration);

    // Maui App
    builder.ConfigureMauiFonts();

    // Application Startup
    var app = builder.Build();

    return app;
  }

  private static IServiceCollection AddMauiPages(this IServiceCollection services)
  {
    services.AddSingleton<MainPage>();

    return services;
  }

  private static MauiAppBuilder ConfigureMauiFonts(this MauiAppBuilder builder)
  {
    builder
      .UseMauiApp<App>()
      .ConfigureFonts(fonts =>
      {
        fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
        fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
      });

    return builder;
  }

  /// <summary>
  /// Gets the environment name based on the build configuration.
  /// </summary>
  /// <remarks>This is because .NET MAUI does not have built-in support for environment variables like ASP.NET Core.</remarks>
  /// <returns>The environment name.</returns>
  private static string GetEnvironmentName()
  {
#if DEBUG
    return "Development";
#else
    return "Production";
#endif
  }
}