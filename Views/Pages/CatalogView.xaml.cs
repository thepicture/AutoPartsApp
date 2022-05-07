using System.Windows;
using System.Windows.Controls;

namespace AutoPartsApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for CatalogView.xaml
    /// </summary>
    public partial class CatalogView : UserControl
    {
        public CatalogView()
        {
            InitializeComponent();
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((dynamic)DataContext).LoadPartsAsync();
        }
    }
}
