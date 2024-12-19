namespace WorkshopBackend.Models
{
    public class Fastening
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Material { get; set; }
        public string Color { get; set; }
        public string ColorCode { get; set; }
        public double price { get; set; }
        public string ModelUrl { get; set; }
    }
}
