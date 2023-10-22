using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archaeoboard.Data.Database.SQLite.Repositories
{
    public class ThreadSQLiteRepository : IThreadRepository
    {
        private readonly BoardSQLiteContext _context;

        public ThreadSQLiteRepository(BoardSQLiteContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Models.Thread>> GetAllThreads()
        {
            return await _context.Set<Models.Thread>().ToListAsync();
        }

        public async Task<Models.Thread> GetThread(long threadId)
        {
            return await _context.Set<Models.Thread>().Where(x => x.ThreadID == threadId).Include(a => a.Posts).SingleAsync();
        }
    }
}
