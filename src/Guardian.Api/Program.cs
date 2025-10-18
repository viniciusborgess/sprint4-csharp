using AutoMapper;
using Guardian.Api.Data;
using Guardian.Api.Mapping;
using Guardian.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var cfg = builder.Configuration;

// EF Core: SQLite local (trocar por PostgreSQL em cloud)
builder.Services.AddDbContext<Guardian.Api.Data.GuardianDbContext>(opt =>
    opt.UseSqlite(cfg.GetConnectionString("sqlite") ?? "Data Source=guardian.db"));

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Serviços
builder.Services.AddScoped<InvestmentSimulator>();
builder.Services.AddScoped<AdvisorService>();

// HttpClient resiliente
builder.Services.AddHttpClient<ExternalRatesClient>()
    .SetHandlerLifetime(TimeSpan.FromMinutes(5));

builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Health checks
builder.Services.AddHealthChecks();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<GuardianDbContext>();
    await db.EnsureSeededAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/health");
app.MapControllers();

app.Run();
