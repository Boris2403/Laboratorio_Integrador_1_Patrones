using LibroFacil.Application.Interfaces;
using LibroFacil.Application.Services;
using LibroFacil.Infrastructure.Persistence;
using LibroFacil.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<LibroFacilDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ILibroRepository, LibroRepositoryEf>();
builder.Services.AddScoped<LibroService>();

var app = builder.Build();

app.MapControllers();
app.Run();