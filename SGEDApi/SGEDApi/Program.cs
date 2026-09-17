using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using SGED.Application.Common;
using SGED.Application.Curriculo.Disciplinas;
using SGED.Application.Curriculo.Disciplinas.AlterarDisciplina;
using SGED.Application.Curriculo.Disciplinas.CadastrarDisciplina;
using SGED.Application.Curriculo.Disciplinas.DeletarDisciplina;
using SGED.Application.Curriculo.Disciplinas.ListarDisciplinas;
using SGED.Application.Curriculo.EtapasAnosEscolares;
using SGED.Application.Curriculo.EtapasAnosEscolares.AlterarEtapaAnoEscolar;
using SGED.Application.Curriculo.EtapasAnosEscolares.CadastrarEtapaAnoEscolar;
using SGED.Application.Curriculo.EtapasAnosEscolares.DeletarEtapaAnoEscolar;
using SGED.Application.Curriculo.EtapasAnosEscolares.ListarEtapasAnosEscolares;
using SGED.Infrastructure.Curriculo.Disciplina;
using SGED.Infrastructure.Curriculo.EtapaAnosEscolares;
using SGED.Infrastructure.Persistence;
using SGEDApi.Common.Exceptions;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<SgedDbContext>(options => 
    options.UseNpgsql(connectionString)
        .LogTo(Console.WriteLine));

builder.Services.AddScoped<
    IDisciplinaRepository,
    DisciplinaRepository>();

builder.Services.AddScoped<
    IEtapaAnoEscolarRepository,
    EtapaAnoEscolarRepository>();

builder.Services.AddScoped<CadastrarDisciplinaHandler>();
builder.Services.AddScoped<ListarDisciplinaHandler>();
builder.Services.AddScoped<DeletarDisciplinaHandler>();
builder.Services.AddScoped<AlterarDisciplinaHandler>();

builder.Services.AddScoped<CadastrarEtapaAnoEscolarHandler>();
builder.Services.AddScoped<ListarEtapaAnoEscolarHandler>();
builder.Services.AddScoped<DeletarEtapaAnoEscolarHandler>();
builder.Services.AddScoped<AlterarEtapaAnoEscolarHandler>();

builder.Services.AddScoped<IUnitOfWork>(
    provider => provider.GetRequiredService<SgedDbContext>());

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

