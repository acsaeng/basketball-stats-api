using AutoMapper;
using BasketballStatsApi.Core.Constants;
using BasketballStatsApi.Core.Dtos.Requests;
using BasketballStatsApi.Core.Dtos.Responses;
using BasketballStatsApi.Core.Entities;

namespace BasketballStatsApi.Core.Dtos.Profiles;

public class TeamProfile : Profile
{
  public TeamProfile()
  {
    CreateMap<CreateTeamRequest, Team>()
      .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => Validation.Player.RosterStatus.Active))
      .ForMember(dest => dest.Wins, opt => opt.MapFrom(_ => 0))
      .ForMember(dest => dest.Losses, opt => opt.MapFrom(_ => 0));

    CreateMap<UpdateTeamRequest, Team>();

    CreateMap<Team, TeamResponse>()
      .ForMember(dest => dest.GamesPlayed, opt => opt.MapFrom(src => src.Wins + src.Losses))
      .ForMember(dest => dest.PreviousGame,
        opt => opt.MapFrom(src =>
          src.HomeGames
            .Concat(src.AwayGames)
            .LastOrDefault(g => g.Status == Validation.Game.Status.Final && g.DateTime < DateTime.Now)))
      .ForMember(dest => dest.NextGame,
        opt => opt.MapFrom(src =>
          src.HomeGames
            .Concat(src.AwayGames)
            .FirstOrDefault(g => g.Status == Validation.Game.Status.Upcoming && g.DateTime >= DateTime.Now)));
  }
}