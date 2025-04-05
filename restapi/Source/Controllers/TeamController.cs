using BasketballStatsApi.Core.Contracts;
using BasketballStatsApi.Core.Dtos.Requests;
using BasketballStatsApi.Core.Dtos.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BasketballStatsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamController(ITeamService teamService) : ControllerBase
{
  [HttpGet("{teamId}")]
  public async Task<ActionResult<TeamResponse?>> GetTeam(int teamId)
  {
    var response = await teamService.GetTeam(teamId);

    if (response is null)
      return NotFound();

    return Ok(response);
  }

  [HttpGet("roster/{teamId}")]
   public async Task<ActionResult<ICollection<PlayerResponse>?>> GetTeamRosterStats(int teamId)
   {
     var response = await teamService.GetTeamRosterStats(teamId);
 
     if (response is null)
       return NotFound();
 
     return Ok(response);
   }

  [HttpGet("schedule/{teamId}")]
  public async Task<ActionResult<ICollection<GameResponse>?>> GetTeamSchedule(int teamId)
  {
    var response = await teamService.GetTeamSchedule(teamId);

    if (response is null)
      return NotFound();
    
    return Ok(response);
  }

  [HttpGet("standings")]
  public async Task<ActionResult<ICollection<TeamResponse>>> GetTeamStandings()
  {
    var response = await teamService.GetTeamStandings();
    return Ok(response);
  }

  [HttpPost]
  public async Task<ActionResult<TeamResponse>> CreateTeam([FromBody] CreateTeamRequest request)
  {
    try
    {
      var response = await teamService.CreateTeam(request);
      return CreatedAtAction(nameof(GetTeam), new { teamId = response!.TeamId }, response);
    }
    catch (ArgumentException error)
    {
      return BadRequest(error.Message);
    }
  }

  [HttpPost("update/{teamId}")]
  public async Task<ActionResult<TeamResponse?>> UpdateTeam(int teamId, [FromBody] UpdateTeamRequest request)
  {
    try
    {
      var response = await teamService.UpdateTeam(teamId, request);

      if (response is null)
        return NotFound();

      return Ok(response);
    }
    catch (InvalidOperationException error)
    {
      return BadRequest(error.Message);
    }
  }

  [HttpPost("add-player/{teamId}")]
  public async Task<ActionResult<ICollection<PlayerResponse>?>> AddPlayerToRoster(int teamId, [FromBody] AddPlayerToRosterRequest request)
  {
    try
    {
      var response = await teamService.AddPlayerToRoster(teamId, request);

      if (response is null)
        return NotFound();

      return Ok(response);
    }
    catch (InvalidOperationException error)
    {
      return BadRequest(error.Message);
    }
  }

  [HttpPost("deactivate/{teamId}")]
  public async Task<ActionResult<TeamResponse?>> DeactivateTeam(int teamId)
  {
    var response = await teamService.DeactivateTeam(teamId);

    if (response is null)
      return NotFound();

    return Ok(response);
  }
}