using System;
using AutoMapper;
using PreciousPoint.Models.DataModel.Account;
using PreciousPoint.Models.ViewModel.Account;

namespace PreciousPoint.Application.Helpers
{
  public class AutoMapperProfiles : Profile
  {
    public AutoMapperProfiles()
    {
      CreateMap<RegisterModel, User>()
        .ForMember(dest => dest.PhoneNumber,
          opt => opt.MapFrom(src => src.PhoneNo));
    }
  }
}

