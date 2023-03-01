namespace Autoparts.ViewModels
{
    public class BlogTableVM
    {
        public DateTime Date { get; set; }
        public IFormFile Image { get; set; }
        public string DateName { get; set; }
        public string PrimaryTitle { get; set; }
        public string SecondaryTitle { get; set; }
    }
}
