using System.Reflection;
using Equipment_Storage_Service.Extensions;
using Equipment_Storage_Service.Middlewares;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Storage.Application.Helpers;
using Storage.Application.MappingProfiles;
using Storage.Application.Models.Validators;
using Storage.Application.Services;
using Storage.Application.Services.Background;
using Storage.Application.Services.Impl;
using Storage.DataAccess.Persistence.Migrations;
using Storage.DataAccess.Repositories;
using Storage.DataAccess.Repositories.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registering Database in DI
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING"));
});

// Registering other dependencies
builder.Services.AddScoped<IContractService, ContractService>();
builder.Services.AddScoped<IContractRepository, ContractRepository>();

builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();
builder.Services.AddScoped<IProductionFacilityRepository, ProductionFacilityRepository>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(ContractProfile));

// Fluent Validator
builder.Services.AddValidatorsFromAssemblyContaining(typeof(ContractModelValidator));

// Background service
builder.Services.AddHostedService<LoggingBackgroundService>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Storage.API");
    c.RoutePrefix = string.Empty;
});

if (app.Environment.IsDevelopment())
{
    app.ApplyMigrations();
    app.CreateDbIfNotExists();
}

app.UseHttpsRedirection();

// Add Middleware
app.UseMiddleware<ApiKeyMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();