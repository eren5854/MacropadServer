using MacropadServer.Application;
using MacropadServer.Infrastructure;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.AddServiceDefaults();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddRateLimiter(x => x.AddFixedWindowLimiter("fixed", cfg =>
{
    cfg.QueueLimit = 100;
    cfg.Window = TimeSpan.FromSeconds(1);
    cfg.PermitLimit = 100;
    cfg.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
}));

var app = builder.Build();

app.MapDefaultEndpoints();

//app.MapGet("/", () => "Hello World!");

app.MapOpenApi();
app.MapDefaultEndpoints();

app.UseCors(cors =>
{
    cors
        .AllowCredentials()
        .SetIsOriginAllowed(origin => true)
        .AllowAnyMethod()
        .AllowAnyHeader();
});

app.MapControllers().RequireRateLimiting("fixed");

app.Run();
