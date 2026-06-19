using API.Middleware;
using API.SignalR;
using Application.Activities.Queries;
using Application.Activities.Validators;
using Application.Core;
using Application.Interfaces;
using Domain;
using FluentValidation;
using Infrastructure.Photos;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
   var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
   options.Filters.Add(new AuthorizeFilter(policy));
});
builder.Services.AddDbContext<AppDbContext>(options =>
{
   options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors();
builder.Services.AddSignalR();
builder.Services
   .AddMediatR(options =>
   {
      options.RegisterServicesFromAssemblyContaining<GetActivityList.Handler>();
      options.AddOpenBehavior(typeof(ValidationBehavior<,>));
   });

builder.Services.AddScoped<IUserAccessor, UserAccessor>();

builder.Services
   .AddAutoMapper(options => options.AddMaps(typeof(MappingProfiles).Assembly));
builder.Services
   .AddValidatorsFromAssemblyContaining<CreateActivityValidator>();
builder.Services.AddTransient<ExceptionMiddleware>();

builder.Services.AddIdentityApiEndpoints<User>(options =>
{
   options.User.RequireUniqueEmail = true;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthorization(options =>
{
   options.AddPolicy("IsActivityHost", policy =>
   {
      policy.Requirements.Add(new IsHostRequirement());
   });
});
builder.Services.AddTransient<IAuthorizationHandler, IsHostRequirementHandler>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

var app = builder.Build();

// Configure HTTP Request pipeline
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(options =>
{
   options.WithOrigins("http://localhost:3000", "https://localhost:3000")
      .AllowAnyHeader()
      .AllowAnyMethod()
      .AllowCredentials();
});
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGroup("api").MapIdentityApi<User>(); // login url: /"api"/login
app.MapHub<CommentHub>("/comments");

// Auto Database-update
using var scope = app.Services.CreateScope(); // Creates a scope in order to use services
var services = scope.ServiceProvider;
try
{
   var context = services.GetRequiredService<AppDbContext>();
   var userManager = services.GetRequiredService<UserManager<User>>();
   await context.Database.MigrateAsync(); // Performs dotnet database update automatically
   await DbInitializer.SeedData(context, userManager);
}
catch (Exception ex)
{
   var logger = services.GetRequiredService<ILogger<Program>>();
   logger.LogError(ex, "An error has occurred during migration");
   throw;
}


app.Run();
