using AutoPartsApp.Commands;
using AutoPartsApp.Models.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace AutoPartsApp.ViewModels
{
    public class SubCategoriesViewModel : BaseViewModel
    {
        public SubCategoriesViewModel(Category category)
        {
            Title = $"Подкатегории категории {category.Title}";
            LoadSubCategoriesAsync();
            Category = category;
        }

        public async void LoadSubCategoriesAsync()
        {
            IEnumerable<SubCategory> currentSubCategories =
                await SubCategoriesRepository
                    .GetAllAsync();
            currentSubCategories = currentSubCategories
                .Where(sc => sc.CategoryId == Category.Id);
            SubCategories = new ObservableCollection<SubCategory>(currentSubCategories);
        }

        private ObservableCollection<SubCategory> subCategories;

        public ObservableCollection<SubCategory> SubCategories
        {
            get => subCategories;
            set => SetProperty(ref subCategories, value);
        }
        public Category Category { get; }

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

        private void PerformGoToCatalogViewModel(object param)
        {
            NavigationService
                .NavigateWithParameter<CatalogViewModel, SubCategory>(
                    param as SubCategory);
        }
    }
}