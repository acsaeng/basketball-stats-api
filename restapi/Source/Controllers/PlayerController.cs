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
    var response = await playerService.GetPlayer(playerId);

    if (response is null)
      return NotFound();

    return Ok(response);
  }

  [HttpGet("leaders/{statType}")]
  public async Task<ActionResult<ICollection<PlayerResponse>>> GetLeagueLeaders(string statType)
  {
    try
    {
      var response = await playerService.GetLeagueLeaders(statType);
      return Ok(response);
    }
    catch (ArgumentException)
    {
      return BadRequest();
    }
  }

  [HttpPost]
  public async Task<ActionResult<PlayerResponse>> CreatePlayer([FromBody] CreatePlayerRequest request)
  {
    var response = await playerService.CreatePlayer(request);
    return CreatedAtAction(nameof(GetPlayer), new { playerId = response!.PlayerId }, response);
  }

  [HttpPost("info/{playerId}")]
  public async Task<ActionResult<PlayerResponse?>> UpdatePlayerInfo(int playerId, [FromBody] UpdatePlayerInfoRequest request)
  {
    var response = await playerService.UpdatePlayerInfo(playerId, request);

    if (response is null)
      return NotFound();

    return Ok(response);
  }

  [HttpPost("injury/{playerId}")]
  public async Task<ActionResult<PlayerResponse?>> UpdatePlayerInjury(int playerId, [FromBody] UpdatePlayerInjuryRequest request)
  {
    var response = await playerService.UpdatePlayerInjury(playerId, request);

    if (response is null)
      return NotFound();

    return Ok(response);
  }

  [HttpPost("roster/{playerId}")]
  public async Task<ActionResult<PlayerResponse?>> UpdatePlayerRosterStatus(int playerId, [FromBody] UpdatePlayerRosterStatusRequest request)
  {
    var response = await playerService.UpdatePlayerRosterStatus(playerId, request);

    if (response is null)
      return NotFound();

    return Ok(response);
  }
}