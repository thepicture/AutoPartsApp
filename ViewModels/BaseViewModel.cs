using AutoPartsApp.Models.Entities;
using AutoPartsApp.Models.Specific;
using AutoPartsApp.Services;
using System.Collections.Generic;
using System;
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
        public IRepository<LoginUser> LoginRepository =>
           DependencyService.Get<IRepository<LoginUser>>();
        public IRepository<User> UserRepository =>
           DependencyService.Get<IRepository<User>>();
        public IRepository<Part> PartRepository =>
           DependencyService.Get<IRepository<Part>>();
        public IRepository<Contact> ContactsRepository =>
           DependencyService.Get<IRepository<Contact>>();
        public IRepository<Feedback> FeedbackRepository =>
           DependencyService.Get<IRepository<Feedback>>();

        public bool IsBusy { get; set; }
        public bool IsNotBusy => !IsBusy;
        public bool IsRefreshing { get; set; }

        public string Title { get; set; }

        protected bool SetProperty<T>(ref T backingStore, T value,
           [CallerMemberName] string propertyName = "",
           Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }


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
