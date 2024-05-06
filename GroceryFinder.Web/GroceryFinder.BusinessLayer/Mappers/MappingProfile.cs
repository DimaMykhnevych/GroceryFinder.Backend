﻿using AutoMapper;
using GroceryFinder.BusinessLayer.DTOs;
using GroceryFinder.DataLayer.Models;

namespace GroceryFinder.BusinessLayer.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateUserDto, AppUser>()
        .ForMember(u => u.Role, m => m.MapFrom(u => u.Role))
        .ForMember(u => u.UserName, m => m.MapFrom(u => u.UserName));
    }
}

