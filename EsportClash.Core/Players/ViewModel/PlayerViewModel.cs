namespace EsportClash.Core.Players.ViewModel;

public class PlayerViewModel {
  public string Id { get; set; }
  public string Name { get; set; }
  public string Role { get; set; }

  public PlayerViewModel() { }

  public PlayerViewModel(Model.Player player) {
    Id = player.Id;
    Name = player.Name;
    Role = player.MainRole.ToString();
  }
}