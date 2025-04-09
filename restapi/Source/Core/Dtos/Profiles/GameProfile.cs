using AutoMapper;
using BasketballStatsApi.Core.Constants;
using BasketballStatsApi.Core.Dtos.Requests;
using BasketballStatsApi.Core.Dtos.Responses;
using BasketballStatsApi.Core.Entities;

namespace BasketballStatsApi.Core.Dtos.Profiles;

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