namespace WpfApp.Services
{
    public interface INavigationService
    {
        object CurrentViewModel { get; }
        void NavigateTo<TViewModel>() where TViewModel : class;
    }
}
