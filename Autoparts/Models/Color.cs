using Autoparts.Models.Base;

namespace Autoparts.Models
{
    public class Color:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<AvailableOption>? AvailableOptions{ get; set; }
    }
}
