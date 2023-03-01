using Autoparts.Models.Base;

namespace Autoparts.Models
{
    public class Slider:BaseEntity
    {
        public string PrimaryTitle { get; set; }
        public string SecondaryTitle { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Order { get; set; }
    }
}
