using BasketballStatsApi.Core.Contracts;
using BasketballStatsApi.Core.Dtos.Requests;
using BasketballStatsApi.Core.Dtos.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BasketballStatsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController(IGameService gameService) : ControllerBase
{
  [HttpGet("{gameId}")]
  public async Task<ActionResult<GameResponse?>> GetGame(int gameId)
  {
    var gameResponse = await gameService.GetGame(gameId);

    if (gameResponse is null)
      return NotFound();

    return Ok(gameResponse);
  }

  [HttpPost]
  public async Task<ActionResult<GameResponse>> CreateGame([FromBody] CreateGameRequest createGameRequest)
  {
    try
    {
      var gameResponse = await gameService.CreateGame(createGameRequest);

      if (gameResponse is null)
        return NotFound();

      return CreatedAtAction(nameof(GetGame), new { gameId = gameResponse!.GameId }, gameResponse);
    }
    catch (InvalidOperationException)
    {
      return BadRequest();
    }
  }
}