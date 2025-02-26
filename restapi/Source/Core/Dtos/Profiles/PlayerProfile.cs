using AutoMapper;
using BasketballStatsApi.Core.Dtos.Requests;
using BasketballStatsApi.Core.Dtos.Responses;
using BasketballStatsApi.Core.Entities;

namespace BasketballStatsApi.Core.Dtos.Profiles;

public class PlayerProfile : Profile
{
  public PlayerProfile()
  {
    CreateMap<AddPlayerRequest, Player>()
      .ForMember(dest => dest.RosterStatus, opt => opt.MapFrom<object>(x => null))
      .ForMember(dest => dest.InjuryStatus, opt => opt.MapFrom<object>(x => null))
      .ForMember(dest => dest.Team, opt => opt.MapFrom<object>(x => null))
      .ForMember(dest => dest.JerseyNumber, opt => opt.MapFrom<object>(x => null))
      .ForMember(dest => dest.Points, opt => opt.MapFrom<object>(x => 0.00000m))
      .ForMember(dest => dest.Assists, opt => opt.MapFrom<object>(x => 0.00000m))
      .ForMember(dest => dest.Rebounds, opt => opt.MapFrom<object>(x => 0.00000m))
      .ForMember(dest => dest.Steals, opt => opt.MapFrom<object>(x => 0.00000m))
      .ForMember(dest => dest.Blocks, opt => opt.MapFrom<object>(x => 0.00000m))
      .ForMember(dest => dest.Turnovers, opt => opt.MapFrom<object>(x => 0.00000m));

    CreateMap<Player, PlayerResponse>();
  }
}