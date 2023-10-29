using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archaeoboard.Data.Database
{
    public interface IThreadRepository
    {
        Task<IEnumerable<Models.ThreadDAO>> GetAllThreads();
        Task<Models.ThreadDAO> GetThread(long threadId);
    }
}
