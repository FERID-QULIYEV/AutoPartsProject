using Autoparts.Models.Base;

namespace Autoparts.Models
{
    public class LastestProduct:BaseEntity
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public double NewSellPrice { get; set; }
        public double SellPrice { get; set; }
        public double? DiscountPrice { get; set; }
    }
}
