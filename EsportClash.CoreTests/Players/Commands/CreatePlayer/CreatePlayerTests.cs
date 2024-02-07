using EsportClash.Core.Players.Model;
using EsportClash.Core.Players.Commands.CreatePlayer;
using EsportClash.Core.Shared.Id;
using EsportClash.Persistence.InMemory.Players;

namespace EsportClash.CoreTests.Players.Commands.CreatePlayer;

public class CreatePlayerTests {
  private readonly FixedIdProvider _idProvider = new FixedIdProvider();
  private readonly InMemoryPlayerRepository _playerRepository = new InMemoryPlayerRepository();

  [SetUp]
  public void Setup() {
    _playerRepository.Clear();
  }

  private CreatePlayerCommandHandler CreateCommandHandler() {
    return new CreatePlayerCommandHandler(_idProvider, _playerRepository);
  }

  [Test]
  public async Task HappyPath_ShouldReturnId() {
    var inputDto = new CreatePlayerCommand {
      Name = "Faker",
      MainRole = Role.Middle
    };
    
    var useCase = CreateCommandHandler();
    var result = await useCase.Handle(inputDto, CancellationToken.None);

    Assert.That(result.Id, Is.EqualTo(FixedIdProvider.Id));  
  }
  
  [Test]
  public async Task HappyPath_ShouldStoreUser() {
    var inputDto = new CreatePlayerCommand {
      Name = "Faker",
      MainRole = Role.Middle
    };
    
    var useCase = CreateCommandHandler();
    var result = await useCase.Handle(inputDto, CancellationToken.None);

    var createdPlayer = await _playerRepository.FindByIdAsync(result.Id);
    
    Assert.NotNull(createdPlayer);
    Assert.That(createdPlayer.Name, Is.EqualTo("Faker"));
    Assert.That(createdPlayer.MainRole, Is.EqualTo(Role.Middle));
  }

}