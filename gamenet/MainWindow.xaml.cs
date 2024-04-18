using gamenet.VM;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace gamenet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowVM();
        }


        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }



        private void Users_Click(object sender, RoutedEventArgs e)
        {
            UserWindow u = new UserWindow();
            u.Show();
        }

        private void Reserveds_Click(object sender, RoutedEventArgs e)
        {
            ReserveWindow r = new ReserveWindow();
            r.Show();
        }
    }
}
