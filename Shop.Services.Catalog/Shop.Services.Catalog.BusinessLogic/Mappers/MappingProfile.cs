using AutoMapper;
using Shop.Services.Catalog.BusinessLogic.Models;
using Shop.Services.Catalog.DataAccess.Entities;

namespace Shop.Services.Catalog.BusinessLogic.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductModel, Product>().ReverseMap();
    }
}