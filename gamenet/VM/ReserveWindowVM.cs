using gamenet.Model;
using gamenet.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace gamenet.VM
{
    class ReserveWindowVM : INotifyPropertyChanged
    {
        private DateTime date;
        private KeyValuePair<string, string> selectedConsoleFilter;
        private ObservableCollection<ReservedStation> reservedStations;

        public delegate void UserWindowVMDlg();

        public ObservableCollection<ReservedStation> ReservedStations { get => reservedStations; set { reservedStations = value; PropertyChanged?.Invoke(this,new PropertyChangedEventArgs("ReservedStations")); } }
        public List<string> Hours { get; set; }
        public List<string> Minutes { get; set; }
        public Dictionary<string, string> Consoles { get; set; }
        public Dictionary<string, string> FilterConsoles { get; set; }
        public string SelectedHour { get; set; }
        public string SelectedMinute { get; set; }
        public byte SelectedToHour { get; set; }
        public byte SelectedToMinute { get; set; }
        public string Description { get; set; }
        public KeyValuePair<string, string> SelectedConsole { get; set; }
        public KeyValuePair<string, string> SelectedConsoleFilter { get => selectedConsoleFilter; set { selectedConsoleFilter = value; loadData(); } }
        public DateTime Date
        {
            get => date;
            set => date = value;
        }
        public ReserveWindowVM()
        {
            Hours = new List<string>();
            Minutes = new List<string>();
            Consoles = new Dictionary<string, string>();
            FilterConsoles = new Dictionary<string, string>();
            Minutes.AddRange(new string[] { "0", "15", "30", "45" });
            Date = DateTime.Now;
            SelectedMinute = "0";
            for (int i = 0; i < 24; i++)
            {
                Hours.Add(i.ToString());

            }
            SelectedHour = DateTime.Now.Hour.ToString();
            using (Core d = new Core())
            {
                var stations = d.Stations.OrderBy(s => s.Num);
                foreach (var s in stations)
                    Consoles.Add(s.Num.ToString(), s.Id);

                ReservedStations = new ObservableCollection<ReservedStation>(d.ReservedStations.Take(50).Include(r => r.Station));

            }
            SelectedConsole = Consoles.First();
            FilterConsoles.Add("همه", "-1");
            foreach (var c in Consoles)
            {
                FilterConsoles.Add(c.Key, c.Value);
            }
            selectedConsoleFilter = FilterConsoles.First();

        }
        private void loadData()
        {
            using (Core d = new Core())
            {

                ReservedStations = new ObservableCollection<ReservedStation>(d.ReservedStations.Where(r => selectedConsoleFilter.Value == "-1" || r.StationId == selectedConsoleFilter.Value).Take(50).Include(r => r.Station));

            }
        }
        public System.Windows.Input.ICommand RegisterCmd => new Command(Register);

        public event PropertyChangedEventHandler PropertyChanged;

        private void Register(object obj)
        {
            Date = Date.Date.AddHours(int.Parse(SelectedHour)).AddMinutes(int.Parse(SelectedMinute));
            if (Date < DateTime.Now)
            {
                MessageBox.Show("زمان انتخاب شده باید پس از زمان فعلی باشد");
                return;
            }

            using (Core db = new Core())
            {

                var rs = new ReservedStation()
                {
                    DateTime = Date,
                    Description = this.Description,
                    StationId = db.Stations.First(s => s.Id == SelectedConsole.Value).Id
                ,
                    ToHour = SelectedToHour,
                    ToMinute = SelectedToMinute
                };
                if (db.ReservedStations.Any(r => r.StationId ==SelectedConsole.Value && r.DateTime == rs.DateTime))
                {
                    MessageBox.Show("این کنسول در زمان مشخص شده قبلا رزرو شده است", "عدم امکان رزرو", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                db.ReservedStations.Add(
                rs
                );

                db.SaveChanges();
                ReservedStations.Add(rs);

            }
            MessageBox.Show("کنسول رزرو شد");
        }
        public System.Windows.Input.ICommand DeleteCmd => new Command(Delete);

        private void Delete(object obj)
        {
            var rs = (ReservedStation)obj;
            if (MessageBox.Show("آیا میخواهید این رزرو حذف شود؟", "حذف رزرو", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                using (Core db = new Core())
                {
                    db.ReservedStations.Remove(rs);
                    db.SaveChanges();
                    ReservedStations.Remove(rs);
                }
            }
        }

        //////
        ///
        public class Item
        {
            public string Key { get; set; }
        }
    }
}
