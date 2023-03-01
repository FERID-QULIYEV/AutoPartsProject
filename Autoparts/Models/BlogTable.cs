using Autoparts.Models.Base;

namespace Autoparts.Models
{
    public class BlogTable:BaseEntity
    {

        public DateTime Date { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string PrimaryTitle { get; set; }
        public string SecondaryTitle { get; set; }
    }
}
