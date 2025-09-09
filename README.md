# Basketball League API

## Summary

A REST API developed in ASP.NET Core that is designed to manage all aspects of a basketball league. It provides a series of endpoints that allow users to query and update a SQL Server database through the use of Entity Framework Core. The API tracks a wide range of data, including detailed information on players, teams, and games, as well as their overall statistics. It also manages the relationships between them, enabling features such as viewing team schedules, team standings, and league leaders.

## Endpoints

<table align="center">
  <tbody>
    <tr>
      <th colspan="3">Player</th>
    </tr>
    <tr>
      <th>Name</th>
      <th>Type</th>
      <th>Endpoint</th>
    </tr>
    <tr>
      <td>Get Player</td>
      <td><code>GET</code></td>
      <td><code>/api/player</code></td>
    </tr>
    <tr>
      <td>Get League Leaders</td>
      <td><code>GET</code></td>
      <td><code>/api/player/leaders/{stat}</code></td>
    </tr>
    <tr>
      <td>Create Player</td>
      <td><code>POST</code></td>
      <td><code>/api/player</code></td>
    </tr>
    <tr>
      <td>Update Player Info</td>
      <td><code>POST</code></td>
      <td><code>/api/player/info/{id}</code></td>
    </tr>
    <tr>
      <td>Update Player Injury Status</td>
      <td><code>POST</code></td>
      <td><code>/api/player/injury/{id}</code></td>
    </tr>
    <tr>
      <td>Update Player Roster Status</td>
      <td><code>POST</code></td>
      <td><code>/api/player/roster/{id}</code></td>
    </tr>
  </tbody>
</table>

<br />

<table align="center">
  <tbody>
    <tr>
      <th colspan="3">Team</th>
    </tr>
    <tr>
      <th>Name</th>
      <th>Type</th>
      <th>Endpoint</th>
    </tr>
    <tr>
      <td>Get Team</td>
      <td><code>GET</code></td>
      <td><code>/api/team/{id}</code></td>
    </tr>
    <tr>
      <td>Get Team Roster Stats</td>
      <td><code>GET</code></td>
      <td><code>/api/team/roster/{id}</code></td>
    </tr>
    <tr>
      <td>Get Team Schedule</td>
      <td><code>GET</code></td>
      <td><code>/api/team/schedule/{id}</code></td>
    </tr>
    <tr>
      <td>Get Team Standings</td>
      <td><code>GET</code></td>
      <td><code>/api/team/standings</code></td>
    </tr>
    <tr>
      <td>Create Team</td>
      <td><code>POST</code></td>
      <td><code>/api/team</code></td>
    </tr>
    <tr>
      <td>Update Team</td>
      <td><code>POST</code></td>
      <td><code>/api/team/update/{id}</code></td>
    </tr>
    <tr>
      <td>Add Player to Roster</td>
      <td><code>POST</code></td>
      <td><code>/api/team/add-player/{player-id}</code></td>
    </tr>
    <tr>
      <td>Deactivate Team</td>
      <td><code>POST</code></td>
      <td><code>/api/team/deactivate/{id}</code></td>
    </tr>
  </tbody>
</table>

<br />

<table align="center" width="50%">
  <tbody>
    <tr>
      <th colspan="3">Game</th>
    </tr>
    <tr>
      <th>Name</th>
      <th>Type</th>
      <th>Endpoint</th>
    </tr>
    <tr>
      <td>Get Game by ID</td>
      <td><code>GET</code></td>
      <td><code>/api/game/1</code></td>
    </tr>
    <tr>
      <td>Get Games by Date Range</td>
      <td><code>GET</code></td>
      <td><code>/api/game</code></td>
    </tr>
    <tr>
      <td>Create Game</td>
      <td><code>POST</code></td>
      <td><code>/api/game</code></td>
    </tr>
    <tr>
      <td>Update Game Info</td>
      <td><code>POST</code></td>
      <td><code>/api/game/info/{id}</code></td>
    </tr>
    <tr>
      <td>Update Game Status</td>
      <td><code>POST</code></td>
      <td><code>/api/game/status/{id}</code></td>
    </tr>
    <tr>
      <td>Finalize Game</td>
      <td><code>POST</code></td>
      <td><code>/api/game/finalize/{id}</code></td>
    </tr>
  </tbody>
</table>
