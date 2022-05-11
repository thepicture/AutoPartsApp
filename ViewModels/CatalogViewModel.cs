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

        public async void LoadPartsAsync()
        {
            IEnumerable<Part> currentParts =
                await PartRepository.GetAllAsync();
            if (!string.IsNullOrWhiteSpace(BaseCodeSearchText))
            {
                currentParts = currentParts.Where(p =>
                {
                    return p.BaseCode
                    .ToLower()
                    .Contains(
                        BaseCodeSearchText.ToLower());
                });
            }
            if (!string.IsNullOrWhiteSpace(PriceSearchTextInRubles)
                && decimal.TryParse(PriceSearchTextInRubles,
                                    out decimal parsedSearchRubles))
            {
                currentParts = currentParts.Where(p =>
                {
                    return p.PricePerUnitInRubles <= parsedSearchRubles
                           || p.PriceOfStockInRubles <= parsedSearchRubles;
                });
            }
            Parts = new ObservableCollection<Part>(currentParts);
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

        private string priceSearchTextInRubles;

        public string PriceSearchTextInRubles
        {
            get => priceSearchTextInRubles;
            set
            {
                if (SetProperty(ref priceSearchTextInRubles, value))
                {
                    LoadPartsAsync();
                }
            }
        }

        private string baseCodeSearchText;

        public string BaseCodeSearchText
        {
            get => baseCodeSearchText;
            set
            {
                if (SetProperty(ref baseCodeSearchText, value))
                {
                    LoadPartsAsync();
                }
            }
        }
    }
}