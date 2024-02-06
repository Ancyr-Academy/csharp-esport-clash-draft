using EsportClash.Core.Shared;
using EsportClash.Core.Shared.Id;
using EsportClash.Core.Teams.Commands.CreateTeam;
using EsportClash.Core.Teams.Commands.DeleteTeam;
using EsportClash.Core.Teams.Model;
using EsportClash.Persistence.InMemory.Teams;

namespace EsportClash.CoreTests.Teams.Commands;

public class DeleteTeamTests {
  private readonly Team _team = new() {
    Id = "team",
    Name = "team"
  };
    
  private readonly InMemoryTeamRepository _teamRepository = new();
  
  public DeleteTeamCommandHandler createCommandHandler() {
    return new DeleteTeamCommandHandler(_teamRepository);
  }


  [SetUp]
  public async Task SetUp() {
    _teamRepository.Clear();
    await _teamRepository.CreateAsync(_team);
  }
  
  [Test]
  public async Task HappyPath_ShouldDeleteTeam() {
    var command = new DeleteTeamCommand {
      Id = _team.Id
    };

    var handler = createCommandHandler();
    await handler.Handle(command, new CancellationToken());
    
    var team = await _teamRepository.FindByIdAsync(_team.Id);
    Assert.That(team, Is.Null);
  }   
  
  [Test]
  public async Task WhenTeamDoesNotExist_ShouldFail() {
    var command = new DeleteTeamCommand {
      Id = "random-id"
    };

    var handler = createCommandHandler();
    var exception = Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, new CancellationToken()));
    Assert.That(exception!.Message, Is.EqualTo("Team (random-id) was not found"));
  } 
}