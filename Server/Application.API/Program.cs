using Application.API;

var builder = WebApplication.CreateBuilder(args);

// Dependency Injection
builder.Configuration.AddApplicationSettings(builder.Environment);
builder.Services.AddApplicationServices(builder.Configuration);

// Build app
var app = builder.Build();

// Middleware
app.UseApplicationPipeline();

app.Run();