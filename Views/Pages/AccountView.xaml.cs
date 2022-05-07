using System.Windows;
using System.Windows.Controls;

namespace AutoPartsApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for AccountView.xaml
    /// </summary>
    public partial class AccountView : UserControl
    {
        public AccountView()
        {
            InitializeComponent();
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            ((dynamic)DataContext).User.Password = (sender as PasswordBox).Password;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            PBoxPassword.Password = ((dynamic)DataContext).User.Password;
        }
    }
}
