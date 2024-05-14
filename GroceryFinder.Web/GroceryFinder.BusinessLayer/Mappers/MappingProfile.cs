using AutoMapper;
using GroceryFinder.BusinessLayer.DTOs;
using GroceryFinder.DataLayer.Enums;
using GroceryFinder.DataLayer.Models;

namespace GroceryFinder.BusinessLayer.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateUserDto, AppUser>()
        .ForMember(u => u.Role, m => m.MapFrom(u => u.Role))
        .ForMember(u => u.UserName, m => m.MapFrom(u => u.UserName));

        CreateMap<ProductDto, Product>().ReverseMap();
        CreateMap<GroceryStoreDto, GroceryStore>().ReverseMap();
        CreateMap<ProductGroceryStoreDto, ProductGroceryStore>().ReverseMap();
        CreateMap<GroceryStoreSearchModeDto, GroceryStoreSearchMode>().ReverseMap();
    }
}

