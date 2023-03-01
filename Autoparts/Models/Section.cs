using Autoparts.Models.Base;

namespace Autoparts.Models
{
    public class Section:BaseEntity
    {
        public string Section1 { get; set; }
        public string? Section2 { get; set; }
        public string? Section3 { get; set; }
        public string? Section4 { get; set; }
        public ICollection<IndexCategory>? IndexCategories { get; set; }
    }
}
