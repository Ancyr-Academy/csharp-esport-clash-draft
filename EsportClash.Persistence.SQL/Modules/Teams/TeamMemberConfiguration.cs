using EsportClash.Core.Teams.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EsportClash.Persistence.SQL.Modules.Teams;

public class TeamMemberConfiguration : IEntityTypeConfiguration<TeamMember> {
  public void Configure(EntityTypeBuilder<TeamMember> builder) {
    builder.Property(q => q.PlayerId).IsRequired();
    builder.Property(q => q.TeamId).IsRequired();
    
    builder.HasOne(q => q.Player)
      .WithMany()
      .HasForeignKey(q => q.PlayerId)
      .IsRequired();
    
    builder.HasOne(q => q.Team)
      .WithMany(q => q.Members)
      .HasForeignKey(q => q.TeamId)
      .IsRequired();
  }
}