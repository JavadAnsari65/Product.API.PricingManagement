using Microsoft.EntityFrameworkCore;
using Product.API.PricingManagement.Application;
using Product.API.PricingManagement.Infrastructure.Configuration;
using Product.API.PricingManagement.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Add DbContext
builder.Services.AddDbContext<PricingDbContext>();

//Add AutoMapper
builder.Services.AddAutoMapper(typeof(CustomMap));

//Add Services
builder.Services.AddScoped<IPriceRepo, PriceRepo>();
builder.Services.AddScoped<CRUDService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
