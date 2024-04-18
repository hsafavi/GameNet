using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace gamenet.Model
{
    public class Station : INotifyPropertyChanged
    {
        public enum Types { PlayStation, Biliard }
        private TimeSpan elapsedTime;
        private bool running;
        private bool isStarted;

        public string Id { get; set; }
        public int Num { get; set; }
        public int BaseFee { get; set; }
        [NotMapped]
        public string PlayerName { get; set; }
        public bool Running { get => running; set { running = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Running")); } }

        public DateTime? StartTime { get; set; }
        [NotMapped]
        public TimeSpan ElapsedTime { get => elapsedTime; set { elapsedTime = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ElapsedTime")); PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalPrice")); } }

        [NotMapped]
        public int TotalPrice { get => (int)(BaseFee * ElapsedTime.TotalMinutes / 60); }

        [NotMapped]
        public bool IsStarted { get => isStarted; set { isStarted = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsStarted")); } }
        public Station()
        {
            Id = Guid.NewGuid().ToString();
            Type = Types.PlayStation;
        }
        public Types Type { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
