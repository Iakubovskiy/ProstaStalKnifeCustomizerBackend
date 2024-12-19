namespace WorkshopBackend.Models
{
    public class BladeCoating
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MaterialUrl { get; set; }
        public double Price { get; set; }
        public List<BladeCoatingColor> Colors { get; set; }
    }
}
