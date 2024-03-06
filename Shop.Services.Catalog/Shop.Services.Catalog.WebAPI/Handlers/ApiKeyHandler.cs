using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Shop.Services.Catalog.WebAPI.Constants;

namespace Shop.Services.Catalog.WebAPI.Handlers;

public class ApiKeyHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IOptionsMonitor<AuthenticationSchemeOptions> _options;
    private readonly ILogger<ApiKeyHandler> _logger;
    private readonly UrlEncoder _encoder;


    public ApiKeyHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder) : base(options, logger, encoder)
    {
        _options = options;
        _logger = logger.CreateLogger<ApiKeyHandler>();
        _encoder = encoder;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var principal = new ClaimsPrincipal(new ClaimsIdentity(Enumerable.Empty<Claim>(), "ApiKeyAuthScheme"));
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        if (Environment.GetEnvironmentVariable(ConfigConstants.Environment) != ConfigConstants.Production)
            return AuthenticateResult.Success(ticket);

        if (!Request.Headers.TryGetValue(ConfigConstants.ApiKeyName, out var extractedApiKey))
            return AuthenticateResult.NoResult();

        var apiKey = Environment.GetEnvironmentVariable(ConfigConstants.ApiKey) ??
                     throw new Exception("ApiKey is null");
        if (!apiKey.Equals(extractedApiKey))
        {
            _logger.LogWarning("Authentication failed");
            return AuthenticateResult.Fail("Authentication failed");
        }

        _logger.LogInformation("{extractedApiKey} got access to {method}: {path}",
            extractedApiKey.ToString(),
            Request.Method,
            Request.Path.Value);

        return AuthenticateResult.Success(ticket);
    }
}