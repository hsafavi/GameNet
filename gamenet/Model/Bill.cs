using System;

namespace gamenet.Model
{
    class Bill
    {
        public string Id { get; set; }
        public int Fee { get; set; }
        public DateTime DateTime { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public Bill()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
