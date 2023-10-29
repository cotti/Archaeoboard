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
        public async Task<IEnumerable<Models.ThreadDAO>> GetAllThreads()
        {
            return await _context.Set<Models.ThreadDAO>().ToListAsync();
        }

        public async Task<Models.ThreadDAO> GetThread(long threadId)
        {
            return await _context.Set<Models.ThreadDAO>().Where(x => x.ThreadID == threadId).Include(a => a.Posts).SingleAsync();
        }
    }
}
