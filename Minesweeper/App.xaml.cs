using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Minesweeper.Models.DbModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Minesweeper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IConfiguration Config { get; private set; }

        public App()
        {
            Config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            using (var dbContext = new GameDbContext())
            {
                if (dbContext.Database.GetPendingMigrations().Any())
                {
                    dbContext.Database.Migrate();
                }
            }
        }
    }

}
