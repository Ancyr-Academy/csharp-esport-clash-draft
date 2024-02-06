using EsportClash.Core.Players.Model;
using EsportClash.Core.Players.UseCases.CreatePlayer;
using EsportClash.Core.Shared.Id;
using EsportClash.Persistence.InMemory.Players;

namespace EsportClash.CoreTests.Players.UseCases.CreatePlayer;

public class CreatePlayerTests {
  private readonly FixedIdProvider _idProvider = new FixedIdProvider();
  private readonly InMemoryPlayerRepository _playerRepository = new InMemoryPlayerRepository();

  [SetUp]
  public void Setup() {
    _playerRepository.Clear();
  }

  private CreatePlayerUseCase CreateUseCase() {
    return new CreatePlayerUseCase(_idProvider, _playerRepository);
  }

  [Test]
  public async Task HappyPath_ShouldReturnId() {
    var inputDto = new CreatePlayerCommand {
      Name = "Faker",
      MainRole = Role.Mid
    };
    
    var useCase = CreateUseCase();
    var result = await useCase.Execute(inputDto);

    Assert.That(result.Id, Is.EqualTo(FixedIdProvider.Id));  
  }
  
  [Test]
  public async Task HappyPath_ShouldStoreUser() {
    var inputDto = new CreatePlayerCommand {
      Name = "Faker",
      MainRole = Role.Mid
    };
    
    var useCase = CreateUseCase();
    var result = await useCase.Execute(inputDto);

    var createdPlayer = await _playerRepository.FindByIdAsync(result.Id);
    
    Assert.NotNull(createdPlayer);
    Assert.That(createdPlayer.Name, Is.EqualTo("Faker"));
    Assert.That(createdPlayer.MainRole, Is.EqualTo(Role.Mid));
  }

}