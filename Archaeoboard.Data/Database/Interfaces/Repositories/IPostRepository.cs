using Archaeoboard.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archaeoboard.Data.Database.Interfaces.Repositories
{
    public interface IPostRepository
    {
        Task<IEnumerable<PostDAO>> GetPosts(Models.ThreadDAO thread);
        Task<Models.PostDAO> GetPost(Models.ThreadDAO thread, long postNum);
        Task<IEnumerable<Models.PostDAO>> FindPosts(string match);
        Task<bool> SetupIndex();
    }
}
