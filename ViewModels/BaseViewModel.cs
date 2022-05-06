using AutoPartsApp.Models;
using AutoPartsApp.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace AutoPartsApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public INavigationService<BaseViewModel> NavigationService =>
            DependencyService.Get<INavigationService<BaseViewModel>>();
        public IIdentity<User> Identity =>
           DependencyService.Get<IIdentity<User>>();
        public IFeedbackService FeedbackService =>
           DependencyService.Get<IFeedbackService>();

        public bool IsBusy { get; set; }
        public bool IsNotBusy => !IsBusy;
        public bool IsRefreshing { get; set; }

        public BaseViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(
                new DependencyObject()))
            {
                _ = typeof(App)
                    .GetMethod("OnStartup")
                    .Invoke(
                        new App(), null);
            }
        }

        public string Title { get; set; }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventHandler changed = PropertyChanged;
            if (changed == null)
            {
                return;
            }

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
