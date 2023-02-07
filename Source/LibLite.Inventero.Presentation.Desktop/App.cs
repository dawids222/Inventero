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
using System.Threading.Tasks;
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
            SetUnhandledExceptionHandling();

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

            services.AddSingleton(DialogCoordinator.Instance);

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

        private void SetUnhandledExceptionHandling()
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                HandleUnhandledException((Exception)e.ExceptionObject, "AppDomain.CurrentDomain.UnhandledException");

            DispatcherUnhandledException += (s, e) =>
            {
                HandleUnhandledException(e.Exception, "Application.Current.DispatcherUnhandledException");
                e.Handled = true;
            };

            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                HandleUnhandledException(e.Exception, "TaskScheduler.UnobservedTaskException");
                e.SetObserved();
            };
        }

        private void HandleUnhandledException(Exception exception, string source)
        {
            var dialogService = Services.GetService<IDialogService>();
            var message = $"Source: {source}{Environment.NewLine}Message: {exception.Message}";
            dialogService.ShowErrorAsync(message);
            // TODO: Add logging
        }
    }
}
