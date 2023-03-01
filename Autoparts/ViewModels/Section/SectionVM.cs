using Autoparts.Models;
using Autoparts.Models.Base;

namespace Autoparts.ViewModels
{
    public class SectionVM:BaseEntity
    {
        public string Section1 { get; set; }
        public string? Section2 { get; set; }
        public string? Section3 { get; set; }
        public string? Section4 { get; set; }
    }
}
