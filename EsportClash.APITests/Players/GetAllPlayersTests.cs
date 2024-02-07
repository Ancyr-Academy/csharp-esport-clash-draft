using EsportClash.Core.Players.Model;
using EsportClash.Core.Players.Ports;
using EsportClash.Core.Players.ViewModel;
using EsportClash.Persistence.SQL.Modules.Players;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace EsportClash.APITests.Players;

public class GetAllPlayersTests {
  private readonly WebTestsFixture _app = new();
  
  [SetUp]
  public async Task SetUp() {
    IPlayerRepository playerRepository = _app.Services.GetRequiredService<IPlayerRepository>();
    await playerRepository.ClearAsync();
  }
  
  private async Task<Player> CreateAndSavePlayer() {
    var playerRepository = _app.Services.GetRequiredService<IPlayerRepository>();
    
    var player = new Player {
      Id = "player",
      Name = "Test Player",
      MainRole = Role.Middle
    };

    await playerRepository.CreateAsync(player);
    return player;
  }
  
  [Test]
  public async Task ShouldGetAllPlayers() {
    var player = await CreateAndSavePlayer();
    
    var client = _app.CreateClient();
    var response = await client.GetAsync("/players");
    response.EnsureSuccessStatusCode();
    
    var content = await response.Content.ReadAsStringAsync();
    
    List<PlayerViewModel> players = JsonConvert.DeserializeObject<List<PlayerViewModel>>(content);

    Assert.That(players.Count, Is.EqualTo(1));
    Assert.That(players[0].Id, Is.EqualTo(player.Id));
    Assert.That(players[0].Name, Is.EqualTo(player.Name));
    Assert.That(players[0].Role, Is.EqualTo(player.MainRole.ToString()));
  }
}