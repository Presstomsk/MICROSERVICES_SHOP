using CartService.Application.Services.Interfaces;
using CartService.Enrichment;
using CartService.Entities.Interfaces;
using CartService.Infrastructure;
using CartService.Middleware;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
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

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Host.UseSerilog();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "cart:";
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Cart API",
        Version = "v1",
        Description = "API для управления корзиной товаров"
    });   
});

builder.Services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy("Process is alive"), tags: ["liveness"])
                .AddRedis(
                    redisConnectionString: builder.Configuration.GetConnectionString("Redis")!,
                    name: "cart",
                    failureStatus: HealthStatus.Unhealthy,
                    tags: ["readiness"]);
builder.Services.AddControllers();

builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService.Application.Services.CartService>();

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseMiddleware<LoggerEnricherMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Cart API V1");        
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
app.MapControllers();

app.Run();
