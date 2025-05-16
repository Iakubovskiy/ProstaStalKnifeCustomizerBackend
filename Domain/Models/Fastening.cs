namespace Domain.Models
{
    public class Fastening : Product
    {
        public string Name { get; set; }
        public string Material { get; set; }
        public string Color { get; set; }
        public string ColorCode { get; set; }
        public double Price { get; set; }
        public string ModelUrl { get; set; }
    }
}
