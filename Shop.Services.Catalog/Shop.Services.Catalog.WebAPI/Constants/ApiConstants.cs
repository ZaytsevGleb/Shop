using System.Runtime.InteropServices;

namespace Shop.Services.Catalog.WebAPI.Constants;

public class ApiConstants
{
    public const string ContentType = "application/json";
    public const string BearerScheme = "Bearer";
    public const string BearerFormat = "JWT";
    public const string V1 = "v1";
    public const string Version = "v1.0";
    public const string Authorization = "Authorization";
    public const string Title = "API";
    public const string Description = "Enter 'Bearer' token";
    public const string Id = "{id:guid}";
    public const string MongoId = "{id:length(24)}";
    public const string Catalog = "api/catalog";
}