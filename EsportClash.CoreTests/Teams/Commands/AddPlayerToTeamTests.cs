using EsportClash.Core.Players.Model;
using EsportClash.Core.Shared;
using EsportClash.Core.Teams.Commands.AddPlayerToTeam;
using EsportClash.Core.Teams.Model;
using EsportClash.Persistence.InMemory.Players;
using EsportClash.Persistence.InMemory.Teams;

namespace EsportClash.CoreTests.Teams.Commands;

public class AddPlayerToTeamTests {
  private Team _team;
  private Player _player;
  
  private readonly InMemoryPlayerRepository _playerRepository = new();
  private readonly InMemoryTeamRepository _teamRepository = new();
  
  private Player CreatePlayer(string name, Role mainRole) {
    return new Player {
      Id = name,
      Name = name,
      MainRole = mainRole
    };
  }

  private Team CreateTeam(string name) {
    return new Team {
      Id = name,
      Name = name
    };
  }

  private AddPlayerToTeamCommandHandler CreateCommandHandler() {
    return new AddPlayerToTeamCommandHandler(
      _teamRepository,
      _playerRepository
    );
  }

  [SetUp]
  public async Task SetUp() {
    _playerRepository.Clear();
    _teamRepository.Clear();
    
    _team = CreateTeam("Team");
    _player = CreatePlayer("player", Role.Middle);
    
    await _teamRepository.CreateAsync(_team);
    await _playerRepository.CreateAsync(_player);
  }
  
  [Test]
  public async Task ShouldAddPlayerToTeam() {
    var command = new AddPlayerToTeamCommand {
      PlayerId = _player.Id,
      TeamId = _team.Id,
      Role = Role.Middle
    };

    var handler = CreateCommandHandler();
    await handler.Handle(command, CancellationToken.None);
    
    var team = await _teamRepository.FindByIdAsync(_team.Id);
    Assert.That(team.HasPlayer(_player), Is.True);
  }  
  
  [Test]
  public void WhenPlayerDoesNotExist_ShouldFail() {
    var command = new AddPlayerToTeamCommand {
      PlayerId = "random-id",
      TeamId = _team.Id,
      Role = Role.Middle
    };

    var handler = CreateCommandHandler();
    
    var exception = Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
    Assert.That(exception!.Message, Is.EqualTo("Player (random-id) was not found"));
  }
  
  [Test]
  public void WhenTeamDoesNotExist_ShouldFail() {
    var command = new AddPlayerToTeamCommand {
      PlayerId = _player.Id,
      TeamId = "random-id",
      Role = Role.Middle
    };

    var handler = CreateCommandHandler();
    
    var exception = Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
    Assert.That(exception!.Message, Is.EqualTo("Team (random-id) was not found"));
  }
}