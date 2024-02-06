using EsportClash.Core.Shared;
using EsportClash.Core.Teams.Model;

namespace EsportClash.Core.Players.Model;

public class Player : BaseEntity {
  public string Name { get; set; } = string.Empty;
  
  public Role MainRole { get; set; }
  
  public string? TeamId { get; set; }
  
  public Team Team { get; set; }
}