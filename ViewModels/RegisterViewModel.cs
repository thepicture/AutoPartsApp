using AutoPartsApp.Commands;
using AutoPartsApp.Models.Entities;
using AutoPartsApp.Models.Specific;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutoPartsApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class RegisterViewModel : BaseViewModel
    {
        public RegisterUser User { get; set; } = new RegisterUser();
        public RegisterViewModel()
        {
            Title = "Регистрация";
            Task.Run(async () =>
            {
                UserRoles = new ObservableCollection<UserRole>(
                    await UserRoleRepository.GetAllAsync());
            });
        }

        public ObservableCollection<UserRole> UserRoles { get; set; }

        private Command selectImageCommand;

        public ICommand SelectImageCommand
        {
            get
            {
                if (selectImageCommand == null)
                    selectImageCommand = new Command(SelectImageAsync);

                return selectImageCommand;
            }
        }

        private async void SelectImageAsync()
        {
            OpenFileDialog imageFileDialog = new OpenFileDialog
            {
                Title = "Выберите фото"
            };
            bool? isSelectedImage = imageFileDialog.ShowDialog();
            if (isSelectedImage.HasValue && isSelectedImage.Value)
            {
                User.Image = File.ReadAllBytes(imageFileDialog.FileName);
                await FeedbackService.InformAsync("Фото изменено");
            }
        }

        private Command registerCommand;

        public ICommand RegisterCommand
        {
            get
            {
                if (registerCommand == null)
                    registerCommand = new Command(RegisterAsync);

                return registerCommand;
            }
        }

        private async void RegisterAsync(object obj)
        {
            if (await RegistrationRepository.CreateAsync(User))
            {
                NavigationService.NavigateToRoot();
            }
        }
    }
}
