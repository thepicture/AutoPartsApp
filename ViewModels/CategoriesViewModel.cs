using AutoPartsApp.Commands;
using AutoPartsApp.Models.Entities;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace AutoPartsApp.ViewModels
{
    public class CategoriesViewModel : BaseViewModel
    {
        public CategoriesViewModel()
        {
            Title = "Категории";
            LoadCategoriesAsync();
        }

        public async void LoadCategoriesAsync()
        {
            Categories = new ObservableCollection<Category>(
                await CategoriesRepository.GetAllAsync());
        }

        private ObservableCollection<Category> categories;

        public ObservableCollection<Category> Categories
        {
            get => categories;
            set => SetProperty(ref categories, value);
        }

        private Command goToSubCategoriesViewModel;

        public ICommand GoToSubCategoriesViewModel
        {
            get
            {
                if (goToSubCategoriesViewModel == null)
                    goToSubCategoriesViewModel = new Command(PerformGoToSubCategoriesViewModel);

                return goToSubCategoriesViewModel;
            }
        }

        private void PerformGoToSubCategoriesViewModel(object param)
        {
            Category inputCategory = param as Category;
            using (AutoPartsBaseEntities entities = new AutoPartsBaseEntities())
            {
                SubCategory subCategoryFromDb = entities.SubCategories
                    .FirstOrDefault(sc => sc.Title == inputCategory.Title);
                if (subCategoryFromDb != null)
                {
                    NavigationService
                        .NavigateWithParameter<CatalogViewModel, SubCategory>(subCategoryFromDb);
                }
                else
                {
                    NavigationService
                        .NavigateWithParameter<SubCategoriesViewModel, Category>(
                            param as Category);
                }
            }
        }
    }
}