namespace EsportClash.Core.Shared.Id;

public class FixedIdProvider : IIdProvider {
  public static string Id = "fixed-id";

  public string NewId() {
    return Id;
  }
}