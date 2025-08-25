using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Mohafadati.Services.Titres.Data;
using Mohafadati.Services.Titres.Middlewares;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy => policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod());
});



builder.Services.AddRateLimiter(option =>
{ 
    option.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    option.AddFixedWindowLimiter(policyName: "fixed", options =>
    {
        options.PermitLimit = 10;
        options.Window = TimeSpan.FromMinutes(15);
        options.QueueLimit = 0;
    });
});
builder.Services.AddControllers();

var app = builder.Build();
app.UseCors("AllowAngular");

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRateLimiter();

app.UseMiddleware<LogsMiddleware>();

app.MapControllers().RequireRateLimiting("fixed");
ApplyMigration();
app.Run();

void ApplyMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        if(_db.Database.GetPendingMigrations().Count() >0)
        {
            _db.Database.Migrate();
        }
    }
}