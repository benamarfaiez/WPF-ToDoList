using System;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace WpfApp.Services
{
    public partial class NavigationService : ObservableObject, INavigationService
    {
        private readonly IServiceProvider _serviceProvider;

        public NavigationService(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        [ObservableProperty]
        private object currentViewModel;

        public void NavigateTo<TViewModel>() where TViewModel : class
        {
            // Comme les ViewModels sont enregistrés en Singleton dans App.xaml.cs,
            // ContactViewModel et TacheViewModel gardent leur état d'une navigation à l'autre.
            CurrentViewModel = _serviceProvider.GetRequiredService<TViewModel>();
        }
    }
}
