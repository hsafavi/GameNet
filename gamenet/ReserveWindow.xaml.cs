using gamenet.VM;
using System.Windows;

namespace gamenet
{
    /// <summary>
    /// Interaction logic for ReserveWindow.xaml
    /// </summary>
    public partial class ReserveWindow : Window
    {
        public ReserveWindow()
        {
            InitializeComponent();
            DataContext = new ReserveWindowVM();
        }

    }
}
