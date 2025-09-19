namespace Common.Shared.Extensions;

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

/// <summary>
/// Common &amp; shared startup extension methods.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "Startup logic does not need to be tested")]
public static class StartupExtensions
{
  /// <summary>
  /// Sets up the application configuration.
  /// </summary>
  /// <param name="config">The configuration builder.</param>
  /// <param name="environmentName">The environment name.</param>
  /// <returns>The configuration builder with the appsettings and user secrets configured.</returns>
  public static IConfigurationBuilder AddCommonAppSettings(
    this IConfigurationBuilder config,
    string environmentName)
  {
    var assemblyLocation = Path.GetDirectoryName(typeof(Startup).Assembly.Location);
    var appSettingsPath = Path.Combine(assemblyLocation!, "appsettings.Common.json");
    config.AddJsonFile(appSettingsPath, optional: true, reloadOnChange: true).AddUserSecrets<Startup>(optional: true, reloadOnChange: true);

    var appSettingsEnvironmentPath = Path.Combine(assemblyLocation!, $"appsettings.Common.{environmentName}.json");
    config.AddJsonFile(appSettingsEnvironmentPath, optional: true, reloadOnChange: true);

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

  /// <summary>
  /// Adds and configures Serilog logging.
  /// </summary>
  /// <param name="services">The application's service collection.</param>
  /// <param name="configuration">The application's configuration.</param>
  /// <returns>The service collection with logging.</returns>
  public static IServiceCollection AddCommonLogging(this IServiceCollection services, IConfiguration configuration)
  {
    LoggerConfiguration loggerConfig = new LoggerConfiguration()
      .Enrich.FromLogContext()
      .ReadFrom.Configuration(configuration);

    Log.Logger = loggerConfig.CreateLogger();

    return services;
  }

  /// <summary>
  /// Gets a connection string by name and throws an exception if not found or empty.
  /// </summary>
  /// <param name="configuration">The application's configuration.</param>
  /// <param name="name">The connection strign name.</param>
  /// <returns>The connection string.</returns>
  public static string TryGetConnectionString(this IConfiguration configuration, string name)
  {
    var connectionString = configuration.GetConnectionString(name);
    ArgumentException.ThrowIfNullOrWhiteSpace(connectionString, nameof(connectionString));

    return connectionString;
  }
}