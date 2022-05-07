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

            DependencyService.Register<UserRoleRepository>();
            DependencyService.Register<RegistrationRepository>();
            DependencyService.Register<HashGenerator>();
            DependencyService.Register<LoginRepository>();
            DependencyService.Register<UserRepository>();
            DependencyService.Register<PartRepository>();
            DependencyService.Register<ContactsRepository>();
            DependencyService.Register<FeedbackRepository>();

            DependencyService.Get<INavigationService<BaseViewModel>>()
                   .Navigate<LoginViewModel>();
            if (!string.IsNullOrWhiteSpace(
                Settings.Default.SerializedIdentity))
            {
                DependencyService
                    .Get<INavigationService<BaseViewModel>>()
                    .Navigate<AccountViewModel>();
            }
        }
    }
}
