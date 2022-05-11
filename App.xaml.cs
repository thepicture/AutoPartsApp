using AutoPartsApp.Models.Entities;
using AutoPartsApp.Properties;
using AutoPartsApp.Services;
using AutoPartsApp.ViewModels;
using System;
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

            DependencyService.Register<MessageBoxFeedbackService>();
            try
            {
                using (AutoPartsBaseEntities entities = new AutoPartsBaseEntities())
                {
                    entities.Database.Connection.Open();
                }
            }
            catch (Exception)
            {
                DependencyService
                    .Get<IFeedbackService>()
                    .InformErrorAsync("Проверьте подключение к базе данных")
                    .Wait();
                return;
            }

            DependencyService.Register<NavigationService>();

            DependencyService.Register<Identity>();

            DependencyService.Register<UserRoleRepository>();
            DependencyService.Register<RegistrationRepository>();
            DependencyService.Register<HashGenerator>();
            DependencyService.Register<LoginRepository>();
            DependencyService.Register<UserRepository>();
            DependencyService.Register<PartRepository>();
            DependencyService.Register<ContactsRepository>();
            DependencyService.Register<FeedbackRepository>();
            DependencyService.Register<CategoriesRepository>();
            DependencyService.Register<SubCategoriesRepository>();

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
