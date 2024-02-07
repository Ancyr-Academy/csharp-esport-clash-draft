using EsportClash.Core.Players.Model;
using EsportClash.Core.Players.Queries.GetAllPlayers;
using EsportClash.Core.Players.Queries.GetPlayerById;
using EsportClash.Core.Shared;
using EsportClash.Persistence.InMemory.Players;

namespace EsportClash.CoreTests.Players.Queries;

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

  private GetAllPlayersQueryHandler CreateQueryHandler() {
    return new GetAllPlayersQueryHandler(_playerRepository);
  }

  [Test]
  public async Task HappyPath_ShouldGetAllPlayers() {
    var inputDto = new GetAllPlayersQuery();
    
    var useCase = CreateQueryHandler();
    var result = await useCase.Handle(inputDto, CancellationToken.None);

    Assert.That(result.Count, Is.EqualTo(1));
    Assert.That(result[0].Id, Is.EqualTo("1"));
    Assert.That(result[0].Name, Is.EqualTo("Faker"));
    Assert.That(result[0].Role, Is.EqualTo("Middle"));
  }

}