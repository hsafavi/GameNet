using System;
using System.ComponentModel.DataAnnotations;

namespace gamenet.Model
{
    public class ReservedStation
    {
        public string Id { get; set; }
        [Required]
        public string StationId { get; set; }
        public Station Station { get; set; }

        public string Description { get; set; }

        public DateTime DateTime { get; set; }
        public byte ToHour { get; set; }
        public byte ToMinute { get; set; }
        public ReservedStation()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
