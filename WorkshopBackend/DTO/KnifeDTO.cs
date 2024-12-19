using WorkshopBackend.Models;

namespace WorkshopBackend.DTO
{
    public class KnifeDTO
    {
        public int Id { get; set; }
        public int ShapeId { get; set; }
        public int BladeCoatingId { get; set; }
        public int BladeCoatingColorId { get; set; }
        public int HandleColorId { get; set; }
        public int SheathColorId { get; set; }
        public string? FasteningJson { get; set; }
        public string? EngravingsJson { get; set; }
        public int Quantity { get; set; }
    }
}
