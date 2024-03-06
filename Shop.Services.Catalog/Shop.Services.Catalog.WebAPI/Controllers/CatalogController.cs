using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Services.Catalog.BusinessLogic.Abstractions;
using Shop.Services.Catalog.BusinessLogic.Models;
using Shop.Services.Catalog.WebAPI.Constants;
using Shop.Services.Catalog.WebAPI.Dtos;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Shop.Services.Catalog.WebAPI.Controllers;

// [Authorize]
[ApiController]
[Produces(ApiConstants.ContentType)]
[Route(ApiConstants.Catalog)]
[ProducesResponseType(Status400BadRequest, Type = typeof(ErrorDto))]
[ProducesResponseType(Status401Unauthorized, Type = typeof(ErrorDto))]
[ProducesResponseType(Status404NotFound, Type = typeof(ErrorDto))]
[ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
public class CatalogController(IProductService productService, IMapper mapper) : ControllerBase
{
    [HttpGet(ApiConstants.MongoId)]
    [ProducesResponseType(Status200OK, Type = typeof(ProductDto))]
    public async Task<ActionResult<ProductDto>> Get(string id, CancellationToken ct)
        => Ok(mapper.Map<ProductDto>(await productService.Get(id, ct)));

    [HttpGet]
    [ProducesResponseType(Status200OK, Type = typeof(ProductDto))]
    public async Task<ActionResult<IEnumerable<ProductDto>>> Get(CancellationToken ct)
        => Ok(mapper.Map<IEnumerable<ProductDto>>(await productService.GetAll(ct)));

    [HttpPost]
    [ProducesResponseType(Status201Created, Type = typeof(ProductDto))]
    public async Task<ActionResult<ProductDto>> Create([Required] [FromBody] ProductDto dto, CancellationToken ct)
    {
        var result = await productService.Add(mapper.Map<ProductModel>(dto), ct);
        return Created($"{ApiConstants.Catalog}/{result.Id}", mapper.Map<ProductDto>(result));
    }

    [HttpPut(ApiConstants.MongoId)]
    [ProducesResponseType(Status200OK, Type = typeof(ProductDto))]
    public async Task<ActionResult<ProductDto>> Update(string id, [FromBody] ProductDto dto, CancellationToken ct)
        => Ok(mapper.Map<ProductDto>(await productService.Update(id, mapper.Map<ProductModel>(dto), ct)));

    [HttpDelete(ApiConstants.MongoId)]
    [ProducesResponseType(Status204NoContent, Type = typeof(ProductDto))]
    public async Task<ActionResult> Delete(string id, CancellationToken ct)
    {
        await productService.Delete(id, ct);
        return NoContent();
    }
}