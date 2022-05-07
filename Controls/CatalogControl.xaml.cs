using AutoPartsApp.Models.Entities;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AutoPartsApp.Controls
{
    /// <summary>
    /// Interaction logic for CatalogControl.xaml
    /// </summary>
    public partial class CatalogControl : UserControl
    {


        public bool IsAnalogue
        {
            get { return (bool)GetValue(IsAnalogueProperty); }
            set { SetValue(IsAnalogueProperty, value); }
        }

        public static readonly DependencyProperty IsAnalogueProperty =
            DependencyProperty.Register("IsAnalogue",
                                        typeof(bool),
                                        typeof(CatalogControl),
                                        new PropertyMetadata(default(bool)));


        public ObservableCollection<Part> Parts
        {
            get { return (ObservableCollection<Part>)GetValue(PartsProperty); }
            set { SetValue(PartsProperty, value); }
        }

        public static readonly DependencyProperty PartsProperty =
            DependencyProperty.Register("Parts",
                                        typeof(ObservableCollection<Part>),
                                        typeof(CatalogControl),
                                        new FrameworkPropertyMetadata(new ObservableCollection<Part>(),
                                                                      FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        public CatalogControl()
        {
            InitializeComponent();
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            (sender as Flipper).IsFlipped = !(sender as Flipper).IsFlipped;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((dynamic)DataContext).LoadPartsAsync();
        }
    }
}
