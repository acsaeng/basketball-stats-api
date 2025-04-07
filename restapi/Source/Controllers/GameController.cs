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
  public async Task<ActionResult<ICollection<GameResponse>>> GetGamesByDateRange([FromBody] GetGamesByDateRangeRequest request)
  {
    try
    {
      var response = await gameService.GetGamesByDateRange(request);
      return Ok(response);
    }
    catch (InvalidOperationException error)
    {
      return BadRequest(error.Message);
    }
  }

  [HttpPost]
  public async Task<ActionResult<GameResponse?>> CreateGame([FromBody] CreateGameRequest request)
  {
    try
    {
      var response = await gameService.CreateGame(request);
      return CreatedAtAction(nameof(GetGameById), new { gameId = response!.GameId }, response);
    }
    catch (InvalidOperationException error)
    {
      return BadRequest(error.Message);
    }
  }

  [HttpPost("update-info/{gameId}")]
  public async Task<ActionResult<GameResponse?>> UpdateGameInfo(int gameId, [FromBody] UpdateGameInfoRequest request)
  {
    try
    {
      var response = await gameService.UpdateGameInfo(gameId, request);

      if (response is null)
        return NotFound();

      return Ok(response);
    }
    catch (InvalidOperationException error)
    {
      return BadRequest(error.Message);
    }
  }

  [HttpPost("update-status/{gameId}")]
  public async Task<ActionResult<GameResponse?>> UpdateGameStatus(int gameId, [FromBody] UpdateGameStatusRequest request)
  {
    try
    {
      var response = await gameService.UpdateGameStatus(gameId, request);

      if (response is null)
        return NotFound();

      return Ok(response);
    }
    catch (InvalidOperationException error) 
    {
      return BadRequest(error.Message);
    }
  }

  [HttpPost("finalize/{gameId}")]
  public async Task<ActionResult<GameResponse?>> FinalizeGame(int gameId, [FromBody] FinalizeGameRequest request)
  {
    try
    {
      var response = await gameService.FinalizeGame(gameId, request);

      if (response is null)
        return NotFound();

      return Ok(response);
    }
    catch (InvalidOperationException error)
    {
      return BadRequest(error.Message);
    }
  }
}