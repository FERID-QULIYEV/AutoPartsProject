using Autoparts.Models.Base;

namespace Autoparts.Models
{
    public class Brand:BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
    }
}
