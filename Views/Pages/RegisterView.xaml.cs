using System.Windows;
using System.Windows.Controls;

namespace AutoPartsApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : UserControl
    {
        public RegisterView()
        {
            InitializeComponent();
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            ((dynamic)DataContext).User.Password = (sender as PasswordBox).Password;
        }
    }
}
