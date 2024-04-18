using gamenet.VM;
using System.Windows;

namespace gamenet
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private UserWindowVM vm;

        public UserWindow()
        {
            InitializeComponent();
            vm = new UserWindowVM();
            DataContext = vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Search_Changed(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            vm.Search = SearchBox.Text;
        }
    }
}
