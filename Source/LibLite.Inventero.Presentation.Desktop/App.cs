using LibLite.Inventero.Adapter.Tools;
using LibLite.Inventero.Core.Contracts.Stores;
using LibLite.Inventero.Core.Contracts.Tools;
using LibLite.Inventero.DAL;
using LibLite.Inventero.DAL.Stores;
using LibLite.Inventero.Presentation.Desktop.Interfaces;
using LibLite.Inventero.Presentation.Desktop.Services;
using LibLite.Inventero.Presentation.Desktop.ViewModel;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace LibLite.Inventero.Presentation.Desktop
{
    public sealed partial class App : Application
    {
        public IServiceProvider Services { get; }
        public new static App Current => (App)Application.Current;

        public App()
        {
            Services = RegisterServices();
            ConfigureServices(Services);
            SetDefaultDateTimeFormat();

            InitializeComponent();
        }

        private static IServiceProvider RegisterServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<InventeroDbContext>(opt =>
            {
                opt.UseSqlite("Data Source=inventero.db");
            });

            services.AddSingleton<IPurchaseStore, PurchaseStore>();
            services.AddSingleton<IProductStore, ProductStore>();
            services.AddSingleton<IGroupStore, GroupStore>();

            services.AddSingleton<IMapper, Adapter.Tools.MapsterMapper>();
            services.AddSingleton<IEventBus, BusLiteEventBus>();

            services.AddSingleton<IDialogCoordinator, DialogCoordinator>();
            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<IViewService, ViewService>();
            services.AddSingleton<IViewModelService>(provider => new ViewModelService(provider));

            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<MainMenuViewModel>();

            services.AddSingleton<PurchasesViewModel>();
            services.AddSingleton<ProductsViewModel>();
            services.AddSingleton<GroupsViewModel>();

            services.AddTransient<PurchaseViewModel>();
            services.AddTransient<ProductViewModel>();
            services.AddTransient<GroupViewModel>();

            return services.BuildServiceProvider();
        }

        private static void SetDefaultDateTimeFormat()
        {
            var newCulture = new CultureInfo(CultureInfo.CurrentCulture.Name);

            newCulture.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy";

            CultureInfo.DefaultThreadCurrentCulture = newCulture;
            CultureInfo.DefaultThreadCurrentUICulture = newCulture;

            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;

            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    System.Windows.Markup.XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        }

        private static void ConfigureServices(IServiceProvider services)
        {
            var context = services.GetRequiredService<InventeroDbContext>();
            context.Database.Migrate();
        }
    }
}
