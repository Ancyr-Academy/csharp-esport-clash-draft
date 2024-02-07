using EsportClash.Core.Players.Model;
using EsportClash.Core.Players.Queries.GetPlayerById;
using EsportClash.Core.Shared;
using EsportClash.Persistence.InMemory.Players;

namespace EsportClash.CoreTests.Players.Queries.GetPlayerById;

public class GetPlayerByIdTests {
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

  private GetPlayerByIdUseCase CreateQueryHandler() {
    return new GetPlayerByIdUseCase(_playerRepository);
  }

  [Test]
  public async Task HappyPath_ShouldGetPlayer() {
    var inputDto = new GetPlayerByIdQuery {
      Id = "1"
    };
    
    var useCase = CreateQueryHandler();
    var result = await useCase.Handle(inputDto, CancellationToken.None);

    Assert.NotNull(result);
    Assert.That(result.Id, Is.EqualTo("1"));
    Assert.That(result.Name, Is.EqualTo("Faker"));
    Assert.That(result.Role, Is.EqualTo("Middle"));
  }
  
  [Test]
  public void WhenNotFound_ShouldThrowNotFoundException() {
    var inputDto = new GetPlayerByIdQuery {
      Id = "2"
    };
    
    var useCase = CreateQueryHandler();
    var exception = Assert.ThrowsAsync<NotFoundException>(async () => await useCase.Handle(inputDto, CancellationToken.None));
    Assert.That(exception!.Message, Is.EqualTo("Player (2) was not found"));
  }
}