using EsportClash.Core.Players.Model;
using EsportClash.Core.Teams.Model;
using EsportClash.Core.Teams.Queries.GetTeamById;
using EsportClash.Core.Teams.ViewModel;
using EsportClash.Persistence.InMemory.Players;
using EsportClash.Persistence.InMemory.Teams;

namespace EsportClash.CoreTests.Teams.Queries;

public class GetTeamByIdTests {
  private readonly InMemoryTeamRepository _teamRepository = new();
  private readonly InMemoryPlayerRepository _playerRepository = new();

  private Team _team;
  private Player _player;
  
  [OneTimeSetUp]
  public void SetUp() {
    _team = new Team {
      Id = "team",
      Name = "Team 1",
    };
    
    _player = new Player {
      Id = "player",
      Name = "Player 1",
      MainRole = Role.Top
    };
    
    _team.Join(_player, Role.Top);

    _playerRepository.CreateAsync(_player).Wait();
    _teamRepository.CreateAsync(_team).Wait();
  }

  private GetTeamByIdQueryHandler CreateQueryHandler() {
    return new GetTeamByIdQueryHandler(_teamRepository, _playerRepository);
  }
  
  [Test]
  public async Task ShouldGetTeamById() {
    var query = new GetTeamByIdQuery {
      Id = _team.Id
    };

    var handler = CreateQueryHandler();
    var result = await handler.Handle(query, CancellationToken.None);

    var expectedViewModel = new TeamViewModel {
      Id = _team.Id,
      Name = _team.Name,
      Members = new List<TeamViewModel.Member> {
        new TeamViewModel.Member {
          PlayerId = _player.Id,
          Name = _player.Name,
          Role = Role.Top.ToString()
        }
      }
    };
    
    Assert.That(result.Id, Is.EqualTo(expectedViewModel.Id));
    Assert.That(result.Name, Is.EqualTo(expectedViewModel.Name));
    Assert.That(result.Members[0], Is.EqualTo(expectedViewModel.Members[0]));
  }
}