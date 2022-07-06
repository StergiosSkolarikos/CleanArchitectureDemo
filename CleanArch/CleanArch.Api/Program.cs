using CleanArch.Api.Configurations;
using CleanArch.Infra.Data.Context;
using CleanArch.Infra.IoC;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UniversityDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("UniversityDBConnection"));
});

builder.Services.AddMediatR(typeof(Program));
builder.Services.RegisterAutoMapper();

RegisterServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

static void RegisterServices(IServiceCollection services)
{
    DependencyContainer.RegisterServices(services);
}

app.Run();
