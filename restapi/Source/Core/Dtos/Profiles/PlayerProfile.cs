using AutoMapper;
using BasketballStatsApi.Core.Constants;
using BasketballStatsApi.Core.Dtos.Requests;
using BasketballStatsApi.Core.Dtos.Responses;
using BasketballStatsApi.Core.Entities;
using BasketballStatsApi.Core.Models;

namespace BasketballStatsApi.Core.Dtos.Profiles;

public class PlayerProfile : Profile
{
  public PlayerProfile()
  {
    CreateMap<CreatePlayerRequest, Player>()
      .ForMember(dest => dest.InjuryStatus, opt => opt.MapFrom(_ => Validation.Player.InjuryStatus.Healthy))
      .ForMember(dest => dest.RosterStatus, opt => opt.MapFrom(_ => Validation.Player.RosterStatus.FreeAgent))
      .ForMember(dest => dest.GamesPlayed, opt => opt.MapFrom(_ => 0))
      .ForMember(dest => dest.Points, opt => opt.MapFrom(_ => 0.00000m))
      .ForMember(dest => dest.Assists, opt => opt.MapFrom(_ => 0.00000m))
      .ForMember(dest => dest.Rebounds, opt => opt.MapFrom(_ => 0.00000m))
      .ForMember(dest => dest.Steals, opt => opt.MapFrom(_ => 0.00000m))
      .ForMember(dest => dest.Blocks, opt => opt.MapFrom(_ => 0.00000m))
      .ForMember(dest => dest.Turnovers, opt => opt.MapFrom(_ => 0.00000m));

    CreateMap<Player, PlayerModel>();

    CreateMap<Player, PlayerResponse>()
      .ForMember(dest => dest.Team, opt => opt.MapFrom(src => src.Team!.Abbreviation))
      .ForMember(dest => dest.Points, opt => opt.MapFrom(src => Math.Round(src.Points, 1)))
      .ForMember(dest => dest.Assists, opt => opt.MapFrom(src => Math.Round(src.Assists, 1)))
      .ForMember(dest => dest.Rebounds, opt => opt.MapFrom(src => Math.Round(src.Rebounds, 1)))
      .ForMember(dest => dest.Steals, opt => opt.MapFrom(src => Math.Round(src.Steals, 1)))
      .ForMember(dest => dest.Blocks, opt => opt.MapFrom(src => Math.Round(src.Blocks, 1)))
      .ForMember(dest => dest.Turnovers, opt => opt.MapFrom(src => Math.Round(src.Turnovers, 1)));

    CreateMap<Player, TeamResponseRoster>();

    CreateMap<PlayerGame, GameResponsePlayerStats>()
      .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Player.FirstName))
      .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Player.LastName))
      .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Player.Position))
      .ForMember(dest => dest.JerseyNumber, opt => opt.MapFrom(src => src.Player.JerseyNumber));
    
    CreateMap<FinalizeGameRequestPlayerStats, PlayerGame>()
      .ForMember(dest => dest.TeamId, opt => opt.MapFrom((_, _, _, context) => context.Items["TeamId"]))
      .ForMember(dest => dest.GameId, opt => opt.MapFrom((_, _, _, context) => context.Items["GameId"]));
  }
}