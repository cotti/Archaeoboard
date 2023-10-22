using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archaeoboard.Data.Database.SQLite.EntityConfiguration;

public class PostConfiguration : IEntityTypeConfiguration<Models.Post>
{
    public void Configure(EntityTypeBuilder<Models.Post> builder)
    {
        builder.ToTable("posts");
        builder.HasKey(p => new { p.ThreadID, p.PostNumber });
        builder.Property(f => f.ThreadID).HasColumnName("threadid");
        builder.Property(f => f.PostNumber).HasColumnName("postnum");
        builder.Property(f => f.PosterName).HasColumnName("postername");
        builder.Property(f => f.PosterMail).HasColumnName("postermeiru");
        builder.Property(f => f.PosterTrip).HasColumnName("postertrip");
        builder.Property(f => f.PosterID).HasColumnName("posterid");
        builder.Property(f => f.PostDate).HasColumnName("posterdate");
        builder.Property(f => f.PostContent).HasColumnName("post").HasConversion(v => v.Trim(), v => v.Trim());
        builder.HasOne(p => p.Thread).WithMany(t => t.Posts).HasForeignKey(p => p.ThreadID);
    }
}