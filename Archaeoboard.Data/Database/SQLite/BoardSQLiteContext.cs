using Archaeoboard.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archaeoboard.Data.Database.SQLite;

public class BoardSQLiteContext : DbContext
{
    public DbSet<Models.ThreadDAO> Threads { get; set; }
    public DbSet<PostDAO> Posts { get; set; }
    public BoardSQLiteContext(DbContextOptions<BoardSQLiteContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(BoardSQLiteContext).Assembly);
    }
}