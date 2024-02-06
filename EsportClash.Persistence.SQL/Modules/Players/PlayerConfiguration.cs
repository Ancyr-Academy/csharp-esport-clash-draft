using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EsportClash.Core.Players.Model;

namespace EsportClash.Persistence.SQL.Modules.Players;

public class PlayerConfiguration : IEntityTypeConfiguration<Player> {
  public void Configure(EntityTypeBuilder<Player> builder) {
    builder.Property(q => q.Name).IsRequired();
    builder.Property(q => q.MainRole).IsRequired();
  
  }
}