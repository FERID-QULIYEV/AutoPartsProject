using Autoparts.Models.Base;

namespace Autoparts.Models
{
    public class AdvertisingVideo:BaseEntity
    {
        public string Video { get; set; }
        public string VideoUrl { get; set; }
        public int Count { get; set; }
    }
}
