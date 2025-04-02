namespace Common.Shared.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

/// <summary>
/// Common & shared startup extension methods.
/// </summary>
public static class StartupExtensions
{
  /// <summary>
  /// Sets up the application configuration.
  /// </summary>
  /// <param name="config">The configuration builder.</param>
  /// <returns>The configuration builder with the appsettings and user secrets configured.</returns>
  public static IConfigurationBuilder AddCommonAppSettings(this IConfigurationBuilder config)
  {
    var assemblyLocation = Path.GetDirectoryName(typeof(Startup).Assembly.Location);
    var appSettingsPath = Path.Combine(assemblyLocation!, "appsettings.Common.json");
    config.AddJsonFile(appSettingsPath, optional: true, reloadOnChange: true).AddUserSecrets<Startup>(optional: true, reloadOnChange: true);

    return config;
  }

  /// <summary>
  /// Configures the various config setting models.
  /// </summary>
  /// <param name="services">The application's service collection.</param>
  /// <param name="configuration">The application's configuration.</param>
  /// <returns>The application's service collection with the setting objects configured.</returns>
  public static IServiceCollection ConfigureCommonSettings(this IServiceCollection services, IConfiguration configuration)
  {
    //services.Configure<object>(configuration.GetSection("ConfigKeyName")); // Example

    return services;
  }

  /// <summary>
  /// Adds Common.Shared services to the service collection.
  /// </summary>
  /// <param name="services">The application's service collection.</param>
  /// <returns>The service collection with Common.Shared services registered.</returns>
  public static IServiceCollection AddCommonSharedServices(this IServiceCollection services)
  {
    //services.AddScoped<IService, Service>(); // Example

    return services;
  }

  public static IServiceCollection AddCommonLogging(this IServiceCollection services, IConfiguration configuration)
  {
    LoggerConfiguration loggerConfig = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(configuration);

    Log.Logger = loggerConfig.CreateLogger();

    return services;
  }
}