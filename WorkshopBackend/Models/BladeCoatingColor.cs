using Microsoft.EntityFrameworkCore;

namespace WorkshopBackend.Models
{
    [Index(nameof(ColorCode))]
    public class BladeCoatingColor
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public string ColorCode { get; set; }
        public string EngravingColorCode { get; set; }
    }
}
