using AutoPartsApp.Models.Entities;
using AutoPartsApp.Models.Specific;
using AutoPartsApp.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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
        public IRepository<UserRole> UserRoleRepository =>
           DependencyService.Get<IRepository<UserRole>>();
        public IRepository<RegisterUser> RegistrationRepository =>
           DependencyService.Get<IRepository<RegisterUser>>();
        public IHashGenerator HashGenerator =>
           DependencyService.Get<IHashGenerator>();

        public bool IsBusy { get; set; }
        public bool IsNotBusy => !IsBusy;
        public bool IsRefreshing { get; set; }

        public BaseViewModel()
        {

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
