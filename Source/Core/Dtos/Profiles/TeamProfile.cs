using AutoMapper;
using BasketballStatsApi.Core.Dtos.Requests;
using BasketballStatsApi.Core.Dtos.Responses;
using BasketballStatsApi.Core.Entities;

namespace BasketballStatsApi.Core.Dtos.Profiles;

public class TeamProfile : Profile
{
  public TeamProfile()
  {
    CreateMap<AddTeamRequest, Team>()
      .ForMember(dest => dest.Status, opt => opt.MapFrom<object>(x => "Active"))
      .ForMember(dest => dest.Players, opt => opt.MapFrom<object>(x => new List<int>()))
      .ForMember(dest => dest.Wins, opt => opt.MapFrom<object>(x => 0))
      .ForMember(dest => dest.Losses, opt => opt.MapFrom<object>(x => 0));
    
    CreateMap<Team, TeamResponse>();
  }
}