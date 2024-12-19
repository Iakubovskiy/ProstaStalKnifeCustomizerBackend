namespace WorkshopBackend.Models
{
    public class SheathColor
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public string ColorCode { get; set; }
        public string Material { get; set; }
        public string? MaterialUrl { get; set; }
        public double Price { get; set; }
    }
}
