using gamenet.Model.Configs;
using Microsoft.EntityFrameworkCore;

namespace gamenet.Model
{
    class Core : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<ReservedStation> ReservedStations { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Settings> Settings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConf());
            modelBuilder.ApplyConfiguration(new BillConf());

            modelBuilder.ApplyConfiguration(new StationConf());
            //modelBuilder.ApplyConfiguration(new ReservedStationConf());

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=data.data");

        }
    }
}
