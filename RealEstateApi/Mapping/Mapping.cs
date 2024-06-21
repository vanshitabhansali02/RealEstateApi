using AutoMapper;
using DataAcess.DTOs;
using DataAcess.Models;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;

namespace RealEstateApi.Mapping
{
    public class Mapping : Profile    
    {
      public  Mapping()
        {
            CreateMap<CreateUserDto, User>();
            CreateMap<CreateUserDto,Agent>().ReverseMap();
           CreateMap<LoginUserDto,User>().ReverseMap();
            CreateMap<PropertyDto,Property>().ReverseMap();
        }
    }
}
