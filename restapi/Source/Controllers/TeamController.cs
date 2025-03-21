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
  public async Task<ActionResult<TeamResponse>> GetTeam(int teamId)
  {
    var teamResponse = await teamService.GetTeam(teamId);

    if (teamResponse is null)
      return NotFound();

    return Ok(teamResponse);
  }

  [HttpPost]
  public async Task<ActionResult> AddTeam([FromBody] AddTeamRequest addTeamRequest)
  {
    var teamResponse = await teamService.AddTeam(addTeamRequest);
    return CreatedAtAction(nameof(GetTeam), new { teamId = teamResponse!.TeamId }, teamResponse);
  }

  [HttpPost("update/{teamId}")]
  public async Task<ActionResult> UpdateTeam(int teamId, [FromBody] UpdateTeamRequest updateTeamRequest)
  {
    try
    {
      var team = await teamService.UpdateTeam(teamId, updateTeamRequest);

      if (team is null)
        return NotFound();
      
      return Ok(team);
    }
    catch (InvalidOperationException)
    {
      return BadRequest();
    }
  }

  [HttpPost("add-player/{teamId}")]
  public async Task<ActionResult> AddPlayerToTeam(int teamId, [FromBody] AddPlayerToTeamRequest addPlayerToTeamRequest)
  {
    try
    {
      var team = await teamService.AddPlayerToTeam(teamId, addPlayerToTeamRequest);

      if (team is null)
        return NotFound();

      return Ok(team);
    }
    catch (InvalidOperationException)
    {
      return BadRequest();
    }
  }

  [HttpPost("deactivate/{teamId}")]
  public async Task<ActionResult> DeactivateTeam(int teamId)
  {
    var team = await teamService.DeactivateTeam(teamId);

    if (team is null)
      return NotFound();

    return Ok(team);
  }
}