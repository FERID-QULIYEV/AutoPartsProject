namespace Autoparts.ViewModels
{
    public class SliderVM
    {
        public string PrimaryTitle { get; set; }
        public string SecondaryTitle { get; set; }
        public string Description { get; set; }
        public IFormFile ImageFile { get; set; }
        public int Order { get; set; }
    }
}
