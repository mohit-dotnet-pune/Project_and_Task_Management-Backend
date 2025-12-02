using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project___Task_Management_Backend.Data;
using Project___Task_Management_Backend.Helpers;
using Project___Task_Management_Backend.Hubs;
using Project___Task_Management_Backend.Interfaces;
using Project___Task_Management_Backend.Middleware;
using Project___Task_Management_Backend.Models;
using Project___Task_Management_Backend.Repository;
using Project___Task_Management_Backend.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 🌟 Load .env file from root folder
DotNetEnv.Env.Load();

// CONTROLLERS
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler =
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

// 🌟 Connection string from .env
var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddScoped<IProjectTaskRepository, ProjectTaskRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();

// DEPENDENCY INJECTION
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectTaskService, ProjectTaskService>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<EmailHelper>();
builder.Services.AddScoped<IAuthService, AuthService>();

//Activity configuration
builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddScoped<ActivityService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🌟 JWT settings from .env
var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
var CloudName = Environment.GetEnvironmentVariable("CloudName");
var ApiKey = Environment.GetEnvironmentVariable("ApiKey");
var ApiSecret = Environment.GetEnvironmentVariable("ApiSecret");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ClockSkew = TimeSpan.Zero
    };
});




builder.Services.AddSingleton(provider =>
{
    var account = new CloudinaryDotNet.Account(
        CloudName,
        ApiKey,
        ApiSecret
    );

    return new CloudinaryDotNet.Cloudinary(account);
});
builder.Services.AddScoped<CloudinaryService>();

// Notification
builder.Services.AddSignalR();
builder.Services.AddScoped<NotificationService>();

builder.Services.Configure<FormOptions>(opt =>
{
    opt.MultipartBodyLengthLimit = long.MaxValue;
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")   // ? REMOVE trailing slash
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});


var app = builder.Build();
app.MapHub<NotificationHub>("/notifications");
app.UseCors("AllowFrontend");   // ? MUST BE HERE before MapHub + MapControllers

// MIDDLEWARE
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();     // FIXED: Authentication MUST come before Authorization
app.UseMiddleware<JwtVerificationMiddleware>();
app.UseAuthorization();

app.MapControllers();


app.Run();
