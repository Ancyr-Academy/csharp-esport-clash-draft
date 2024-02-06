using EsportClash.Core.Players.Model;
using EsportClash.Core.Shared;

namespace EsportClash.Core.Teams.Model;

public class TeamMember : BaseEntity {
  public string PlayerId { get; set; }
  
  public Players.Model.Player Player { get; set; }
  
  public string TeamId { get; set; }

  public Team Team { get; set; }
  
  public Role Role { get; set; }

}