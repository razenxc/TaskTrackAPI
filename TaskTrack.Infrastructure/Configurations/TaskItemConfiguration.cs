using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItemEntity>
{
    public void Configure(EntityTypeBuilder<TaskItemEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
        .IsRequired();

        builder.Property(x => x.Description)
        .IsRequired();

        builder.Property(x => x.Status)
        .IsRequired();

        builder.Property(x => x.CreatedAt)
        .IsRequired();
    }
}