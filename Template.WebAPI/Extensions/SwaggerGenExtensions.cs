using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Template.WebAPI.Extensions;

public static class SwaggerGenExtensions
{
    public static void ConfigureSwaggerGen(this SwaggerGenOptions options)
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer"
        });
    
        options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("Bearer", document)] = []
        });
    }
}