using gamenet.Model;
using gamenet.Utility;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace gamenet.VM
{
    class UserWindowVM : INotifyPropertyChanged
    {
        private bool editMode;
        private bool editOrRegister;
        private ObservableCollection<User> users;
        private User user;
        private bool justActiveUsers;
        private string search;

        public delegate void UserWindowVMDlg();

        public ObservableCollection<User> Users { get => users; private set { users = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Users")); } }

        public bool EditMode
        {
            get => editMode;
            set { editMode = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EditMode")); }
        }
        public bool JustActiveUsers { get => justActiveUsers; set { justActiveUsers = value; loadItems(); } }
        public bool EditOrRegister { get => editOrRegister; set { editOrRegister = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EditOrRegister")); } }
        public string Search { get => search; set { search = value; loadItems(); } }

        public UserWindowVM()
        {
            JustActiveUsers = true;
            User = new User();
            loadItems();

        }
        private void loadItems()
        {
            using (Core db = new Core())
            {

                Users = new ObservableCollection<User>(db.Users.Where(u => (string.IsNullOrWhiteSpace(search) || (u.Name + " " + u.Family).Contains(search)) && !u.IsDeleted && (!JustActiveUsers || u.IsActive)));
                var userBills = users.GroupJoin(db.Bills, usr => usr.Id, b => b.UserId, (usr, b) => new { usr, w = b.Sum(bill => bill.Fee) }).ToList();
                foreach (var ub in userBills)
                {
                    ub.usr.Wallet = ub.w;
                }
            }
        }
        public User User { get => user; set { user = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("User")); } }

        public System.Windows.Input.ICommand AddUserCmd => new Command(AddUser);

        public event PropertyChangedEventHandler PropertyChanged;

        private void AddUser(object obj)
        {
            EditMode = false;
            User = new User();

            EditOrRegister = true;
        }
        public System.Windows.Input.ICommand EditCmd => new Command(Edit);

        private void Edit(object obj)
        {
            EditMode = true;
            User = (User)obj;
            EditOrRegister = true;
        }
        public System.Windows.Input.ICommand CancelCmd => new Command(Cancel);

        private void Cancel(object obj)
        {
            EditMode = false;
            EditOrRegister = false;
        }
        public System.Windows.Input.ICommand DeleteCmd => new Command(Delete);

        private void Delete(object obj)
        {
            var user = (User)obj;
            //s.TotalPrice = (int)( s.BaseFee * s.ElapsedTime.TotalMinutes / 60);
            if (MessageBox.Show("آیا میخواهید این مشترک حذف شود؟", "حذف مشترک", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                using (Core db = new Core())
                {
                    user.IsDeleted = true;
                    db.Users.Update(user);
                    Users.Remove(user);
                    db.SaveChanges();
                }

        }
        public System.Windows.Input.ICommand SaveCmd => new Command(Save);

        private void Save(object obj)
        {
            if (EditMode)
            {
                var user = (User)obj;
                User.Family?.Trim();
                user.Father?.Trim();
                user.Name?.Trim();
                user.Tel?.Trim();
                using (Core db = new Core())
                {
                    var usr = db.Users.First(u => u.Id == user.Id);
                    usr.Name = user.Name;
                    user.Family = user.Family;
                    usr.Father = user.Father;
                    usr.Tel = user.Tel;
                    usr.IsActive = user.IsActive;
                    db.Users.Update(usr);
                    db.SaveChanges();
                    EditOrRegister = false;
                }
            }
            else
            {
                using (Core db = new Core())
                {
                    if (db.Users.Any(u => !u.IsDeleted && u.Name == User.Name && u.Family == User.Family && u.Father == User.Family))
                    {
                        MessageBox.Show("مشترکی با همین مشخصات قبلا ثبت شده است", "مشترک تکراری", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }


                    user.Num = db.Users.Any() ? db.Users.Max(u => u.Num) + 1 : 1;
                    db.Add(User);
                    db.SaveChanges();
                    users.Add(user);
                    User = new User();

                }
            }
        }
        public System.Windows.Input.ICommand WalletTransCmd => new Command(WalletTrans);

        private void WalletTrans(object obj)
        {
            var u = (User)obj;
            var w = new AddToWalletWindow(u);
            w.ShowDialog();
        }
    }
}
