using AutoPartsApp.ViewModels;
using System;
using System.Collections.Specialized;
using System.Linq;

namespace AutoPartsApp.Services
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class NavigationService : INavigationService<BaseViewModel>
    {
        public NavigationService()
        {
            Journal.CollectionChanged += OnNavigated;
        }

        private void OnNavigated(object sender,
                                 NotifyCollectionChangedEventArgs e)
        {
            Navigated?.Invoke();
        }

        public ObservableStack<BaseViewModel> Journal { get; set; } =
            new ObservableStack<BaseViewModel>();

        public BaseViewModel CurrentTarget => Journal.Peek();

        public event Action Navigated;

        public void Navigate<TWhere>() where TWhere : BaseViewModel
        {
            Journal.Push(
                Activator.CreateInstance<TWhere>());
        }

        public void NavigateBack()
        {
            Journal.Pop();
        }

        public void NavigateToRoot()
        {
            while (Journal.Count > 1)
            {
                NavigateBack();
            }
        }

        public void NavigateWithParameter<TWhere, TParam>(TParam param)
            where TWhere : BaseViewModel
        {
            Journal.Push(
                (BaseViewModel)
                Activator.CreateInstance(typeof(TWhere),
                                         new object[] { param }));
        }

        public bool CanNavigateBack()
        {
            if (Journal.Count < 2)
            {
                return false;
            }
            BaseViewModel viewModel = Journal
                .ElementAt(1);
            return !(viewModel is LoginViewModel)
                   || Journal.Peek() is RegisterViewModel;
        }
    }
}
