using System;
using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers;

public class AutoMapperProfile : Profile
{

    public AutoMapperProfile()
    {
        CreateMap<AppUser,MemberDto>()
        .ForMember(d=> d.Age,o=>o.MapFrom(s=>s.DateOfBirth.CalculateAge()))
        //this is a custom mapping technique which automapper fails to do below
        .ForMember(d=> d.PhotoUrl,o=>o.MapFrom(s=>s.Photos.FirstOrDefault(x=>x.IsMain)!.Url));
        CreateMap<Photo,PhotoDto>();   
    }

}
