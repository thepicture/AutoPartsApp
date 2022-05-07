using AutoPartsApp.Models.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AutoPartsApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class AnalogViewModel : BaseViewModel
    {
        public Part ReferencePart { get; set; }
        public AnalogViewModel(Part referencePart)
        {
            Title = $"Аналоги запчасти {referencePart.Title}";
            ReferencePart = referencePart;
            LoadPartsAsync();
        }

        public async void LoadPartsAsync()
        {
            IEnumerable<Part> currentParts =
                await PartRepository.GetAllAsync();
            currentParts = currentParts.Where(p => p.Id != ReferencePart.Id);
            currentParts = currentParts.Where(p => p.BaseCode.Contains(ReferencePart.BaseCode));

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
                    return p.PricePerUnitInRubles == parsedSearchRubles
                           || p.PriceOfStockInRubles == parsedSearchRubles;
                });
            }

            Parts = new ObservableCollection<Part>(currentParts);
        }

        public ObservableCollection<Part> Parts { get; set; }

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
                if(SetProperty(ref baseCodeSearchText, value))
                {
                    LoadPartsAsync();
                }
            }
        }
    }
}
