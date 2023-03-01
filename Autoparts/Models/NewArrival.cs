using Autoparts.Models.Base;

namespace Autoparts.Models
{
    public class NewArrival:BaseEntity
    {
        public string CoverImage { get; set; }
        public string InnerImage { get; set; }
        public string Name { get; set; }
        public double NewSellPrice {get; set;}
        public double SellPrice { get; set; }
        public double? DiscountPrice { get; set; }

    }
}
