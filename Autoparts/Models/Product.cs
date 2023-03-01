using Autoparts.Models.Base;

namespace Autoparts.Models
{
    public class Product:BaseEntity
    {
        public string CoverImage { get; set; }
        public string InnerImage { get; set; }
        public string OtherImage { get; set; }
        public string Name { get; set; }
        public double SellPrice { get; set; }
        public double DiscountPrice { get; set; }
        public string Title { get; set; }
        public int ColorId { get; set; }
        public ICollection<Color> Colors { get; set; }

    }
}
