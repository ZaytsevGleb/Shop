using AutoMapper;
using Shop.Services.Catalog.BusinessLogic.Models;
using Shop.Services.Catalog.WebAPI.Dtos;

namespace Shop.Services.Catalog.WebAPI.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductModel, ProductDto>().ReverseMap();
    }
}