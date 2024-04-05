using AuthenticationService.Models;
using CommonLibrary.CommonResponseModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;
using WebGatewayAPI;

var builder = WebApplication.CreateBuilder(args);

//builder.Host.ConfigureWebHostDefaults(webBuilder =>
//{
   var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
//    // webBuilder.UseStartup<Startup>();
//   webBuilder.ConfigureAppConfiguration(config =>
//   config.AddJsonFile($"ocelot.{env}.json"));
//});//.ConfigureLogging(logging => logging.AddConsole());

builder.Host.ConfigureLogging(logging => logging.AddConsole());
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile(($"ocelot.{env}.json"), optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();
builder.Services.AddOcelot(builder.Configuration)
.AddSingletonDefinedAggregator<VisitAndBookingAggregator>(); // Added for aggregator sample
//builder.Services.AddOcelot(builder.Configuration)
//.AddSingletonDefinedAggregator<UserAndProductAggregator>(); // Added for aggregator sample

// Add services to the container.
var appSettingSections = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingSections);
//JWT Authentication
var appSettings = appSettingSections.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSettings.Key);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer("Bearer", options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
//builder.Services.AddOcelot();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
await app.UseOcelot();
app.Run();
