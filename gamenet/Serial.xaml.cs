using gamenet.VM;
using System.Windows;

namespace gamenet
{
    /// <summary>
    /// Interaction logic for Serial.xaml
    /// </summary>
    public partial class Serial : Window
    {
        public Serial()
        {
            InitializeComponent();
            DataContext = new SerialVM(this);
        }
    }
}
