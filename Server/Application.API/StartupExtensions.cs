namespace Application.API;

using System.Diagnostics.CodeAnalysis;
using Application.API.Services;
using Business.Core.Domains.ApplicationUsers.Session;
using Business.Core.Interfaces;
using Business.Infrastructure.Contexts;
using Common.Shared.Constants;
using Common.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;

/// <summary>
/// Startup extensions for configuring the application pipeline.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "Startup logic does not need to be tested")]
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
      .AddApplicationDomainServices(configuration)
      .AddSharedDomainServices(configuration)
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
    string mainDatabase = configuration.TryGetConnectionString(DatabaseConstants.MainDatabaseConnectionStringName);
    services.AddDbContext<IAppDbContext, AppDbContext>(options => options.UseSqlServer(mainDatabase));

    return services;
  }

  /// <summary>
  /// Adds application domain services and configuration to the service collection.
  /// </summary>
  /// <param name="services">The application's service collection.</param>
  /// <param name="configuration">The application's configuration.</param>
  /// <returns>The service collection with application domain services registered.</returns>
  private static IServiceCollection AddApplicationDomainServices(this IServiceCollection services, IConfiguration configuration)
  {
    //services.Configure<object>(configuration.GetSection("ConfigKeyName")); // Example

    return services;
  }

  /// <summary>
  /// Adds shared domain services to the service collection.
  /// </summary>
  /// <param name="services">The application's service collection.</param>
  /// <param name="configuration">The application's configuration.</param>
  /// <returns>The service collection with shared domain services registered.</returns>
  private static IServiceCollection AddSharedDomainServices(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddScoped<IUserSessionService, UserSessionService>();

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