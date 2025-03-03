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
  public async Task<ActionResult<PlayerResponse?>> GetPlayer(int playerId)
  {
    var playerResponse = await playerService.GetPlayer(playerId);

    if (playerResponse is null)
      return NotFound();
    
    return Ok(playerResponse);
  }

  [HttpPost]
  public async Task<ActionResult> AddPlayer([FromBody] AddPlayerRequest addPlayerRequest)
  {
    var playerResponse = await playerService.AddPlayer(addPlayerRequest);
    return CreatedAtAction(nameof(GetPlayer), new { playerId = playerResponse!.PlayerId }, playerResponse);
  }

  [HttpPatch("info/{playerId}")]
  public async Task<ActionResult> UpdatePlayerInfo(int playerId, [FromBody] UpdatePlayerInfoRequest updatePlayerInfoRequest)
  {
    var playerToUpdate = await playerService.GetPlayer(playerId);

    if (playerToUpdate is null)
      return NotFound();
    
    await playerService.UpdatePlayerInfo(playerId, updatePlayerInfoRequest);
    return NoContent();
  }

  [HttpPatch("injury/{playerId}")]
  public async Task<ActionResult> UpdatePlayerInjury(int playerId, UpdatePlayerInjuryRequest updatePlayerInjuryRequest)
  {
    var player = await playerService.GetPlayer(playerId);

    if (player is null)
      return NotFound();

    await playerService.UpdatePlayerInjury(playerId, updatePlayerInjuryRequest);
    return NoContent();
  }
  
  [HttpPatch("team/{playerId}")]
  public async Task<ActionResult> UpdatePlayerTeam(int playerId, UpdatePlayerTeamRequest updatePlayerTeamRequest)
  {
    var player = await playerService.GetPlayer(playerId);

    if (player is null)
      return NotFound();

    await playerService.UpdatePlayerTeam(playerId, updatePlayerTeamRequest);
    return NoContent();
  }
}