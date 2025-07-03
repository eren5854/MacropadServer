using MacropadServer.Application;
using MacropadServer.Infrastructure;
using MacropadServer.WebAPI;
using MacropadServer.WebAPI.Controller;
using MacropadServer.WebAPI.Middlewares;
using MacropadServer.WebAPI.Modules;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.RateLimiting;
using Scalar.AspNetCore;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
builder.AddServiceDefaults();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddOpenApi();
builder.Services.AddControllers().AddOData(opt =>
        opt
        .Select()
        .Filter()
        .Count()
        .Expand()
        .OrderBy()
        .SetMaxTop(null)
        .AddRouteComponents("odata", MacropadODataController.GetEdmModel()));
builder.Services.AddRateLimiter(x => x.AddFixedWindowLimiter("fixed", cfg =>
{
    cfg.QueueLimit = 100;
    cfg.Window = TimeSpan.FromSeconds(1);
    cfg.PermitLimit = 100;
    cfg.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
}));
builder.Services.AddExceptionHandler<ExceptionHandler>().AddProblemDetails();
var app = builder.Build();
app.UseStaticFiles();
ExtensionMiddleware.CreateFirstUser(app);
ExtensionMiddleware.CreateAdmin(app);
app.MapOpenApi();
app.MapScalarApiReference();
app.MapDefaultEndpoints();
app.UseCors(cors =>
{
    cors
        .AllowCredentials()
        .SetIsOriginAllowed(origin => true)
        .AllowAnyMethod()
        .AllowAnyHeader();
});
app.RegisterRoutes();
app.UseAuthentication();
app.UseAuthorization();
app.UseExceptionHandler();
app.MapControllers().RequireRateLimiting("fixed");
app.Run();