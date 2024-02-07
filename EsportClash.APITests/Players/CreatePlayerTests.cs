using System.Text;
using EsportClash.Core.Players.Commands.CreatePlayer;
using EsportClash.Core.Players.Model;
using EsportClash.Core.Players.Ports;
using EsportClash.Core.Shared;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace EsportClash.APITests.Players;

public class CreatePlayerTests {
  private readonly WebTestsFixture _app = new();

  [SetUp]
  public async Task SetUp() {
    var playerRepository = _app.Services.GetRequiredService<IPlayerRepository>();
    await playerRepository.ClearAsync();
  }

  [Test]
  public async Task ShouldCreateThePlayer() {
    var command = new CreatePlayerCommand {
      Name = "Player",
      MainRole = Role.Middle
    };

    var data = new StringContent(
      JsonConvert.SerializeObject(command),
      Encoding.UTF8,
      "application/json"
    );

    var client = _app.CreateClient();
    var response = await client.PostAsync("/players", data);
    response.EnsureSuccessStatusCode();

    var content = await response.Content.ReadAsStringAsync();
    var idResponse = JsonConvert.DeserializeObject<IdResponse>(content);

    var playerRepository = _app.Services.GetRequiredService<IPlayerRepository>();
    var createdPlayer = await playerRepository.FindByIdAsync(idResponse.Id);

    Assert.That(createdPlayer, Is.Not.Null);
    Assert.That(createdPlayer.Name, Is.EqualTo(command.Name));
    Assert.That(createdPlayer.MainRole, Is.EqualTo(command.MainRole));
  }
}