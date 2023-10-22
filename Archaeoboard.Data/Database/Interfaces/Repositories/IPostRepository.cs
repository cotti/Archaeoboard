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
        Task<IEnumerable<Post>> GetPosts(Models.Thread thread);
        Task<Models.Post> GetPost(Models.Thread thread, long postNum);
        Task<IEnumerable<Models.Post>> FindPosts(string match);
        Task<bool> SetupIndex();
    }
}
