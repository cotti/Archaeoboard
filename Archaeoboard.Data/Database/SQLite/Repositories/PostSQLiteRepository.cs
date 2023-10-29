using Archaeoboard.Data.Database.Interfaces.Repositories;
using Archaeoboard.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Archaeoboard.Data.Database.SQLite.Repositories;
public class PostSQLiteRepository : IPostRepository
{
    private readonly BoardSQLiteContext _context;

    public PostSQLiteRepository(BoardSQLiteContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PostDAO>> FindPosts(string match)
    {
        return await _context.Set<PostDAO>().Where(x => x.PostContent.Contains(match, StringComparison.InvariantCultureIgnoreCase)).Take(10).ToListAsync();
    }

    public async Task<PostDAO> GetPost(Models.ThreadDAO thread, long postNum)
    {
        return await _context.Set<PostDAO>().Where(x => x.ThreadID == thread.ThreadID && x.PostNumber == postNum).SingleAsync();
    }

    public Task<IEnumerable<PostDAO>> GetPosts(Models.ThreadDAO thread)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SetupIndex()
    {
        var indexExists = _context.Database.ExecuteSqlRaw("SELECT COUNT(*) FROM sqlite_master WHERE type='index' AND name='idx_posts_post'");
        if (indexExists == 0)
        {
            _context.Database.ExecuteSqlRaw("CREATE INDEX idx_posts_post ON posts (post)");
        }
        await _context.SaveChangesAsync();

        return await Task.FromResult(true);
    }
}
