using Autoparts.Models.Base;

namespace Autoparts.ViewModels
{
    public class UpdateProductVM:BaseEntity
    {
        public IFormFile CoverImage { get; set; }
        public IFormFile InnerImage { get; set; }
        public IFormFile OtherImage { get; set; }
        public string Name { get; set; }
        public double SellPrice { get; set; }
        public double DiscountPrice { get; set; }
        public string Title { get; set; }
        public int ColorId { get; set; }
    }
}
