using System.Reflection;
using Common.Shared.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configurations
builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddCommonAppSettings();
builder.Services.ConfigureCommonSettings(builder.Configuration);

// Dependency Injection
builder.Services.AddCommonSharedServices();
builder.Services.AddCommonLogging(builder.Configuration);

builder.Services.AddSwaggerGen();
//builder.Services.AddSwaggerGen(options =>
//{
//  // Add Comments to Swagger
//  var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//  var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
//  options.IncludeXmlComments(xmlPath);
//});

var app = builder.Build();
Log.Logger.Information("API started");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
