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
  public async Task<ActionResult> AddPlayer([FromBody] AddPlayerRequest addPlayerRequest)
  {
    var newPlayer = mapper.Map<Player>(addPlayerRequest);
    var player = await playerService.AddPlayer(newPlayer);
    var addPlayerResponse = mapper.Map<PlayerResponse>(player);
    return CreatedAtAction(nameof(GetPlayer), new { playerId = player!.PlayerId }, addPlayerResponse);
  }

  [HttpPut("roster/free-agent/{playerId}")]
  public async Task<ActionResult> MakePlayerFreeAgent(int playerId)
  {
    var player = await playerService.GetPlayer(playerId);

    if (player is null)
      return NotFound();

    await playerService.MakePlayerFreeAgent(playerId);
    return NoContent();
  }

  [HttpPut("roster/retire/{playerId}")]
  public async Task<ActionResult> RetirePlayer(int playerId)
  {
    var player = await playerService.GetPlayer(playerId);

    if (player is null)
      return NotFound();

    await playerService.RetirePlayer(playerId);
    return NoContent();
  }
}