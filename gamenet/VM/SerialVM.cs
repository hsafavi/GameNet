using gamenet.Model;
using gamenet.Utility;
using System.Linq;
using System.Windows;

namespace gamenet.VM
{
    class SerialVM
    {

        public string Serial { get; set; }
        public string Id { get; set; }

        public SerialVM(Serial serialWindow)
        {
            SerialWindow = serialWindow;
            Id = Security.GetId();
        }

        public System.Windows.Input.ICommand InputCmd => new Command(Input);


        private void Input(object obj)
        {
            if (Security.CheckSerial(Serial))
            {
                using (Core db = new Core())
                {
                    if(db.Settings.Any(s=>s.Key=="s"))
                    {
                        db.Settings.Update(new Settings() { Key = "s", Value = Serial });
                    }
                    else
                    {
                        db.Settings.Add(new Settings() { Key = "s", Value = Serial });
                    }
                    db.SaveChanges();
                    MainWindow main = new MainWindow();
                    main.Show();
                    SerialWindow.Close();
                }

            }
            else
            {
                MessageBox.Show("سریال نادرست", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        public System.Windows.Input.ICommand DeleteCmd => new Command(Delete);

        public Serial SerialWindow { get; }

        private void Delete(object obj)
        {
        }

        //////
        ///
        public class Item
        {
            public string Key { get; set; }
        }
    }
}
