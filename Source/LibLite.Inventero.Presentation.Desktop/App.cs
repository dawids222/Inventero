using LibLite.Inventero.DAL;
using LibLite.Inventero.Presentation.Desktop.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace LibLite.Inventero.Presentation.Desktop
{
    public sealed partial class App : Application
    {
        public App()
        {
            Services = RegisterServices();
            ConfigureServices(Services);

            InitializeComponent();
        }

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }

        /// <summary>
        /// Registers the services for the application.
        /// </summary>
        private static IServiceProvider RegisterServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<InventeroDbContext>(opt =>
            {
                opt.UseSqlite("Data Source=inventero.db");
            });

            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<Test1ViewModel>();
            services.AddSingleton<Test2ViewModel>();

            return services.BuildServiceProvider();
        }

        private static void ConfigureServices(IServiceProvider services)
        {
            var context = services.GetRequiredService<InventeroDbContext>();
            context.Database.Migrate();
        }
    }
}
