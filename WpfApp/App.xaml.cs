using System;
using System.Windows;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WpfApp.Services;
using WpfApp.ViewModels;
using WpfApp.Views;

namespace WpfApp
{
    public partial class App : Application
    {
        public static IHost AppHost { get; private set; } = null!;

        public static IServiceProvider Services => AppHost.Services;

        public App()
        {
            // Construction du Generic Host .NET
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    // Les services existants
                    services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);
                    services.AddSingleton<INavigationService, NavigationService>();
                    services.AddSingleton<ITodoService, TodoService>();
                    services.AddSingleton<IThemeService, ThemeService>();

                    // Les ViewModels existants
                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton<ContactViewModel>();
                    services.AddSingleton<TacheViewModel>();
                    services.AddSingleton<SettingsViewModel>();

                    // Fenêtre principale
                    services.AddSingleton<MainWindow>();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Démarrage asynchrone du Host (chargement des configurations, logs, etc.)
            await AppHost.StartAsync();

            // Affichage de la fenêtre principale depuis le conteneur IoC
            var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            // Arrêt propre des services hébergés à la fermeture de l'application
            using (AppHost)
            {
                await AppHost.StopAsync();
            }

            base.OnExit(e);
        }
    }
}
