using EsportClash.Core.Players.Model;
using EsportClash.Core.Shared;

namespace EsportClash.Core.Teams.Model;

public class Team : BaseEntity {
  public string Name { get; set; }

  public ICollection<TeamMember> Members = new List<TeamMember>();

  /**
   * Allows a Player to join the team.
   * If the role is already taken, the player can't join the team with this role.
   * Also, if the player is already in the team, he can't join it again.
   */
  public void Join(Players.Model.Player player, Role role) {
    var memberWithRole = Members.FirstOrDefault(m => m.Role.Equals(role));
    if (memberWithRole != null) {
      if (memberWithRole.PlayerId.Equals(player.Id)) {
        return; // Already added
      }
      
      throw new InvalidOperationException("Role already taken");
    }
    
    var memberWithId = Members.FirstOrDefault(m => m.PlayerId.Equals(player.Id));
    if (memberWithId != null) {
      if (memberWithId.Role.Equals(role)) {
        return;
      }
      
      throw new InvalidOperationException("Player already in team");
    }
    
    Members.Add(new TeamMember {
      Id = Guid.NewGuid().ToString(),
      PlayerId = player.Id,
      TeamId = this.Id
    });
  }
  
  /**
   * Removes a player from the team.
   */
  public void Leave(Players.Model.Player player) {
    var member = Members.First(m => m.PlayerId.Equals(player.Id));
    if (member == null) {
      return; // Not in team
    }
    
    Members.Remove(member);
  }

  public bool IsComplete() {
    return Members.Count == 5;
  }
  
  public bool HasPlayer(Players.Model.Player player) {
    return Members.Any(m => m.PlayerId.Equals(player.Id));
  }
}