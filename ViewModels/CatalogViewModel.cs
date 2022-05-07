using AutoPartsApp.Commands;
using AutoPartsApp.Models.Entities;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AutoPartsApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class CatalogViewModel : BaseViewModel
    {
        public CatalogViewModel()
        {
            Title = "Каталог";
            LoadPartsAsync();
        }

        public async void LoadPartsAsync()
        {
            Parts = new ObservableCollection<Part>(
                await PartRepository.GetAllAsync());
        }

        public ObservableCollection<Part> Parts { get; set; }

        private Command goToAnaloguesCommand;

        public ICommand GoToAnaloguesCommand
        {
            get
            {
                if (goToAnaloguesCommand == null)
                    goToAnaloguesCommand = new Command(GoToAnalogues);

                return goToAnaloguesCommand;
            }
        }

        private void GoToAnalogues(object parameter)
        {
            NavigationService
                .NavigateWithParameter<AnalogViewModel, Part>(parameter as Part);
        }
    }
}