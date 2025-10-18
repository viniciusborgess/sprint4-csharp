using AutoMapper;
using Guardian.Api.Data;
using Guardian.Api.Mapping;
using Guardian.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var cfg = builder.Configuration;

// EF Core: SQLite (trocar por Postgres em cloud, se quiser)
builder.Services.AddDbContext<GuardianDbContext>(opt =>
    opt.UseSqlite(cfg.GetConnectionString("sqlite") ?? "Data Source=guardian.db"));

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<InvestmentSimulator>();
builder.Services.AddScoped<AdvisorService>();
builder.Services.AddHttpClient<ExternalRatesClient>()
    .SetHandlerLifetime(TimeSpan.FromMinutes(5));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

var app = builder.Build();

// aplica migrações/seed
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<GuardianDbContext>();
    await db.EnsureSeededAsync();
}

// 🔓 Swagger também em produção
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Guardian.Api v1");
    c.RoutePrefix = "swagger"; // URL será /swagger
});

app.MapHealthChecks("/health");
app.MapControllers();

app.Run();
