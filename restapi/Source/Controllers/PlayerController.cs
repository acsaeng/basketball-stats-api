using AutoMapper;
using BasketballStatsApi.Core.Contracts;
using BasketballStatsApi.Core.Dtos.Requests;
using BasketballStatsApi.Core.Dtos.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BasketballStatsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayerController(IPlayerService playerService) : ControllerBase
{
  [HttpGet("{playerId}")]
  public async Task<ActionResult<PlayerResponse>> GetPlayer(int playerId)
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
    var player = await playerService.UpdatePlayerInfo(playerId, updatePlayerInfoRequest);

    if (player is null)
      return NotFound();

    return Ok(player);
  }

  [HttpPatch("injury/{playerId}")]
  public async Task<ActionResult> UpdatePlayerInjury(int playerId, [FromBody] UpdatePlayerInjuryRequest updatePlayerInjuryRequest)
  {
    var player = await playerService.UpdatePlayerInjury(playerId, updatePlayerInjuryRequest);

    if (player is null)
      return NotFound();

    return Ok(player);
  }

  [HttpPatch("team/{playerId}")]
  public async Task<ActionResult> UpdatePlayerTeam(int playerId, [FromBody] UpdatePlayerTeamRequest updatePlayerTeamRequest)
  {
    var player = await playerService.UpdatePlayerTeam(playerId, updatePlayerTeamRequest);

    if (player is null)
      return NotFound();

    return Ok(player);
  }
}