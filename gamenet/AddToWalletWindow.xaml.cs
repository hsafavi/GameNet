using gamenet.Model;
using gamenet.VM;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace gamenet
{
    /// <summary>
    /// Interaction logic for AddToWalletWindow.xaml
    /// </summary>
    public partial class AddToWalletWindow : Window
    {
        public AddToWalletWindow(User user)
        {
            InitializeComponent();
            DataContext = new AddToWalletWindowVM(user, this);
        }
        private static readonly Regex _regex = new Regex("[^0-9]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

    }
}
