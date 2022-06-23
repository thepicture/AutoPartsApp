using AutoPartsApp.Commands;
using AutoPartsApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace AutoPartsApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class AnalogViewModel : BaseViewModel
    {
        public Part ReferencePart { get; set; }
        public SubCategory SubCategory { get; }

        public AnalogViewModel(object[] referencePartAndSubCategory)
        {
            ReferencePart = referencePartAndSubCategory[0] as Part;
            Title = $"Аналоги запчасти {ReferencePart.Title}";
            SubCategory = referencePartAndSubCategory[1] as SubCategory;
            LoadPartsAsync();
        }

        private const int MaxPriceOfStockDifference = 1000;

        public async void LoadPartsAsync()
        {
            IEnumerable<Part> currentParts =
                await PartRepository.GetAllAsync();
            currentParts = currentParts.Where(p => p.Id != ReferencePart.Id);
            currentParts = currentParts.Where(p =>
            {
                return Math.Abs(p.PriceOfStockInRubles - ReferencePart.PriceOfStockInRubles) < MaxPriceOfStockDifference;
            });

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
            currentParts = currentParts.Where(p => p.SubCategoryId == SubCategory.Id);
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
                if (SetProperty(ref baseCodeSearchText, value))
                {
                    LoadPartsAsync();
                }
            }
        }

        private Command goToAnaloguesCommand;

        public ICommand GoToAnaloguesCommand
        {
            get
            {
                if (goToAnaloguesCommand == null)
                    goToAnaloguesCommand = new Command(() => { });

                return goToAnaloguesCommand;
            }
        }
    }
}
