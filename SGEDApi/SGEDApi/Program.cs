using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using SGED.Application.Common;
using SGED.Application.Curriculo.EtapasAnosEscolares;
using SGED.Application.Curriculo.EtapasAnosEscolares.AlterarEtapaAnoEscolar;
using SGED.Application.Curriculo.EtapasAnosEscolares.CadastrarEtapaAnoEscolar;
using SGED.Application.Curriculo.EtapasAnosEscolares.DeletarEtapaAnoEscolar;
using SGED.Application.Curriculo.EtapasAnosEscolares.ListarEtapasAnosEscolares;
using SGED.Application.Curriculo.Modalidades;
using SGED.Application.Curriculo.Modalidades.AlterarModalidade;
using SGED.Application.Curriculo.Modalidades.CadastrarModalidade;
using SGED.Application.Curriculo.Modalidades.DeletarModalidade;
using SGED.Application.Curriculo.Modalidades.ListarModalidades;
using SGED.Infrastructure.Curriculo.EtapaAnosEscolares;
using SGED.Infrastructure.Curriculo.Modalidades;
using SGED.Infrastructure.Persistence;
using SGEDApi.Common.Exceptions;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<SgedDbContext>(options => 
    options.UseNpgsql(connectionString)
        .LogTo(Console.WriteLine));

builder.Services.AddScoped<
    IEtapaAnoEscolarRepository,
    EtapaAnoEscolarRepository>();

builder.Services.AddScoped<
    IModalidadeRepository,
    ModalidadeRepository>();

builder.Services.AddScoped<CadastrarEtapaAnoEscolarHandler>();
builder.Services.AddScoped<ListarEtapaAnoEscolarHandler>();
builder.Services.AddScoped<DeletarEtapaAnoEscolarHandler>();
builder.Services.AddScoped<AlterarEtapaAnoEscolarHandler>();

builder.Services.AddScoped<CadastrarModalidadeHandler>();
builder.Services.AddScoped<AlterarModalidadeHandler>();
builder.Services.AddScoped<DeletarModalidadeHandler>();
builder.Services.AddScoped<ListarModalidadeHandler>();

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

