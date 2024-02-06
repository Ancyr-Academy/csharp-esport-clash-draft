using EsportClash.Core.Players.Model;
using EsportClash.Core.Players.UseCases.GetPlayerById;
using EsportClash.Core.Shared;
using EsportClash.Persistence.InMemory.Players;

namespace EsportClash.CoreTests.Players.UseCases.GetPlayerById;

public class GetPlayerByIdTests {
  private readonly Player _faker = new Player {
    Id = "1",
    Name = "Faker",
    MainRole = Role.Mid
  };
  
  private readonly InMemoryPlayerRepository _playerRepository = new InMemoryPlayerRepository();
  
  [SetUp]
  public async Task Setup() {
    _playerRepository.Clear();
    await _playerRepository.CreateAsync(_faker);
  }

  private GetPlayerByIdUseCase CreateUseCase() {
    return new GetPlayerByIdUseCase(_playerRepository);
  }

  [Test]
  public async Task HappyPath_ShouldGetPlayer() {
    var inputDto = new GetPlayerByIdCommand {
      Id = "1"
    };
    
    var useCase = CreateUseCase();
    var result = await useCase.Execute(inputDto);

    Assert.NotNull(result);
    Assert.That(result.Id, Is.EqualTo("1"));
    Assert.That(result.Name, Is.EqualTo("Faker"));
    Assert.That(result.Role, Is.EqualTo("Mid"));
  }
  
  [Test]
  public async Task WhenNotFound_ShouldThrowNotFoundException() {
    var inputDto = new GetPlayerByIdCommand {
      Id = "2"
    };
    
    var useCase = CreateUseCase();
    var exception = Assert.ThrowsAsync<NotFoundException>(async () => await useCase.Execute(inputDto));
    Assert.That(exception!.Message, Is.EqualTo("Player (2) was not found"));
  }
}