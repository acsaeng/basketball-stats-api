using AutoMapper;
using BasketballLeagueApi.Core.Constants;
using BasketballLeagueApi.Core.Dtos.Requests;
using BasketballLeagueApi.Core.Dtos.Responses;
using BasketballLeagueApi.Core.Entities;

namespace BasketballLeagueApi.Core.Dtos.Profiles;

public class TeamProfile : Profile
{
  public TeamProfile()
  {
    CreateMap<CreateTeamRequest, Team>()
      .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => Validation.Player.RosterStatus.Active))
      .ForMember(dest => dest.Wins, opt => opt.MapFrom(_ => 0))
      .ForMember(dest => dest.Losses, opt => opt.MapFrom(_ => 0));

    CreateMap<Team, TeamResponse>()
      .ForMember(dest => dest.GamesPlayed, opt => opt.MapFrom(src => src.Wins + src.Losses));
  }
}