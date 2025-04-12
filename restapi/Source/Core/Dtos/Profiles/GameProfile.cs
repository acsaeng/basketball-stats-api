using AutoMapper;
using BasketballLeagueApi.Core.Constants;
using BasketballLeagueApi.Core.Dtos.Requests;
using BasketballLeagueApi.Core.Dtos.Responses;
using BasketballLeagueApi.Core.Entities;

namespace BasketballLeagueApi.Core.Dtos.Profiles;

public class GameProfile : Profile
{
  public GameProfile()
  {
    CreateMap<CreateGameRequest, Game>()
      .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => Validation.Game.Status.Upcoming));
    
    CreateMap<Game, GameResponse>()
      .ForMember(dest => dest.HomeTeam, opt => opt.MapFrom(src => src.HomeTeam.Abbreviation))
      .ForMember(dest => dest.AwayTeam, opt => opt.MapFrom(src => src.AwayTeam.Abbreviation))
      .ForMember(dest => dest.HomeTeamPlayerStats,
        opt => opt.MapFrom(src => src.PlayerStats.Where(ps => ps.TeamId == src.HomeTeam.TeamId)))
      .ForMember(dest => dest.AwayTeamPlayerStats,
        opt => opt.MapFrom(src => src.PlayerStats.Where(ps => ps.TeamId == src.AwayTeam.TeamId)));
  }
}