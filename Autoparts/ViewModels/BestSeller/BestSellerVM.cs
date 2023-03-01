namespace Autoparts.ViewModels
{
    public class BestSellerVM
    {
        public IFormFile CoverImage { get; set; }
        public IFormFile InnerImage { get; set; }
        public string Name { get; set; }
        public double NewSellPrice { get; set; }
        public double SellPrice{ get; set; }
        public double? DiscountPrice { get; set; }
    }
}
