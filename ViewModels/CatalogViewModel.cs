using AutoPartsApp.Commands;
using AutoPartsApp.Models.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        public CatalogViewModel(Part referencePart)
        {
            Title = $"Аналоги запчасти {referencePart.Title}";
            ReferencePart = referencePart;
            LoadPartsAsync();
        }

        private const int DifferenceBetweenPricesInRubles = 100;

        public async void LoadPartsAsync()
        {
            IEnumerable<Part> currentParts =
                await PartRepository.GetAllAsync();
            if (ReferencePart != null)
            {
                currentParts = currentParts.Where(p =>
                {
                    return 2 == 1;
                });
                currentParts = currentParts.Where(p => p.Id != ReferencePart.Id);
            }
            Parts = new ObservableCollection<Part>(
                currentParts);
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

        public Part ReferencePart { get; set; }

        private void GoToAnalogues(object parameter)
        {
            NavigationService.NavigateBack();
            NavigationService
                .NavigateWithParameter<CatalogViewModel, Part>(parameter as Part);
        }
    }
}