namespace EsportClash.Core.Shared.Id;

public class RandomIdProvider : IIdProvider {
  public string NewId() {
    return Guid.NewGuid().ToString();
  }
}