namespace WorkshopBackend.Models
{
    public class Knife : Product
    {
        public BladeShape Shape { get; set; }
        public BladeCoatingColor BladeCoatingColor { get; set; }
        public HandleColor HandleColor { get; set; }
        public SheathColor SheathColor { get; set; }
        public Fastening? Fastening { get; set; }
        public List<Engraving>? Engravings { get; set; }
    }
}
