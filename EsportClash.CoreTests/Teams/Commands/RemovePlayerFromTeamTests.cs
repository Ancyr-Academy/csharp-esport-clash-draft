using EsportClash.Core.Players.Model;
using EsportClash.Core.Shared;
using EsportClash.Core.Teams.Commands.AddPlayerToTeam;
using EsportClash.Core.Teams.Commands.RemovePlayerFromTeam;
using EsportClash.Core.Teams.Model;
using EsportClash.Persistence.InMemory.Players;
using EsportClash.Persistence.InMemory.Teams;

namespace EsportClash.CoreTests.Teams.Commands;

public class RemovePlayerFromTeamTests {
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

  private RemovePlayerFromTeamCommandHandler CreateCommandHandler() {
    return new RemovePlayerFromTeamCommandHandler(
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
    
    _team.Join(_player, Role.Middle);
    
    await _teamRepository.CreateAsync(_team);
    await _playerRepository.CreateAsync(_player);
  }
  
  [Test]
  public async Task ShouldRemovePlayerFromTeam() {
    var command = new RemovePlayerFromTeamCommand() {
      PlayerId = _player.Id,
      TeamId = _team.Id,
    };

    var handler = CreateCommandHandler();
    await handler.Handle(command, new CancellationToken());
    
    Assert.That(_team.HasPlayer(_player), Is.False);
  }  
  
  [Test]
  public void WhenPlayerDoesNotExist_ShouldFail() {
    var command = new RemovePlayerFromTeamCommand {
      PlayerId = "random-id",
      TeamId = _team.Id,
    };

    var handler = CreateCommandHandler();
    
    var exception = Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, new CancellationToken()));
    Assert.That(exception!.Message, Is.EqualTo("Player (random-id) was not found"));
  }
  
  [Test]
  public void WhenTeamDoesNotExist_ShouldFail() {
    var command = new RemovePlayerFromTeamCommand {
      PlayerId = _player.Id,
      TeamId = "random-id",
    };

    var handler = CreateCommandHandler();
    
    var exception = Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, new CancellationToken()));
    Assert.That(exception!.Message, Is.EqualTo("Team (random-id) was not found"));
  }
}