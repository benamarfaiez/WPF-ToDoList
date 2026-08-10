using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp.Tests.Helpers
{
    public static class WpfTestHelpers
    {
        public static void RunOnStaThread(Action action, int timeoutMs = 10000)
        {
            Exception ex = null;
            var thread = new Thread(() =>
            {
                try
                {
                    // Ensure a WPF Application exists and required resources are loaded for StaticResource lookups
                    if (System.Windows.Application.Current == null)
                    {
                        var app = new System.Windows.Application();
                        try
                        {
                            var dict = new System.Windows.ResourceDictionary { Source = new System.Uri("pack://application:,,,/WpfApp;component/Styles/Composants.xaml", System.UriKind.Absolute) };
                            app.Resources.MergedDictionaries.Add(dict);
                        }
                        catch
                        {
                            // If resource cannot be loaded, continue and let the test fail with clear error
                        }
                    }

                    action();
                }
                catch (Exception e)
                {
                    ex = e;
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            if (!thread.Join(timeoutMs))
            {
                throw new TimeoutException("STA thread timed out.");
            }
            if (ex != null) throw ex;
        }

        public static T RunOnStaThread<T>(Func<T> func, int timeoutMs = 10000)
        {
            T result = default;
            Exception ex = null;
            var thread = new Thread(() =>
            {
                try
                {
                    result = func();
                }
                catch (Exception e)
                {
                    ex = e;
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            if (!thread.Join(timeoutMs))
            {
                throw new TimeoutException("STA thread timed out.");
            }
            if (ex != null) throw ex;
            return result;
        }

        public static Task RunOnStaThreadAsync(Func<Task> func, int timeoutMs = 10000)
        {
            return Task.Run(() => RunOnStaThread(() => func().GetAwaiter().GetResult(), timeoutMs));
        }
    }
}
