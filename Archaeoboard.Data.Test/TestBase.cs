using Archaeoboard.Data.Database.SQLite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archaeoboard.Data.Test
{
    public class TestBase
    {
        private const string DB_LOCATION = @"Z:\prog-20140418.db";
        public IServiceProvider ServiceProvider { get; init; }

        public TestBase()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(l => l.AddDebug().AddConsole());
            serviceCollection.AddDbContext<BoardSQLiteContext>(s =>
            {
                s.UseSqlite($"Data Source={DB_LOCATION}");
                s.EnableSensitiveDataLogging().EnableDetailedErrors();
            }, ServiceLifetime.Transient, ServiceLifetime.Transient);

            ServiceProvider = serviceCollection.BuildServiceProvider();
            using var context = ServiceProvider.GetService<BoardSQLiteContext>();
        }
    }
}
