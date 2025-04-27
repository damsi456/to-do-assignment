using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TodoApi.Application.Interfaces;
using TodoApi.Infrastructure.Persistence;
using TodoApi.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); 

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString); // Register dbcontext to use sql server provider
});

// Register repositories
// Tells to provide an instance of TaskRepository when ITaskRepository is requested.
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddControllers();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Ensure controller routes are mapped
app.MapControllers();

app.Run();

