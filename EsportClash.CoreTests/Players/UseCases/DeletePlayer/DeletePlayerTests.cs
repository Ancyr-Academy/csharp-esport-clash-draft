using EsportClash.Core.Players.Model;
using EsportClash.Core.Players.UseCases.DeletePlayer;
using EsportClash.Core.Shared;
using EsportClash.Persistence.InMemory.Players;

namespace EsportClash.CoreTests.Players.UseCases.DeletePlayer;

public class DeletePlayerTests {
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

  private DeletePlayerUseCase CreateUseCase() {
    return new DeletePlayerUseCase(_playerRepository);
  }
  
  [Test]
  public async Task HappyPath_DeleteThePlayer() {
    var command = new DeletePlayerCommand {
      Id = _faker.Id
    };
    
    var useCase = CreateUseCase();
    await useCase.Execute(command);

    var player = await _playerRepository.FindByIdAsync(_faker.Id);
    Assert.That(player, Is.Null);
  }
  
  [Test]
  public async Task WhenTheUserDoesNotExist_ShouldFail() {
    var command = new DeletePlayerCommand {
      Id = "random-id"
    };
    
    var useCase = CreateUseCase();
    var exception = Assert.ThrowsAsync<NotFoundException>(async () => await useCase.Execute(command));
    Assert.That(exception!.Message, Is.EqualTo("Player (random-id) was not found"));
  }
}