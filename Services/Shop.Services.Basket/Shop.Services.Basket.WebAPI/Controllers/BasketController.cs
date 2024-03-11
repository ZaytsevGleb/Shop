using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop.Services.Catalog.BusinessLogic.Abstractions;
using Shop.Services.Catalog.BusinessLogic.Models;
using Shop.Services.Catalog.WebAPI.Constants;
using Shop.Services.Catalog.WebAPI.Dtos;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Shop.Services.Catalog.WebAPI.Controllers;

[ApiController]
[Produces(ApiConstants.ContentType)]
[Route(ApiConstants.ControllerName)]
[ProducesResponseType(Status400BadRequest, Type = typeof(ErrorDto))]
[ProducesResponseType(Status401Unauthorized, Type = typeof(ErrorDto))]
[ProducesResponseType(Status404NotFound, Type = typeof(ErrorDto))]
[ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
public class BasketController(IBasketService basketService, IMapper mapper) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(Status200OK, Type = typeof(BasketCheckoutDto))]
    public async Task<ActionResult<BasketCheckoutDto>> Get(string userName, CancellationToken ct)
        => mapper.Map<BasketCheckoutDto>(await basketService.Get(userName, ct));

    [HttpPut]
    [ProducesResponseType(Status200OK, Type = typeof(BasketCheckoutDto))]
    public async Task<ActionResult<BasketCheckoutDto>> Update([FromBody] ShoppingCartModel dto, CancellationToken ct)
        => mapper.Map<BasketCheckoutDto>(await basketService.Update(mapper.Map<ShoppingCartModel>(dto), ct));

    [HttpDelete]
    [ProducesResponseType(Status204NoContent, Type = typeof(BasketCheckoutDto))]
    public async Task<ActionResult> Delete(string userName, CancellationToken ct)
    {
        await basketService.Delete(userName, ct);
        return NoContent();
    }
}