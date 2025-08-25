using EmailCodeVerificationAPI.Middlewares;
using EmailCodeVerificationAPI.Services;
using EmailCodeVerificationAPI.Settings;


var builder = WebApplication.CreateBuilder(args);

// Bind EmailSitittings
builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<CodeGeneratorService>();
builder.Services.AddSingleton<EmailService>();
//allow CORS for all origins, methods, and headers
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy => policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod());
});


var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//always before the app.MapControllers();
app.UseCors("AllowAngular");
app.UseAuthorization();
app.UseMiddleware<LogsMiddleware>();


app.MapControllers();

app.Run();

