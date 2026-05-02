using Scalar.AspNetCore;
using Serilog;
using Template.Application;
using Template.Infrastructure;
using Template.Infrastructure.Enums;
using Template.WebAPI;
using Template.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentation()
    .AddInfrastructure(builder)
    .AddApplication();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();
app.UseAuthentication();  
app.UseAuthorization();
app.UseCors(nameof(Policy.AllowAny));
app.UseSerilogRequestLogging();
app.UseExceptionHandler();
app.MapGroup("/api").MapEndpoints();

Log.Information("Configuring web host in {ServiceEnvironment}...", app.Environment.EnvironmentName);

app.Run();