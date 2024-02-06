using EsportClash.Core.Players.Model;
using EsportClash.Core.Players.UseCases.GetAllPlayers;
using EsportClash.Core.Players.UseCases.GetPlayerById;
using EsportClash.Core.Shared;
using EsportClash.Persistence.InMemory.Players;

namespace EsportClash.CoreTests.Players.UseCases.GetAllPlayers;

public class GetAllPlayerTests {
  private readonly Player _faker = new Player {
    Id = "1",
    Name = "Faker",
    MainRole = Role.Middle
  };
  
  private readonly InMemoryPlayerRepository _playerRepository = new InMemoryPlayerRepository();
  
  [SetUp]
  public async Task Setup() {
    _playerRepository.Clear();
    await _playerRepository.CreateAsync(_faker);
  }

  private GetAllPlayersUseCase CreateUseCase() {
    return new GetAllPlayersUseCase(_playerRepository);
  }

  [Test]
  public async Task HappyPath_ShouldGetAllPlayers() {
    var inputDto = new GetAllPlayersCommand();
    
    var useCase = CreateUseCase();
    var result = await useCase.Execute(inputDto);

    Assert.That(result.Count, Is.EqualTo(1));
    Assert.That(result[0].Id, Is.EqualTo("1"));
    Assert.That(result[0].Name, Is.EqualTo("Faker"));
    Assert.That(result[0].Role, Is.EqualTo("Middle"));
  }

}