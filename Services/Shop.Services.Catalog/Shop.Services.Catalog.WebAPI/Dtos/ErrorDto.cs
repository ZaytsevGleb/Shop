namespace Shop.Services.Catalog.WebAPI.Dtos;

public class ErrorDto
{
    public string Message { get; init; } = default!;
    public string? Description { get; init; } = default!;
}