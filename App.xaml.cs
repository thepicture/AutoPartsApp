using AutoPartsApp.Properties;
using AutoPartsApp.Services;
using AutoPartsApp.ViewModels;
using System.Windows;

namespace AutoPartsApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DependencyService.Register<NavigationService>();
            DependencyService.Register<MessageBoxFeedbackService>();
            DependencyService.Register<Identity>();

            DependencyService.Get<INavigationService<BaseViewModel>>()
                   .Navigate<LoginViewModel>();
            if (!string.IsNullOrWhiteSpace(
                Settings.Default.SerializedIdentity))
            {
                // TODO: Navigate to the auto parts page
            }
        }
    }
}
