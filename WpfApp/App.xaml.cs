using System;
using System.Windows;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using WpfApp.Services;
using WpfApp.ViewModels;
using WpfApp.Views;

namespace WpfApp
{
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            // Le Messenger est LE canal de communication entre ViewModels indépendants.
            // Singleton : c'est le même "bus" pour toute l'application.
            services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);

            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<ITodoService, TodoService>();
            services.AddSingleton<IThemeService, ThemeService>();

            // Singleton : ContactViewModel et TacheViewModel gardent leur état
            // (sélection, liste de tâches...) même après avoir changé d'écran.
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<ContactViewModel>();
            services.AddSingleton<TacheViewModel>();
            services.AddSingleton<SettingsViewModel>();

            services.AddSingleton<MainWindow>();

            Services = services.BuildServiceProvider();
            Services.GetRequiredService<MainWindow>().Show();
        }
    }
}
