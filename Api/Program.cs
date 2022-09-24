using Api.Extensions;
using Api.Helpers;
using Api.Middlewares;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSwaggerDocumentation();

builder.Services.AddApplicationServicesExtensions();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .WithOrigins("https://localhost:4200");
    });
});

//Database connection
builder.Services.AddDbContext<StoreContext>(op =>
{
    op.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(typeof(MappingProfiles));

var app = builder.Build();

//Apply migrations, create database and seed data at startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    var logger = loggerFactory.CreateLogger<Program>();
    try
    {
        var storeContext = services.GetRequiredService<StoreContext>();
        await storeContext.Database.MigrateAsync();
        logger.LogInformation("Migration verification completed!");
        await StoreContextSeed.SeedAsync(storeContext, loggerFactory);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error ocorrued during migration verification or execution");
    }
}

// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation();
}

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
