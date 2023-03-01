using Autoparts.Models.Base;

namespace Autoparts.Models
{
    public class IndexCategory:BaseEntity
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public int? SectionId { get; set; }
        public Section? Section { get; set; }
    }
}
