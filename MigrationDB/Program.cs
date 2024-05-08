using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (builder.Environment.IsProduction())
{
    builder.Services.AddDbContext<CoPartnerDbContextProd>();
    builder.Services.AddDbContext<AuthenticationDbContextProd>();
    builder.Services.AddDbContext<SignInDbContextProd>();
}
else
{
    builder.Services.AddDbContext<CoPartnerDbContextProd, CoPartnerDbContext>();
    builder.Services.AddDbContext<AuthenticationDbContextProd, AuthenticationDbContext >();
    builder.Services.AddDbContext<SignInDbContextProd, SignInDbContext >();
}
var app = builder.Build();

//migrate any database changes on startup (includes initial db creation)
//using (var scope = app.Services.CreateScope())
//{
//    var dataContext = scope.ServiceProvider.GetRequiredService<CoPartnerDbContextProd>();
//    dataContext.Database.Migrate();
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
