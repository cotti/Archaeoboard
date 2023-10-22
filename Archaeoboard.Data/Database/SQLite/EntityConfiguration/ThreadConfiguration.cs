using Archaeoboard.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archaeoboard.Data.Database.SQLite.EntityConfiguration;

public class ThreadConfiguration : IEntityTypeConfiguration<Models.Thread>
{
    public void Configure(EntityTypeBuilder<Models.Thread> builder)
    {
        builder.ToTable("threads");
        builder.HasKey(f => f.ThreadID).HasName("threadid");
        builder.Property(f => f.ThreadID).HasColumnName("threadid");
        builder.Property(f => f.Title).HasColumnName("title");
        builder.Property(f => f.Board).HasColumnName("board");
        builder.Property(f => f.FirstPost).HasColumnName("firstposttime");
        builder.Property(f => f.LastPost).HasColumnName("lastposttime");
        builder.Property(f => f.LastBump).HasColumnName("lastbumptime");
        builder.Property(f => f.PostAmount).HasColumnName("numposts");
        builder.HasMany(f => f.Posts).WithOne(p => p.Thread).HasForeignKey(p => p.ThreadID);
    }
}
