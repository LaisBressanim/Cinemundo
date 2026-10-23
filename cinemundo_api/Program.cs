
using Microsoft.EntityFrameworkCore;
using cinemundo_api.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "server=localhost;port=3306;database=cinemundo;user=root;password=;";

builder.Services.AddDbContext<CinemaContext>(options =>
    options.UseMySQL(connectionString));

builder.Services.AddControllers();

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
