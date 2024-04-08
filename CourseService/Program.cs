
using CommonLibrary.Authorization;
using CommonLibrary.ErrorHandler;
using CourseService.Data;
using CourseService.Logic;
using ExpertService.Profiles;
using FluentValidation;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;

namespace CourseService
{
    public class Program
    {
        public static void Main(string[] args)
        {
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
                    Title = "Experts Service API",
                    Version = "v1",
                    Description = "An API to perform Experts operations",
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



            //Resolve Dependencies Start

            //Experts Service Dependencies
            builder.Services.AddScoped<ICourseBusinessProcessor, CourseBusinessProcessor>();
            builder.Services.AddScoped<IJsonMapper, JsonMapper>();
            //AutoMapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
            //builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>));

            ////Resolve Dependencies Ends
            if (builder.Environment.IsProduction())
            {
                builder.Services.AddDbContext<CourseDbContextProd>();
            }
            else
            {
                builder.Services.AddDbContext<CourseDbContextProd, CourseDbContext>();
            }


            builder.Services.AddCors();





            //Mass Transit RabbitMQ

            var app = builder.Build();

            //migrate any database changes on startup (includes initial db creation)
            using (var scope = app.Services.CreateScope())
            {
                var dataContext = scope.ServiceProvider.GetRequiredService<CourseDbContext>();
                dataContext.Database.Migrate();
            }











            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Experts Service API V1");
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
        }
    }
}
