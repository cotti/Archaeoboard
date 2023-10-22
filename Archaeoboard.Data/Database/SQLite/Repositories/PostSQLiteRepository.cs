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

    public async Task<IEnumerable<Post>> FindPosts(string match)
    {
        return await _context.Set<Post>().Where(x => x.PostContent.Contains(match, StringComparison.InvariantCultureIgnoreCase)).Take(10).ToListAsync();
    }

    public async Task<Post> GetPost(Models.Thread thread, long postNum)
    {
        return await _context.Set<Post>().Where(x => x.ThreadID == thread.ThreadID && x.PostNumber == postNum).SingleAsync();
    }

    public Task<IEnumerable<Post>> GetPosts(Models.Thread thread)
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
