using Application.Activities.Queries;
using Application.Core;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
{
   options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")); 
});
builder.Services.AddCors();
builder.Services
   .AddMediatR(options => options.RegisterServicesFromAssemblyContaining<GetActivityList.Handler>());
builder.Services
   .AddAutoMapper(options => options.AddMaps(typeof(MappingProfiles).Assembly));



var app = builder.Build();

// Configure HTTP Request pipeline
app.UseCors(options =>
{
   options.WithOrigins("http://localhost:3000", "https://localhost:3000")
      .AllowAnyHeader()
      .AllowAnyMethod();
});

app.MapControllers();

using var scope = app.Services.CreateScope(); // Creates a scope in order to use services
var services = scope.ServiceProvider;
try
{
   var context = services.GetRequiredService<AppDbContext>();
   await context.Database.MigrateAsync(); // Performs dotnet database update automatically
   await DbInitializer.SeedData(context);
}
catch (Exception ex)
{
   var logger = services.GetRequiredService<ILogger<Program>>();
   logger.LogError(ex, "An error has occurred during migration");
   throw;
}
app.Run();
