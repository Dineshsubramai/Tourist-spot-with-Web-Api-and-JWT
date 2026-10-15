using Scalar.AspNetCore;
using TouristWebApi.Dbcontext;
using Microsoft.EntityFrameworkCore;

using TouristWebApi.Services;
using TourWebAPI.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Tourist_DbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("TouristDb")));

builder.Services.AddScoped<ITourist_service, Tourist_service>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5201",
                "http://127.0.0.1:5201"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<Tourist_service>();

var app = builder.Build();

app.UseCors("AllowFrontend");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

