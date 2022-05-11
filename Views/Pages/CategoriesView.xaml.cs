using AutoPartsApp.Models.Entities;
using Microsoft.Win32;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace AutoPartsApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for CategoriesView.xaml
    /// </summary>
    public partial class CategoriesView : UserControl
    {
        public CategoriesView()
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
                    Category category = (sender as Ellipse).DataContext as Category;
                    entities.Categories.Find(category.Id).Image =
                        File.ReadAllBytes(imageFileDialog.FileName);
                    entities.SaveChanges();
                    ((dynamic)DataContext).LoadCategoriesAsync();
                }
            }
        }
    }
}
