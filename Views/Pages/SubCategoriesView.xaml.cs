using AutoPartsApp.Models.Entities;
using Microsoft.Win32;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace AutoPartsApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for SubCategoriesView.xaml
    /// </summary>
    public partial class SubCategoriesView : UserControl
    {
        public SubCategoriesView()
        {
            InitializeComponent();
        }

        private void OnChangeImage(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            using (AutoPartsBaseEntities entities = new AutoPartsBaseEntities())
            {
                OpenFileDialog imageFileDialog = new OpenFileDialog
                {
                    Title = "Выберите фото"
                };
                bool? isSelectedImage = imageFileDialog.ShowDialog();
                if (isSelectedImage.HasValue && isSelectedImage.Value)
                {
                    SubCategory subCategory = (sender as Ellipse).DataContext as SubCategory;
                    entities.SubCategories.Find(subCategory.Id).Image =
                        File.ReadAllBytes(imageFileDialog.FileName);
                    entities.SaveChanges();
                    ((dynamic)DataContext).LoadSubCategoriesAsync();
                }
            }
        }
    }
}
