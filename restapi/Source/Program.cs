using BasketballLeagueApi.Core.Contracts;
using BasketballLeagueApi.Infrastructure.Data;
using BasketballLeagueApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IGameService, GameService>();

builder.Services.AddDbContext<DataContext>(options =>
{
  var connectionString = Environment.GetEnvironmentVariable("MSSQL_CONNECTION_STRING");
  options.UseSqlServer(connectionString, builder =>
  {
    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
  });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
  var db = scope.ServiceProvider.GetRequiredService<DataContext>();
  db.Database.Migrate();
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();