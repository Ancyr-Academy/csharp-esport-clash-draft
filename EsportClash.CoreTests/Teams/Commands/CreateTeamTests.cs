using EsportClash.Core.Shared.Id;
using EsportClash.Core.Teams.Commands.CreateTeam;
using EsportClash.Persistence.InMemory.Teams;

namespace EsportClash.CoreTests.Teams.Commands;

public class CreateTeamTests {
  private readonly FixedIdProvider _idProvider = new();
  private readonly InMemoryTeamRepository _teamRepository = new();
  
  public CreateTeamCommandHandler CreateCommandHandler() {
    return new CreateTeamCommandHandler(_idProvider, _teamRepository);
  }

  [Test]
  public async Task HappyPath_ShouldReturnId() {
    var command = new CreateTeamCommand {
      Name = "Team"
    };

    var handler = CreateCommandHandler();
    var result = await handler.Handle(command, CancellationToken.None);
    
    Assert.That(result.Id, Is.EqualTo(_idProvider.NewId()));
  } 
  
  [Test]
  public async Task HappyPath_ShouldReturnCreateTeam() {
    var command = new CreateTeamCommand {
      Name = "Team"
    };

    var handler = CreateCommandHandler();
    var result = await handler.Handle(command, CancellationToken.None);

    var createdTeam = await _teamRepository.FindByIdAsync(result.Id);
    
    Assert.That(createdTeam, Is.Not.Null);
    Assert.That(createdTeam.Id, Is.EqualTo(result.Id));
    Assert.That(createdTeam.Name, Is.EqualTo(command.Name));
  } 
}