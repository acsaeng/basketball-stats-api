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
  public async Task<ActionResult<GameResponse?>> GetGameById(int gameId)
  {
    var gameResponse = await gameService.GetGameById(gameId);

    if (gameResponse is null)
      return NotFound();

    return Ok(gameResponse);
  }
  
  [HttpGet]
  public async Task<ActionResult<ICollection<GameResponse>>> GetGamesByDate([FromBody] GetGamesByDateRequest getGamesByDateRequest)
  {
    var gameResponses = await gameService.GetGamesByDate(getGamesByDateRequest);
    return Ok(gameResponses);
  }

  [HttpPost]
  public async Task<ActionResult<GameResponse?>> CreateGame([FromBody] CreateGameRequest createGameRequest)
  {
    try
    {
      var gameResponse = await gameService.CreateGame(createGameRequest);

      if (gameResponse is null)
        return NotFound();

      return CreatedAtAction(nameof(GetGameById), new { gameId = gameResponse!.GameId }, gameResponse);
    }
    catch (InvalidOperationException)
    {
      return BadRequest();
    }
  }

  [HttpPost("update-info/{gameId}")]
  public async Task<ActionResult<GameResponse?>> UpdateGameInfo(int gameId, [FromBody] UpdateGameInfoRequest updateGameInfoRequest)
  {
    try
    {
      var gameResponse = await gameService.UpdateGameInfo(gameId, updateGameInfoRequest);

      if (gameResponse is null)
        return NotFound();

      return Ok(gameResponse);
    }
    catch (InvalidOperationException)
    {
      return BadRequest();
    }
  }
  
  [HttpPost("update-status/{gameId}")]
  public async Task<ActionResult<GameResponse?>> UpdateGameStatus(int gameId, [FromBody] UpdateGameStatusRequest updateGameStatusRequest)
  {
    try
    {
      var gameResponse = await gameService.UpdateGameStatus(gameId, updateGameStatusRequest);

      if (gameResponse is null)
        return NotFound();

      return Ok(gameResponse);
    }
    catch (InvalidOperationException)
    {
      return BadRequest();
    }
  }
}