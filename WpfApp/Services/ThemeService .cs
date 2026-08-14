using System.Linq;
using System.Windows;

namespace WpfApp.Services
{
    public class ThemeService : IThemeService
    {
        public void AppliquerTheme(bool modeSombre)
        {
            var dictionnaires = Application.Current.Resources.MergedDictionaries;

            // Supprime toute ResourceDictionary correspondant aux thèmes existants (Light/Dark)
            var themesExistants = dictionnaires
                .Where(d => d.Source != null && (
                    d.Source.OriginalString.Contains("Styles/Light.xaml") ||
                    d.Source.OriginalString.Contains("Styles/Dark.xaml")
                ))
                .ToList();

            foreach (var t in themesExistants)
                dictionnaires.Remove(t);

            var uri = new System.Uri(modeSombre ? "Styles/Dark.xaml" : "Styles/Light.xaml",
                                      System.UriKind.Relative);
            // Ajoute le thème choisi
            dictionnaires.Add(new ResourceDictionary { Source = uri });

        }
    }
}
