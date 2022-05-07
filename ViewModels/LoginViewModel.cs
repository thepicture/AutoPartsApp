using AutoPartsApp.Commands;
using System.Windows.Input;

namespace AutoPartsApp.ViewModels
{
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
    }
}
