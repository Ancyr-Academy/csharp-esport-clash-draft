using EsportClash.Core.Players.Model;
using EsportClash.Core.Teams.Model;

namespace EsportClash.CoreTests.Teams.Model;

public class TeamTests {
  private Player CreatePlayer(string name, Role role) {
    return new Player {
      Id = name,
      Name = name,
      MainRole = role
    };
  }

  private Team CreateTeam() {
    return new Team {
      Id = "team",
      Name = "team"
    };
  }
  [Test]
  public void PlayerShouldJoinTeam() {
    var player = CreatePlayer("player", Role.Middle);
    var team = CreateTeam();

    team.Join(player, Role.Middle);
    
    Assert.That(team.HasPlayer(player), Is.True);
    Assert.That(team.IsComplete(), Is.False);
  }
  
  [Test]
  public void WhenRoleIsAlreadyTaken_ShouldNotAddToTheTeam() {
    var mid1 = CreatePlayer("Mid1", Role.Middle);
    var mid2  = CreatePlayer("Mid2", Role.Middle);
    var team = CreateTeam();

    team.Join(mid1, Role.Middle);
    var exception = Assert.Throws<InvalidOperationException>(() => team.Join(mid2, Role.Middle));
    Assert.That(exception!.Message, Is.EqualTo("Role is already taken"));
  }
 
  [Test]
  public void WhenPlayerAlreadyHasRole_ShouldBeIdempotent() {
    var mid1 = CreatePlayer("Mid1", Role.Middle);
    var team = CreateTeam();

    team.Join(mid1, Role.Middle);
    team.Join(mid1, Role.Middle);
    
    Assert.That(team.HasPlayer(mid1), Is.True);
    Assert.That(team.MembersCount(), Is.EqualTo(1));
  }

  [Test]
  public void WhenPlayerIsAlreadyInTeam_houldNotAddToTheTeam() {
    var mid = CreatePlayer("Mid", Role.Middle);
    var team = CreateTeam();

    team.Join(mid, Role.Middle);
    var exception = Assert.Throws<InvalidOperationException>(() => team.Join(mid, Role.Top));
    Assert.That(exception!.Message, Is.EqualTo("Player is already in team"));
  }

  [Test]
  public void WhenAddingFivePlayers_ShouldBeComplete() {
    var top = CreatePlayer("Top", Role.Top);
    var jungle = CreatePlayer("Jungle", Role.Jungle);
    var mid = CreatePlayer("Mid", Role.Middle);
    var bottom = CreatePlayer("Bottom", Role.Bottom);
    var support = CreatePlayer("Support", Role.Support);
    var team = CreateTeam();

      
    team.Join(top, Role.Top);
    team.Join(jungle, Role.Jungle);
    team.Join(mid, Role.Middle);
    team.Join(bottom, Role.Bottom);
    team.Join(support, Role.Support);
    
    Assert.That(team.IsComplete(), Is.True);
  }
  
  [Test]
  public void PlayerShouldLeaveTeam() {
    var player = CreatePlayer("player", Role.Middle);
    var team = CreateTeam();

    team.Join(player, Role.Middle);
    team.Leave(player);
    
    Assert.That(team.HasPlayer(player), Is.False);
  }
}