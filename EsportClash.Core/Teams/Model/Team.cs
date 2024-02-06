using EsportClash.Core.Players.Model;
using EsportClash.Core.Shared;

namespace EsportClash.Core.Teams.Model;

public class Team : BaseEntity {
  public string Name { get; set; } = string.Empty;

  public ICollection<TeamMember> Members = new List<TeamMember>();


  /**
   * Allows a Player to join the team.
   * If the role is already taken, the player can't join the team with this role.
   * Also, if the player is already in the team, he can't join it again.
   */
  public void Join(Player player, Role role) {
    var memberWithRole = Members.FirstOrDefault(m => m.Role.Equals(role));
    if (memberWithRole != null) {
      if (memberWithRole.PlayerId.Equals(player.Id)) {
        return; // Already added
      }
      
      throw new InvalidOperationException("Role is already taken");
    }
    
    var memberWithId = Members.FirstOrDefault(m => m.PlayerId.Equals(player.Id));
    if (memberWithId != null) {
      throw new InvalidOperationException("Player is already in team");
    }
    
    // Note : the invariant of having a team of 5 people is respected by default
    // because there's only 5 roles.
    
    Members.Add(new TeamMember {
      // Note : Team generates its own ID because it's part of the internal representation
      // of the Team object.
      Id = Guid.NewGuid().ToString(),
      PlayerId = player.Id,
      TeamId = this.Id,
      Role = role,
    });
  }
  
  /**
   * Removes a player from the team.
   */
  public void Leave(Player player) {
    var member = Members.First(m => m.PlayerId.Equals(player.Id));
    if (member != null) {
      Members.Remove(member);
    }
  }

  public bool IsComplete() {
    return Members.Count == 5;
  }
  
  public bool HasPlayer(Player player) {
    return Members.Any(m => m.PlayerId.Equals(player.Id));
  }

  public int MembersCount() {
    return Members.Count;
  }
}