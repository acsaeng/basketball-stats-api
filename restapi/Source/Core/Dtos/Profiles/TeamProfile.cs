using AutoMapper;
using BasketballStatsApi.Core.Dtos.Requests;
using BasketballStatsApi.Core.Dtos.Responses;
using BasketballStatsApi.Core.Entities;

namespace BasketballStatsApi.Core.Dtos.Profiles;

public class TeamProfile : Profile
{
  public TeamProfile()
  {
    CreateMap<CreateTeamRequest, Team>()
      .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => "Active"))
      .ForMember(dest => dest.Wins, opt => opt.MapFrom(_ => 0))
      .ForMember(dest => dest.Losses, opt => opt.MapFrom(_ => 0));

    CreateMap<UpdateTeamRequest, Team>();

    CreateMap<Team, TeamResponse>();
  }
}