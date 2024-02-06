using EsportClash.Core.Players.Model;
using EsportClash.Core.Players.Commands.DeletePlayer;
using EsportClash.Core.Shared;
using EsportClash.Persistence.InMemory.Players;

namespace EsportClash.CoreTests.Players.Commands.DeletePlayer;

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

  private DeletePlayerCommandHandler CreateUseCase() {
    return new DeletePlayerCommandHandler(_playerRepository);
  }
  
  [Test]
  public async Task HappyPath_DeleteThePlayer() {
    var command = new DeletePlayerCommand {
      Id = _faker.Id
    };
    
    var useCase = CreateUseCase();
    await useCase.Handle(command, new CancellationToken());

    var player = await _playerRepository.FindByIdAsync(_faker.Id);
    Assert.That(player, Is.Null);
  }
  
  [Test]
  public async Task WhenTheUserDoesNotExist_ShouldFail() {
    var command = new DeletePlayerCommand {
      Id = "random-id"
    };
    
    var useCase = CreateUseCase();
    var exception = Assert.ThrowsAsync<NotFoundException>(async () => await useCase.Handle(command, new CancellationToken()));
    Assert.That(exception!.Message, Is.EqualTo("Player (random-id) was not found"));
  }
}