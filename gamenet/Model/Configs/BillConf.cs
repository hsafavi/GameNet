using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace gamenet.Model.Configs
{
    class BillConf : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.HasOne(b => b.User).WithMany().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
