namespace Application.GUI;

using Common.Shared.Extensions;

public static class MauiProgram
{
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
    builder
      .UseMauiApp<App>()
      .ConfigureFonts(fonts =>
      {
        fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
        fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
      });

    // Application Startup
    var app = builder.Build();

    return app;
  }

  private static IServiceCollection AddMauiPages(this IServiceCollection services)
  {
    services.AddSingleton<MainPage>();

    return services;
  }

  private static string GetEnvironmentName()
  {
#if DEBUG
    return "Development";
#else
    return "Production";
#endif
  }
}