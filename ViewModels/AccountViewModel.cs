using AutoPartsApp.Commands;
using AutoPartsApp.Models.Entities;
using Microsoft.Win32;
using System.IO;
using System.Windows.Input;

namespace AutoPartsApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class AccountViewModel : BaseViewModel
    {
        public AccountViewModel()
        {
            Title = "Личный кабинет";
            User = Identity.WeakTarget;
        }

        private Command saveChangesCommand;

        public ICommand SaveChangesCommand
        {
            get
            {
                if (saveChangesCommand == null)
                    saveChangesCommand = new Command(SaveChangesAsync);

                return saveChangesCommand;
            }
        }

        private async void SaveChangesAsync()
        {
            if (await UserRepository.UpdateAsync(User))
            {
                Identity.WeakTarget = User;
            }
        }

        public User User { get; set; }

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

        private Command goToCatalogViewModel;

        public ICommand GoToCatalogViewModel
        {
            get
            {
                if (goToCatalogViewModel == null)
                    goToCatalogViewModel = new Command(PerformGoToCatalogViewModel);

                return goToCatalogViewModel;
            }
        }

        private void PerformGoToCatalogViewModel()
        {
            NavigationService.Navigate<CatalogViewModel>();
        }

        private Command goToContactsViewModel;

        public ICommand GoToContactsViewModel
        {
            get
            {
                if (goToContactsViewModel == null)
                    goToContactsViewModel = new Command(PerformGoToContactsViewModel);

                return goToContactsViewModel;
            }
        }

        private void PerformGoToContactsViewModel()
        {
            NavigationService.Navigate<ContactsViewModel>();
        }
    }
}