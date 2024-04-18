using gamenet.Model;
using gamenet.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;

namespace gamenet.VM
{
    class MainWindowVM
    {
        public ObservableCollection<Station> Stations { get; }

        Dictionary<string, DispatcherTimer> timers = new Dictionary<string, DispatcherTimer>();
        public MainWindowVM()
        {

            using (Core db = new Core())
            {


                if (db.Stations.Any())
                {
                    Stations = new ObservableCollection<Station>(db.Stations);
                    foreach (var s in Stations)
                    {
                        if (s.Running)
                        {
                            DispatcherTimer timer = new DispatcherTimer();
                            timer.Interval = new TimeSpan(0, 0, 1);
                            timers.Add(s.Id, timer);
                            s.ElapsedTime = DateTime.Now.Subtract(s.StartTime.Value);
                            s.IsStarted = true;
                            s.ElapsedTime = TimeSpan.Parse(s.ElapsedTime.ToString(@"hh\:mm\:ss"));
                            timer.Tick += (sn, e) =>
                            {
                                s.ElapsedTime = s.ElapsedTime.Add(timer.Interval);
                                //s.TotalPrice = (int)(s.BaseFee * s.ElapsedTime.TotalMinutes / 60);
                            };
                            timer.Start();
                        }
                    }
                }
                else
                {
                    var s = new List<Station>() { new Station() { Num = 1}, new Station() { Num = 2 }, new Station() { Num = 3 }, new Station() { Num = 4 }, new Station() { Num = 5 },
                     new Station() { Num = 6}, new Station() { Num = 7}, new Station() { Num = 8,Type=Station.Types.Biliard}, new Station() { Num = 9,Type=Station.Types.Biliard}, new Station() { Num = 10,Type=Station.Types.Biliard}
                    };
                    db.Stations.AddRange(s);
                    Stations = new ObservableCollection<Station>(s);
                    db.SaveChanges();
                }

            }

        }
        public System.Windows.Input.ICommand Pause => new Command(pause);

        private void pause(object obj)
        {
            var s = (Station)obj;
            //s.TotalPrice = (int)( s.BaseFee * s.ElapsedTime.TotalMinutes / 60);
            if (s.Running)
            {
                using (Core db = new Core())
                {
                    var sta = db.Stations.First(st => st.Id == s.Id);
                    sta.Running = false;
                    db.Update(sta);
                    db.SaveChanges();
                }

                timers.First(t => t.Key == s.Id).Value.Stop();

                s.Running = false;
            }
            else
            {
                using (Core db = new Core())
                {
                    var sta = db.Stations.First(st => st.Id == s.Id);
                    sta.Running = true;
                    db.Update(sta);
                    db.SaveChanges();
                }

                timers.First(t => t.Key == s.Id).Value.Start();

                s.Running = true;
            }

        }
        public System.Windows.Input.ICommand Start => new Command(start);

        private void start(object obj)
        {
            var s = (Station)obj;
            s.ElapsedTime = new TimeSpan();
            s.StartTime = DateTime.Now;
            s.Running = true;
            s.IsStarted = true;
            if (timers.ContainsKey(s.Id))
            {
                timers.First(t => t.Key == s.Id).Value.Start();

            }
            else
            {
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = new TimeSpan(0, 0, 1);
                timers.Add(s.Id, timer);
                timer.Tick += (sn, e) =>
                {
                    s.ElapsedTime = s.ElapsedTime.Add(timer.Interval);
                    //s.TotalPrice = (int)(s.BaseFee * s.ElapsedTime.TotalMinutes / 60);
                };
                timer.Start();
            }
            using (Core db = new Core())
            {
                var sta = db.Stations.First(st => st.Id == s.Id);
                sta.StartTime = s.StartTime;
                sta.BaseFee = s.BaseFee;
                sta.Running = true;
                db.Update(sta);
                db.SaveChanges();
            }
        }
    }
}
