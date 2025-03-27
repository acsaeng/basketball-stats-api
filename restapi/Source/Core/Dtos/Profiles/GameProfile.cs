using AutoMapper;
using BasketballStatsApi.Core.Dtos.Requests;
using BasketballStatsApi.Core.Dtos.Responses;
using BasketballStatsApi.Core.Entities;

namespace BasketballStatsApi.Core.Dtos.Profiles;

public class GameProfile : Profile
{
  public GameProfile()
  {
    CreateMap<CreateGameRequest, Game>()
      .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => "Upcoming"));

    CreateMap<UpdateGameInfoRequest, Game>();

    CreateMap<Game, GameResponse>()
      .ForMember(dest => dest.HomeTeam, opt => opt.MapFrom(src => src.HomeTeam.Abbreviation))
      .ForMember(dest => dest.AwayTeam, opt => opt.MapFrom(src => src.AwayTeam.Abbreviation));
  }
}