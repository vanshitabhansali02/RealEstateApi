using AutoMapper;
using DataAcess.DTOs;
using DataAcess.Models;
using Microsoft.AspNetCore.Mvc;

namespace RealEstateApi.Mapping
{
    public class Mapping : Profile    
    {
      public  Mapping()
        {
            CreateMap<UserDto, User>();
            CreateMap<PropertyDto,Property>().ReverseMap();
        }
    }
}
