using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Archaeoboard.Data.Database.SQLite;
using Microsoft.EntityFrameworkCore;

namespace Archaeoboard.UI.DependencyInjection
{
    public static class Bootstrapper
    {
        public static IHost? Container { get; private set; }

        public static IHost Register(string[] args)
        {
            Container = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddLogging(l => l.AddDebug().AddConsole());
                    services.AddDbContext<BoardSQLiteContext>(s =>
                    {
                        s.UseSqlite($"Data Source=");
                        s.EnableSensitiveDataLogging().EnableDetailedErrors();
                    }, ServiceLifetime.Transient, ServiceLifetime.Transient);
                })
                .Build();

            Container.Start();

            return Container;
        }
    }
}
