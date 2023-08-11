using Backend.Contexts;
using Frontend.Pages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using System;
using System.Configuration;
using System.IO;
using ViewModel;

namespace Frontend
{

    public partial class App : Application
    {
        public static IHost AppHost { get; private set; }


        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    string connectionstring = hostContext.Configuration.GetConnectionString("LocalDB");
                    DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(connectionstring).Options;

                    services.AddSingleton<MainWindow>();
                    services.AddDbContext<LocalDB>(s => new LocalDB(options));
                })
                .Build();
            this.InitializeComponent();
        }

        public Window m_window;
        protected override async void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            VerifyFolderIntegrity();
            using (var scope = AppHost.Services.CreateScope())
            {
                using (var db = AppHost.Services.GetRequiredService<LocalDB>())
                {
                    db.Database.Migrate();
                }

            }
            await AppHost.StartAsync();
            m_window = AppHost.Services.GetRequiredService<MainWindow>();
            m_window.Activate();
        }

        private void VerifyFolderIntegrity()
        {
            if (!Directory.Exists(AppDataPath.BasePath))
                Directory.CreateDirectory(AppDataPath.BasePath);
            if (!Directory.Exists(AppDataPath.PDFPath))
                Directory.CreateDirectory(AppDataPath.PDFPath);
            if (!Directory.Exists(AppDataPath.ImgPath))
                Directory.CreateDirectory(AppDataPath.ImgPath);
        }

        
    }

}
