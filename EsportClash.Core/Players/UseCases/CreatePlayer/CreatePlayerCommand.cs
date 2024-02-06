using EsportClash.Core.Players.Model;

namespace EsportClash.Core.Players.UseCases.CreatePlayer;

public class CreatePlayerCommand {
  public string Name { get; set; } = string.Empty;
  public Role MainRole { get; set; }
}