using AutoPartsApp.Commands;
using System.Windows.Input;

namespace AutoPartsApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class NavigationViewModel : BaseViewModel
    {
        public NavigationViewModel()
        {
            NavigationService.Navigated += OnNavigated;
        }

        private void OnNavigated()
        {
            OnPropertyChanged(
                nameof(CurrentViewModel));
            OnPropertyChanged(
                nameof(IsCanGoBack));
            OnPropertyChanged(
                nameof(IsCanClearIdentity));
        }

        public BaseViewModel CurrentViewModel =>
            NavigationService.CurrentTarget;
        public bool IsCanGoBack =>
            NavigationService.CanNavigateBack();
        public bool IsCanClearIdentity =>
            Identity.StrongTarget != null || Identity.WeakTarget != null;

        private Command goBackCommand;

        public ICommand GoBackCommand
        {
            get
            {
                if (goBackCommand == null)
                {
                    goBackCommand = new Command(GoBack);
                }

                return goBackCommand;
            }
        }

        private void GoBack(object commandParameter)
        {
            NavigationService.NavigateBack();
        }

        private Command clearSessionCommand;

        public ICommand ClearSessionCommand
        {
            get
            {
                if (clearSessionCommand == null)
                {
                    clearSessionCommand = new Command(ClearSessionAsync);
                }

                return clearSessionCommand;
            }
        }

        private async void ClearSessionAsync()
        {
            if (await FeedbackService.AskAsync("Вернуться на страницу логина?"))
            {
                Identity.Logout();
                NavigationService.NavigateToRoot();
            }
        }
    }
}