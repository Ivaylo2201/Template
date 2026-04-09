using System.Reflection;
using Serilog;
using Template.Application;
using Template.Infrastructure;
using Template.Infrastructure.Enums;
using Template.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

var serviceName = Assembly.GetEntryAssembly()?.GetName().Name ?? "Template.WebAPI";

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(options => options.ConfigureSwaggerGen())
    .AddInfrastructure(builder)
    .AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();  
app.UseAuthorization();
app.UseCors(nameof(Policy.AllowAny));
app.UseSerilogRequestLogging();

app.MapEndpoints();

Log.Information("[{ServiceName}]: Configuring web host in {ServiceEnvironment}...", serviceName, app.Environment.EnvironmentName);

app.Run();