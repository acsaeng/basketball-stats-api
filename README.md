# Basketball Stats API

**Author:** Aron Saengchan

## Summary

A REST API developed in ASP.NET Core that is designed to manage all aspects of a basketball league. It offers a set of endpoints that allow users to query and update a SQL Server database through the use of Entity Framework Core. The API tracks a wide range of data, including detailed information on players, teams, and games, as well as their overall statistics. It also manages the relationships between them, enabling features such as team schedules, team standings, and league leaders.

To run the application:

- Ensure you have [Docker Desktop](https://www.docker.com/products/docker-desktop/) installed on your device
- Enter a valid SQL Server SA password in the `.env` file
- Build and run the containers by executing the `docker-compose up` command
- Call the endpoints using:
  - Postman - Import the collection found in `restapi/Source/Postman`
  - Swagger - Access http://localhost:5057/swagger in the browser
- Stop the containers by executing the `docker-compose down` command once finished
