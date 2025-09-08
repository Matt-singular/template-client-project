namespace Application.API;

using Business.Core.Interfaces;
using Business.Infrastructure.Contexts;
using Common.Shared.Constants;
using Common.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;

/// <summary>
/// Startup extensions for configuring the application pipeline.
/// </summary>
public static class StartupExtensions
{
  /// <summary>
  /// Adds all application-specific services to the service collection.
  /// </summary>
  /// <param name="services">The application's service collection.</param>
  /// <param name="configuration">The application's configuration.</param>
  /// <returns>The service collection with application services registered.</returns>
  public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
  {
    services
      .AddApplicationControllers()
      .AddApplicationDataServices(configuration)
      .AddCommonLogging(configuration)
      .AddCommonSharedServices()
      .AddApplicationModelServices(configuration)
      .AddApplicationSwagger();

    return services;
  }

  /// <summary>
  /// Adds controller and endpoint API explorer services to the service collection.
  /// </summary>
  /// <param name="services">The application's service collection.</param>
  /// <returns>The service collection with controller services registered.</returns>
  private static IServiceCollection AddApplicationControllers(this IServiceCollection services)
  {
    services.AddControllers();
    services.AddEndpointsApiExplorer();

    return services;
  }

  /// <summary>
  /// Adds data-related services, such as DbContext, to the service collection.
  /// </summary>
  /// <param name="services">The application's service collection.</param>
  /// <param name="configuration">The application's configuration.</param>
  /// <returns>The service collection with data services registered.</returns>
  private static IServiceCollection AddApplicationDataServices(this IServiceCollection services, IConfiguration configuration)
  {
    string mainDatabase = configuration.TryGetConnectionString(ApplicationConstants.MainDatabaseConnectionStringName);
    services.AddDbContext<IAppDbContext, AppDbContext>(options => options.UseSqlServer(mainDatabase));

    return services;
  }

  /// <summary>
  /// Adds model-related services and configuration to the service collection.
  /// </summary>
  /// <param name="services">The application's service collection.</param>
  /// <param name="configuration">The application's configuration.</param>
  /// <returns>The service collection with model services registered.</returns>
  private static IServiceCollection AddApplicationModelServices(this IServiceCollection services, IConfiguration configuration)
  {
    //services.Configure<object>(configuration.GetSection("ConfigKeyName")); // Example

    return services;
  }

  /// <summary>
  /// Adds Swagger generation services to the service collection.
  /// </summary>
  /// <param name="services">The application's service collection.</param>
  /// <returns>The service collection with Swagger services registered.</returns>
  private static IServiceCollection AddApplicationSwagger(this IServiceCollection services)
  {
    services.AddSwaggerGen();
    //services.AddSwaggerGen(options =>
    //{
    //    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //    options.IncludeXmlComments(xmlPath);
    //});

    return services;
  }

  /// <summary>
  /// Adds application and environment-specific configuration files and common app settings to the configuration builder.
  /// </summary>
  /// <param name="configBuilder">The configuration builder.</param>
  /// <param name="environment">The web host environment.</param>
  /// <returns>The configuration builder with additional configuration sources.</returns>
  public static IConfigurationBuilder AddApplicationSettings(
  this IConfigurationBuilder configBuilder,
  IWebHostEnvironment environment)
  {
    configBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
    configBuilder.AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
    configBuilder.AddCommonAppSettings(environment.EnvironmentName);

    return configBuilder;
  }

  /// <summary>
  /// Configures the application's HTTP request pipeline.
  /// </summary>
  /// <param name="app">The web application instance.</param>
  /// <returns>The configured web application.</returns>
  public static WebApplication UseApplicationPipeline(this WebApplication app)
  {
    Log.Logger.Information("API started");

    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    return app;
  }
}