using Autoparts.Models.Base;

namespace Autoparts.Models
{
    public class AvailableOption:BaseEntity
    {
        public int ColorId { get; set; }
        public Color? Color { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
