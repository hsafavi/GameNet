using gamenet.Model;
using gamenet.Utility;
using System;
using System.Windows;

namespace gamenet.VM
{
    class AddToWalletWindowVM
    {
        private readonly Window owner;

        public string Msg { get; private set; }
        public User User { get; set; }
        public bool MinusMode { get; set; }
        public int Value { get; set; }
        public AddToWalletWindowVM(User user, Window owner)
        {
            MinusMode = true;
            User = user;
            this.owner = owner;
        }
        public System.Windows.Input.ICommand RegisterCmd => new Command(Register);

        private void Register(object obj)
        {
            using (Core db = new Core())
            {
                db.Bills.Add(new Bill() { UserId = this.User.Id, DateTime = DateTime.Now, Fee = (MinusMode ? -1 * Value : Value) });
                User.Wallet += MinusMode ? -1 * Value : Value;
                db.SaveChanges();
            }
            owner.Close();
        }

        public System.Windows.Input.ICommand CancelCmd => new Command(Cancel);

        private void Cancel(object obj)
        {
            owner.Close();
        }
    }
}
