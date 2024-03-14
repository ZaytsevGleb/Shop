using Common.Logging;
using FluentValidation;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Serilog;
using Shop.Services.Catalog.BusinessLogic.DI;
using Shop.Services.Catalog.WebAPI.Constants;
using Shop.Services.Catalog.WebAPI.Mappers;
using Shop.Services.Catalog.WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
var configuration = builder.Configuration;

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();
builder.Host.UseSerilog(SeriLogger.Configure);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting();
builder.Services.AddAuthorization();
/*builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<ApplicationDbContext>();*/
builder.Services.AddCors(opt => opt.AddDefaultPolicy(p => p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
builder.Services.AddControllers();
builder.Services.AddHealthChecks();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddApplicationDependencies(configuration);

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc(ApiConstants.V1, new OpenApiInfo { Title = ApiConstants.Title, Version = ApiConstants.V1 });
    opt.AddSecurityDefinition(ApiConstants.BearerScheme, new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = ApiConstants.Description,
        Name = ApiConstants.Authorization,
        Type = SecuritySchemeType.Http,
        BearerFormat = ApiConstants.BearerFormat,
        Scheme = ApiConstants.BearerScheme
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = ApiConstants.BearerScheme
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddHealthChecks()
    .AddRedis(configuration["CacheSettings:ConnectionString"], "Redis Health", HealthStatus.Degraded);   

var app = builder.Build();

if (!env.IsEnvironment(ConfigConstants.Production))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CustomExceptionHandlerMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();
// app.MapIdentityApi<User>();
app.UseHttpsRedirection();
app.UseCors();
app.UseRouting();
// app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");
app.MapGet("/env", () => env.EnvironmentName);
app.Run();