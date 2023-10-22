using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archaeoboard.Data.Database.Interfaces.Repositories
{
    public interface IThreadRepository
    {
        Task<long> GetThreadCount();
        Task<IEnumerable<Models.Thread>> GetThreadsPaged(int amount, int page);
        Task<IEnumerable<Models.Thread>> GetThreadMatches(string match);

        Task<Models.Thread> GetThread(long threadId);
    }
}
