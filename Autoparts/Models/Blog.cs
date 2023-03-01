using Autoparts.Models.Base;

namespace Autoparts.Models
{
    public class Blog:BaseEntity
    {
        public string Image { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
    }
}
