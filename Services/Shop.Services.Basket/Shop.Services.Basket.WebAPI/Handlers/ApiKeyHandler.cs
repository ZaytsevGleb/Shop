using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Shop.Services.Catalog.WebAPI.Constants;

namespace Shop.Services.Catalog.WebAPI.Handlers;

public class ApiKeyHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    private readonly IOptionsMonitor<AuthenticationSchemeOptions> _options = options;
    private readonly ILogger<ApiKeyHandler> _logger = logger.CreateLogger<ApiKeyHandler>();
    private readonly UrlEncoder _encoder = encoder;

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var principal = new ClaimsPrincipal(new ClaimsIdentity(Enumerable.Empty<Claim>(), "ApiKeyAuthScheme"));
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        if (Environment.GetEnvironmentVariable(ConfigConstants.Environment) != ConfigConstants.Production)
            return Task.FromResult(AuthenticateResult.Success(ticket));

        if (!Request.Headers.TryGetValue(ConfigConstants.ApiKeyName, out var extractedApiKey))
            return Task.FromResult(AuthenticateResult.NoResult());

        var apiKey = Environment.GetEnvironmentVariable(ConfigConstants.ApiKey) ??
                     throw new Exception("ApiKey is null");
        if (!apiKey.Equals(extractedApiKey))
        {
            _logger.LogWarning("Authentication failed");
            return Task.FromResult(AuthenticateResult.Fail("Authentication failed"));
        }

        _logger.LogInformation("{extractedApiKey} got access to {method}: {path}",
            extractedApiKey.ToString(),
            Request.Method,
            Request.Path.Value);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}