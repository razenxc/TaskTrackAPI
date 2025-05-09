using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTrack.Infrastructure.Entities;

namespace TaskTrack.Infrastructure.Configurations;

public class JwtRefreshTokenConfiguration : IEntityTypeConfiguration<JwtRefreshTokenEntity>
{
    public void Configure(EntityTypeBuilder<JwtRefreshTokenEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .IsRequired();
        
        builder.Property(x => x.RefreshToken)
            .IsRequired();

        builder.Property(x => x.Expires)
            .IsRequired();
    }
}
