namespace EsportClash.Core.Teams.ViewModel;

public class TeamViewModel {
  public class Member {
    public string PlayerId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;

    protected bool Equals(Member other) {
      return PlayerId == other.PlayerId && Name == other.Name && Role == other.Role;
    }

    public override bool Equals(object? obj) {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((Member)obj);
    }

    public override int GetHashCode() {
      return HashCode.Combine(PlayerId, Name, Role);
    }
  }

  public string Id { get; set; } = string.Empty;
  public string Name { get; set; } = string.Empty;
  public IList<Member> Members { get; set; } = new List<Member>();
}