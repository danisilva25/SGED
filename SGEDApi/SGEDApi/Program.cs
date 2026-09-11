using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using SGED.Application.Common;
using SGED.Application.Curriculo.Disciplinas;
using SGED.Application.Curriculo.Disciplinas.CadastrarDisciplina;
using SGED.Infrastructure.Curriculo.Disciplina;
using SGED.Infrastructure.Persistence;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<SgedDbContext>(options => 
    options.UseNpgsql(connectionString)
        .LogTo(Console.WriteLine));

builder.Services.AddScoped<
    IDisciplinaRepository,
    DisciplinaRepository>();

builder.Services.AddScoped<CadastrarDisciplinaHandler>();

builder.Services.AddScoped<IUnitOfWork>(
    provider => provider.GetRequiredService<SgedDbContext>());

builder.Services.AddControllers();

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

