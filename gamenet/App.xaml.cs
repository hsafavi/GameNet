using gamenet.Model;
using gamenet.Utility;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace gamenet
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            splash sp = new splash();
            sp.Show();
            using (Core db = new Core())
            {
                db.Database.EnsureCreated();
                var s = db.Settings.LastOrDefault(st => st.Key == "s")?.Value;

                try
                {
                    (new MainWindow()).Show();
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.ToString());
                }
            }

            sp.Close();
            base.OnStartup(e);
        }


        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
        }
    }
}