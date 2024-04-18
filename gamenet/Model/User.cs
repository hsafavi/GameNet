using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace gamenet.Model
{
    public class User : INotifyPropertyChanged
    {
        private int wallet;

        public string Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Father { get; set; }
        public string Tel { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public int Num { get; set; }
        [NotMapped]
        public int Wallet { get => wallet; set { wallet = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Wallet")); } }
        public User()
        {
            IsActive = true;

            Id = Guid.NewGuid().ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
