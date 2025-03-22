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
    var teamResponse = await teamService.GetTeam(teamId);

    if (teamResponse is null)
      return NotFound();

    return Ok(teamResponse);
  }
  
  [HttpGet("players/{teamId}")]
  public async Task<ActionResult<ICollection<PlayerResponse>?>> GetTeamRoster(int teamId)
  {
    var playersResponse = await teamService.GetTeamRoster(teamId);
    
    if (playersResponse is null)
      return NotFound();

    return Ok(playersResponse);
  }
  
  [HttpPost]
  public async Task<ActionResult<TeamResponse>> CreateTeam([FromBody] CreateTeamRequest createTeamRequest)
  {
    var teamResponse = await teamService.CreateTeam(createTeamRequest);
    return CreatedAtAction(nameof(GetTeam), new { teamId = teamResponse!.TeamId }, teamResponse);
  }

  [HttpPost("update/{teamId}")]
  public async Task<ActionResult<TeamResponse?>> UpdateTeam(int teamId, [FromBody] UpdateTeamRequest updateTeamRequest)
  {
    try
    {
      var teamResponse = await teamService.UpdateTeam(teamId, updateTeamRequest);

      if (teamResponse is null)
        return NotFound();
      
      return Ok(teamResponse);
    }
    catch (InvalidOperationException)
    {
      return BadRequest();
    }
  }

  [HttpPost("add-player/{teamId}")]
  public async Task<ActionResult<ICollection<PlayerResponse>?>> AddPlayerToTeam(int teamId, [FromBody] AddPlayerToTeamRequest addPlayerToTeamRequest)
  {
    try
    {
      var playersResponse = await teamService.AddPlayerToTeam(teamId, addPlayerToTeamRequest);

      if (playersResponse is null)
        return NotFound();

      return Ok(playersResponse);
    }
    catch (InvalidOperationException)
    {
      return BadRequest();
    }
  }

  [HttpPost("deactivate/{teamId}")]
  public async Task<ActionResult<TeamResponse?>> DeactivateTeam(int teamId)
  {
    var teamResponse = await teamService.DeactivateTeam(teamId);

    if (teamResponse is null)
      return NotFound();

    return Ok(teamResponse);
  }
}