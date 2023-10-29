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
        Task<IEnumerable<Models.ThreadDAO>> GetThreadsPaged(int amount, int page);
        Task<IEnumerable<Models.ThreadDAO>> GetThreadMatches(string match);

        Task<Models.ThreadDAO> GetThread(long threadId);
    }
}
