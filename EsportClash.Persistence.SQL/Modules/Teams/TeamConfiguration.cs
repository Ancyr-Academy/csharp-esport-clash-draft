using EsportClash.Core.Teams.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EsportClash.Persistence.SQL.Modules.Teams;

public class TeamConfiguration : IEntityTypeConfiguration<Team> {
  public void Configure(EntityTypeBuilder<Team> builder) {
    builder.Property(q => q.Name).IsRequired();

    builder.HasMany(q => q.Members)
      .WithOne(q => q.Team)
      .HasForeignKey(q => q.TeamId)
      .IsRequired(false);
  }
}