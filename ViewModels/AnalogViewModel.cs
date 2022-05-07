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
            Parts = new ObservableCollection<Part>(currentParts);
        }

        public ObservableCollection<Part> Parts { get; set; }
    }
}
