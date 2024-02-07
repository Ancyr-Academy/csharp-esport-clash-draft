using EsportClash.Core.Players.Model;
using EsportClash.Core.Teams.Model;
using EsportClash.Core.Teams.Queries.GetAllTeams;
using EsportClash.Core.Teams.Queries.GetTeamById;
using EsportClash.Core.Teams.ViewModel;
using EsportClash.Persistence.InMemory.Players;
using EsportClash.Persistence.InMemory.Teams;

namespace EsportClash.CoreTests.Teams.Queries;

public class GetAllTeamsTests {
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

  private GetAllTeamsQueryHandler CreateQueryHandler() {
    return new GetAllTeamsQueryHandler(_teamRepository, _playerRepository);
  }
  
  [Test]
  public async Task ShouldGetAllTeams() {
    var query = new GetAllTeamsQuery();

    var handler = CreateQueryHandler();
    var result = await handler.Handle(query, CancellationToken.None);

    var expectedFirstTeam = new TeamViewModel {
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
    
    Assert.That(result.Count, Is.EqualTo(1));
    Assert.That(result[0].Id, Is.EqualTo(expectedFirstTeam.Id));
    Assert.That(result[0].Name, Is.EqualTo(expectedFirstTeam.Name));
    Assert.That(result[0].Members[0], Is.EqualTo(expectedFirstTeam.Members[0]));
  }
}