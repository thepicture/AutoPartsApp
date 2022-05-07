using AutoPartsApp.Commands;
using AutoPartsApp.Models.Specific;
using System.Windows.Input;

namespace AutoPartsApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
            Title = "Логин";
        }

        private Command goToRegisterViewModelCommand;

        public ICommand GoToRegisterViewModelCommand
        {
            get
            {
                if (goToRegisterViewModelCommand == null)
                {
                    goToRegisterViewModelCommand = new Command(GoToRegisterViewModel);
                }

                return goToRegisterViewModelCommand;
            }
        }

        private void GoToRegisterViewModel()
        {
            NavigationService.Navigate<RegisterViewModel>();
        }

        public LoginUser User { get; set; } = new LoginUser();

        private Command loginCommand;

        public ICommand LoginCommand
        {
            get
            {
                if (loginCommand == null)
                    loginCommand = new Command(LoginAsync);

                return loginCommand;
            }
        }

        private async void LoginAsync()
        {
            IsBusy = true;
            if (await LoginRepository.CreateAsync(User))
            {
                NavigationService.Navigate<AccountViewModel>();
            }
            IsBusy = false;
        }
    }
}
