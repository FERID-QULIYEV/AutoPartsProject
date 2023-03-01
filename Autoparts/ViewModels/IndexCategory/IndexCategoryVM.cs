using Autoparts.Models;

namespace Autoparts.ViewModels
{
    public class IndexCategoryVM
    {
        public IFormFile Image { get; set; }
        public string Name { get; set; }
        public int? SectionId { get; set; }
        public Section? Section { get; set; }
    }
}
