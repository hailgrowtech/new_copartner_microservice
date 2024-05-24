using AdminDashboardService.Logic;
using CommonLibrary.Authorization;
using FluentValidation;
using Microsoft.OpenApi.Models;
using MigrationDB.Data;
using Serilog;
using System.Reflection;
using AdminDashboardService.Profiles;
using AdminDashboardService.Configuration;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.S3;
using CommonLibrary;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Adding Serilog
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    //.WriteTo.File()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add services to the container.
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
{
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
}
builder.Services.AddControllers();
builder.Services.AddApplicationInsightsTelemetry();


builder.Services.AddControllers().AddNewtonsoftJson(); //For Use in JsonPatchDocument

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Admin Dashboard Service API",
        Version = "v1",
        Description = "An API to perform Blogs operations",
        TermsOfService = new Uri("https://hailgrotech.com/"),
        Contact = new OpenApiContact
        {
            Name = "CoPartner",
            Email = "hailgrotech.com",
            Url = new Uri("https://hailgrotech.com/"),
        },
        License = new OpenApiLicense
        {
            Name = "Copartner API LICX",
            Url = new Uri("https://hailgrotech.com/"),
        }
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// Load configuration from appsettings.json
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();
// Retrieve AWS credentials from configuration
string encryptAccessKey = configuration["AWSS3Credentials:EncryptedAccessKey"];
string encryptSecretKey = configuration["AWSS3Credentials:EncryptedSecretKey"];
string region = configuration["AWSS3Credentials:Region"];

string accessKey = EncryptionHelper.DecryptString(encryptAccessKey);
string secretKey = EncryptionHelper.DecryptString(encryptSecretKey);

// Create AWS options
AWSOptions awsOptions = new AWSOptions
{
    Credentials = new BasicAWSCredentials(accessKey, secretKey),
    Region = Amazon.RegionEndpoint.GetBySystemName(region)
};
builder.Services.AddDefaultAWSOptions(awsOptions);
builder.Services.AddAWSService<IAmazonS3>();

//Resolve Dependencies Start

//Experts Service Dependencies
builder.Services.AddScoped<IBlogBusinessProcessor, BlogBusinessProcessor>();
builder.Services.AddScoped<IMarketingContentBusinessProcessor, MarketingContentBusinessProcessor>();
builder.Services.AddScoped<IAdAgencyDetailsBusinessProcessor, AdAgencyDetailsBusinessProcessor>();
builder.Services.AddScoped<IExpertsAdAgencyBusinessProcessor, ExpertsAdAgencyBusinessProcessor>();
builder.Services.AddScoped<IRelationshipManagerBusinessProcessor, RelationshipManagerBusinessProcessor>();
builder.Services.AddScoped<IUserDataListingBusinessProcessor, UserDataListingBusinessProcessor>();
builder.Services.AddScoped<IJoinBusinessProcessor, JoinBusinessProcessor>();
builder.Services.AddScoped<IAWSStorageBusinessProcessor, AWSStorageBusinessProcessor>();
builder.Services.AddScoped<IJsonMapper, JsonMapper>();
//AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
//builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>));

////Resolve Dependencies Ends
//if (builder.Environment.IsProduction())
//{
//    builder.Services.AddDbContext<ExpertsDbContextProd>();
//}
//else
//{
builder.Services.AddDbContext<CoPartnerDbContextProd, CoPartnerDbContext>();
//}


builder.Services.AddCors();


//Mass Transit RabbitMQ

var app = builder.Build();

//migrate any database changes on startup (includes initial db creation)
//using (var scope = app.Services.CreateScope())
//{
//    var dataContext = scope.ServiceProvider.GetRequiredService<CoPartnerDbContext>();
//    //dataContext.Database.Migrate();
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Admin Dashboard Service API V1");
    });
}

// global cors policy
app.UseCors(x => x
    .SetIsOriginAllowed(origin => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<JwtMiddleware>();
// custom jwt auth middleware 
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
