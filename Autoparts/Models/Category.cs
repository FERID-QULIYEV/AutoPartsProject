using Autoparts.Models.Base;

namespace Autoparts.Models
{
    public class Category:BaseEntity
    {
        public string CoverImage { get; set; }
        public string InnerImage { get; set; }
        public string Name { get; set; }
        public double SellPrice { get; set; }
        public string Description { get; set; }
    }
}
