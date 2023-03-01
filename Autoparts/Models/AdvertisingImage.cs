using Autoparts.Models.Base;

namespace Autoparts.Models
{
    public class AdvertisingImage:BaseEntity
    {
        public string Image { get; set; }
        public int Count = 1;
    }
}
