using Hoursly.Domain.Projects;
using Hoursly.Persistance.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hoursly.Persistance.Projects
{
    internal sealed class ProjectEntityConfiguration : EntityConfiguration<Project>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Project> builder)
        {
            builder.Property(c => c.Deadline).IsRequired(false);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(255);
        }
    }
}