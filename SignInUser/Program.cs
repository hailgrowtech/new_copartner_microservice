using CommonLibrary.ErrorHandler;
using Microsoft.OpenApi.Models;
using Serilog;
using SignInService.Data;
using SignInService.JWToken;
using SignInService.Logic;
using System.Reflection;
using SignInUserService;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);



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

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddCors();
// Add services to the container.
//builder.Services.AddServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddApplicationInsightsTelemetry();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SignIn Service API",
        Version = "v1",
        Description = "An API to perform SignIn operations",
        TermsOfService = new Uri("https://hailgrotech.com"),
        Contact = new OpenApiContact
        {
            Name = "Hailgrotech",
            Email = "hailgrotech.com",
            Url = new Uri("https://hailgrotech.com"),
        },
        License = new OpenApiLicense
        {
            Name = "Hailgrotech API LICX",
            Url = new Uri("https://hailgrotech.com"),
        }
    });
    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
// Register configuration settings
builder.Services.Configure<CommonLibrary.AppSettings>(builder.Configuration.GetSection("AppSettings"));

// Register as singleton if you need to inject it as a concrete type
builder.Services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<CommonLibrary.AppSettings>>().Value);


//User Service Dependencies
builder.Services.AddScoped<ISignInBusinessProcessor, SignInBusinessProcessor>();
builder.Services.AddScoped<IJwtUtils, JwtUtils>();

//AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Resolve Dependencies Ends

//if (builder.Environment.IsProduction())
//{
//    builder.Services.AddDbContext<SignUpDbContextProd>();
//}
//else
//{
builder.Services.AddDbContext<SignInDbContextProd, SignInDbContext>();
//}TODO:Deepak



var app = builder.Build();

// migrate any database changes on startup (includes initial db creation)
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<SignInDbContext>();
    //dataContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SignIn Service API V1");
    });
}

// global cors policy
app.UseCors(x => x
    .SetIsOriginAllowed(origin => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

// global error handler //TODO : Implement error handling
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
