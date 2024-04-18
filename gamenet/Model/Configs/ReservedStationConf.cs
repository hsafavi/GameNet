using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace gamenet.Model.Configs
{
    class ReservedStationConf : IEntityTypeConfiguration<ReservedStation>
    {
        public void Configure(EntityTypeBuilder<ReservedStation> builder)
        {

            builder.HasOne(r => r.Station).WithMany().HasForeignKey(r => r.StationId);

        }
    }
}
