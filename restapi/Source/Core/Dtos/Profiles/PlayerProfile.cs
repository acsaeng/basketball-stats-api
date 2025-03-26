using AutoMapper;
using BasketballStatsApi.Core.Dtos.Requests;
using BasketballStatsApi.Core.Dtos.Responses;
using BasketballStatsApi.Core.Entities;

namespace BasketballStatsApi.Core.Dtos.Profiles;

public class PlayerProfile : Profile
{
  public PlayerProfile()
  {
    CreateMap<CreatePlayerRequest, Player>()
      .ForMember(dest => dest.InjuryStatus, opt => opt.MapFrom(_ => "Healthy"))
      .ForMember(dest => dest.RosterStatus, opt => opt.MapFrom(_ => "Free agent"))
      .ForMember(dest => dest.GamesPlayed, opt => opt.MapFrom(_ => 0))
      .ForMember(dest => dest.Points, opt => opt.MapFrom(_ => 0.00000m))
      .ForMember(dest => dest.Assists, opt => opt.MapFrom(_ => 0.00000m))
      .ForMember(dest => dest.Rebounds, opt => opt.MapFrom(_ => 0.00000m))
      .ForMember(dest => dest.Steals, opt => opt.MapFrom(_ => 0.00000m))
      .ForMember(dest => dest.Blocks, opt => opt.MapFrom(_ => 0.00000m))
      .ForMember(dest => dest.Turnovers, opt => opt.MapFrom(_ => 0.00000m));

    CreateMap<UpdatePlayerInfoRequest, Player>();

    CreateMap<UpdatePlayerInjuryRequest, Player>();

    CreateMap<UpdatePlayerRosterStatusRequest, Player>();

    CreateMap<AddPlayerToRosterRequest, Player>();

    CreateMap<Player, PlayerBasicResponse>();

    CreateMap<Player, PlayerResponse>()
      .ForMember(dest => dest.Team, opt => opt.MapFrom(src => src.Team!.Abbreviation));
  }
}