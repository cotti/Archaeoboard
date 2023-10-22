using Archaeoboard.Data.Database.SQLite;
using Archaeoboard.Data.Database.SQLite.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archaeoboard.Data.Test
{
    public class PostSQLiteRepositoryTests : IClassFixture<TestBase>
    {
        private IServiceProvider _serviceProvider;

        public PostSQLiteRepositoryTests(TestBase testBase)
        {
            _serviceProvider = testBase.ServiceProvider;
        }

        [Fact]
        public async Task GetPost_Successful()
        {
            using var context = _serviceProvider.GetService<BoardSQLiteContext>();
            var repo = new PostSQLiteRepository(context!);

            var result = await repo.GetPost(new Models.Thread() { ThreadID = 1376617142 }, 9);

            Assert.NotNull(result);
            Assert.Equal("It's dark. You are likely to be eaten by a grue.", result.PostContent);

        }
    }
}
