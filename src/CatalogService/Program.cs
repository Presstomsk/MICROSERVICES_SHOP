using CatalogService.Enrichment;
using CatalogService.Entities;
using CatalogService.Entities.Interfaces;
using CatalogService.Extensions;
using CatalogService.Infrastructure.Database;
using CatalogService.Middleware;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi;
using Serilog;
using Serilog.Enrichers.Span;
using Serilog.Events;
using Serilog.Formatting.Compact;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()          
    .Enrich.WithMachineName()         
    .Enrich.WithEnvironmentName()     
    .Enrich.WithThreadId()
    .Enrich.WithSpan()    
    .Enrich.With<ServiceVersionEnricher>()
    .WriteTo.Async(a => a.Console(new CompactJsonFormatter()))
    .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Catalog API",
        Version = "v1",
        Description = "API для управления каталогом товаров"
    });   
});

builder.Services.AddDbContext<ProductContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("CatalogDb"));
    });
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy("Process is alive"), tags: ["liveness"])
                .AddNpgSql(
                    connectionString: builder.Configuration.GetConnectionString("CatalogDb")!,
                    healthQuery: "SELECT 1;",
                    name: "catalog",
                    failureStatus: HealthStatus.Unhealthy,
                    tags: ["readiness"]);
builder.Services.AddControllers();

builder.Services.AddScoped<ICategory, Category>();
builder.Services.AddScoped<IProduct, Product>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseMiddleware<LoggerEnricherMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog API V1");        
    });
}

app.MapHealthChecks("/health/liveness", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("liveness"),
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapHealthChecks("/health/readiness", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("readiness"),
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseHttpsRedirection();
await app.AddDatabaseMigration();
app.MapControllers();

app.Run();
