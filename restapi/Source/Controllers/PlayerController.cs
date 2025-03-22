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
  public async Task<ActionResult<PlayerResponse?>> GetPlayer(int playerId)
  {
    var playerResponse = await playerService.GetPlayer(playerId);

    if (playerResponse is null)
      return NotFound();

    return Ok(playerResponse);
  }

  [HttpPost]
  public async Task<ActionResult<PlayerResponse>> CreatePlayer([FromBody] CreatePlayerRequest createPlayerRequest)
  {
    var playerResponse = await playerService.CreatePlayer(createPlayerRequest);
    return CreatedAtAction(nameof(GetPlayer), new { playerId = playerResponse!.PlayerId }, playerResponse);
  }

  [HttpPost("info/{playerId}")]
  public async Task<ActionResult> UpdatePlayerInfo(int playerId, [FromBody] UpdatePlayerInfoRequest updatePlayerInfoRequest)
  {
    var player = await playerService.UpdatePlayerInfo(playerId, updatePlayerInfoRequest);

    if (player is null)
      return NotFound();

    return Ok(player);
  }

  [HttpPost("injury/{playerId}")]
  public async Task<ActionResult<PlayerResponse?>> UpdatePlayerInjury(int playerId, [FromBody] UpdatePlayerInjuryRequest updatePlayerInjuryRequest)
  {
    var player = await playerService.UpdatePlayerInjury(playerId, updatePlayerInjuryRequest);

    if (player is null)
      return NotFound();

    return Ok(player);
  }

  [HttpPost("roster-status/{playerId}")]
  public async Task<ActionResult<PlayerResponse?>> UpdatePlayerRosterStatus(int playerId, [FromBody] UpdatePlayerRosterStatusRequest updatePlayerRosterStatusRequest)
  {
    var player = await playerService.UpdatePlayerRosterStatus(playerId, updatePlayerRosterStatusRequest);

    if (player is null)
      return NotFound();

    return Ok(player);
  }
}