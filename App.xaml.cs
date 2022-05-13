using AutoPartsApp.Models.Entities;
using AutoPartsApp.Properties;
using AutoPartsApp.Services;
using AutoPartsApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;

namespace AutoPartsApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static string currentConnectionString;

        public static string CurrentConnectionString
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(PermamentConnectionString))
                {
                    return PermamentConnectionString;
                }
                return currentConnectionString;
            }

            set => currentConnectionString = value;
        }
        public static string PermamentConnectionString
        {
            get => Settings.Default.WorkingConnectionString;
            set
            {
                Settings.Default.WorkingConnectionString = value;
                Settings.Default.Save();
            }
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            DependencyService.Register<MessageBoxFeedbackService>();

            Stack<string> dataSources = new Stack<string>();

            dataSources.Push($@"{Environment.MachineName}\SQLEXPRESS");
            dataSources.Push(Environment.MachineName);
            dataSources.Push(".");
            dataSources.Push(@".\SQLEXPRESS");

            while (true)
            {
                if (!string.IsNullOrWhiteSpace(PermamentConnectionString))
                {
                    break;
                }
                CurrentConnectionString = $"metadata=res://*/Models.Entities.BaseModel.csdl|res://*/Models.Entities.BaseModel.ssdl|res://*/Models.Entities.BaseModel.msl;"
                                          + $"provider=System.Data.SqlClient;"
                                          + $"provider connection string=\"data source={dataSources.Pop()};"
                                          + $"initial catalog=AutoPartsBase;"
                                          + $"integrated security=True;"
                                          + $"MultipleActiveResultSets=True;"
                                          + $"App=EntityFramework\"";
                try
                {
                    if (dataSources.Count == 0) throw new Exception("Все строки подключения недоступны");
                    using (AutoPartsBaseEntities db = new AutoPartsBaseEntities())
                    {
                        db.Database.Connection.Open();
                        PermamentConnectionString = CurrentConnectionString;
                        DependencyService
                            .Get<IFeedbackService>()
                            .InformAsync("База данных подключена "
                                         + "со строкой подключения "
                                         + PermamentConnectionString);
                        break;
                    }
                }
                catch (Exception ex)
                {
                    if (dataSources.Count == 0)
                    {
                        DependencyService
                            .Get<IFeedbackService>()
                            .InformErrorAsync(ex)
                            .Wait();
                        return;
                    }
                }
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
