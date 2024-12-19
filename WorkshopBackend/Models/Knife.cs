namespace WorkshopBackend.Models
{
    public class Knife
    {
        public int Id { get; set; }
        public BladeShape Shape { get; set; }
        public BladeCoating BladeCoating { get; set; }
        public BladeCoatingColor BladeCoatingColor { get; set; }
        public HandleColor HandleColor { get; set; }
        public SheathColor SheathColor { get; set; }
        public List<Fastening>? Fastening { get; set; }
        public List<Engraving>? Engravings { get; set; }
        public int Quantity { get; set; }
    }
}
