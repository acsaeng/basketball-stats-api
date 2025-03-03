using AutoMapper;
using BasketballStatsApi.Core.Contracts;
using BasketballStatsApi.Core.Dtos.Requests;
using BasketballStatsApi.Core.Dtos.Responses;
using BasketballStatsApi.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BasketballStatsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayerController(IPlayerService playerService, IMapper mapper) : ControllerBase
{
  [HttpGet("{playerId}")]
  public async Task<ActionResult<Player?>> GetPlayer(int playerId)
  {
    var player = await playerService.GetPlayer(playerId);

    if (player is null)
      return NotFound();

    var playerResponse = mapper.Map<PlayerResponse>(player);
    return Ok(playerResponse);
  }

  [HttpPost]
  public async Task<ActionResult> AddPlayer([FromBody] PlayerInfoRequest playerInfoRequest)
  {
    var newPlayer = mapper.Map<Player>(playerInfoRequest);
    var player = await playerService.AddPlayer(newPlayer);
    var addPlayerResponse = mapper.Map<PlayerResponse>(player);
    return CreatedAtAction(nameof(GetPlayer), new { playerId = player!.PlayerId }, addPlayerResponse);
  }

  [HttpPatch("/info/{playerId}")]
  public async Task<ActionResult> UpdatePlayerInfo(int playerId, [FromBody] PlayerInfoRequest playerInfoRequest)
  {
    var playerToUpdate = await playerService.GetPlayer(playerId);

    if (playerToUpdate is null)
      return NotFound();

    var player = mapper.Map<Player>(playerInfoRequest);
    await playerService.UpdatePlayerInfo(playerId, player);
    return NoContent();
  }

  [HttpPatch("team/{playerId}")]
  public async Task<ActionResult> UpdatePlayerTeam(int playerId, PlayerTeamRequest playerTeamRequest)
  {
    var player = await playerService.GetPlayer(playerId);

    if (player is null)
      return NotFound();

    await playerService.UpdatePlayerTeam(playerId, playerTeamRequest);
    return NoContent();
  }
}