using EsportClash.Core.Players.Model;
using EsportClash.Core.Players.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace EsportClash.APITests.Players;

public class DeletePlayerTests {
  private readonly WebTestsFixture _app = new();

  [SetUp]
  public async Task SetUp() {
    var playerRepository = _app.Services.GetRequiredService<IPlayerRepository>();
    await playerRepository.ClearAsync();
  }

  private async Task<Player> CreateAndSavePlayer() {
    var playerRepository = _app.Services.GetRequiredService<IPlayerRepository>();

    var player = new Player {
      Id = "123",
      Name = "Test Player",
      MainRole = Role.Middle
    };

    await playerRepository.CreateAsync(player);
    return player;
  }

  [Test]
  public async Task ShouldDeleteThePlayer() {
    var player = await CreateAndSavePlayer();

    var client = _app.CreateClient();
    var response = await client.DeleteAsync($"/players/{player.Id}");
    response.EnsureSuccessStatusCode();

    using var scope = _app.Services.CreateScope();
    var playerRepository = scope.ServiceProvider.GetRequiredService<IPlayerRepository>();
    var deletedPlayer = await playerRepository.FindByIdAsync(player.Id);

    Assert.That(deletedPlayer, Is.Null);
  }
}