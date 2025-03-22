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
      .ForMember(dest => dest.InjuryStatus, opt => opt.MapFrom<object>(_ => "Healthy"))
      .ForMember(dest => dest.RosterStatus, opt => opt.MapFrom<object>(_ => "Free agent"))
      .ForMember(dest => dest.TeamId, opt => opt.MapFrom<object>(_ => null))
      .ForMember(dest => dest.JerseyNumber, opt => opt.MapFrom<object>(_ => null))
      .ForMember(dest => dest.Points, opt => opt.MapFrom<object>(_ => 0.00000m))
      .ForMember(dest => dest.Assists, opt => opt.MapFrom<object>(_ => 0.00000m))
      .ForMember(dest => dest.Rebounds, opt => opt.MapFrom<object>(_ => 0.00000m))
      .ForMember(dest => dest.Steals, opt => opt.MapFrom<object>(_ => 0.00000m))
      .ForMember(dest => dest.Blocks, opt => opt.MapFrom<object>(_ => 0.00000m))
      .ForMember(dest => dest.Turnovers, opt => opt.MapFrom<object>(_ => 0.00000m));

    CreateMap<UpdatePlayerInfoRequest, Player>();

    CreateMap<UpdatePlayerInjuryRequest, Player>();

    CreateMap<UpdatePlayerRosterStatusRequest, Player>();

    CreateMap<MovePlayerToTeam, Player>();
    
    CreateMap<Player, PlayerResponse>()
      .ForMember(dest => dest.Team,opt => opt.MapFrom((_, _, _, context) => context.Items["Team"]));
  }
}