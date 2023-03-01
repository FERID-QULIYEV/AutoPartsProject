namespace Autoparts.ViewModels
{
    public class LastestProductVM
    {
        public IFormFile Image { get; set; }
        public string Name { get; set; }
        public double NewSellPrice { get; set; }
        public double SellPrice { get; set; }
        public double? DiscountPrice { get; set; }
    }
}
