using System.ComponentModel.DataAnnotations;

namespace gamenet.Model
{
    class Settings
    {
        [Key]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
