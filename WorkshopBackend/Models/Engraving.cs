using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WorkshopBackend.Models
{
    public class Engraving : IEntity
    {
        [BindNever]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int Side { get; set; }
        public string? Text { get; set; }
        public string? Font { get; set; }
        public string? pictureUrl { get; set; }
        public double rotationX { get; set; }
        public double rotationY { get; set; }
        public double rotationZ { get; set; }
        public double locationX { get; set; }
        public double locationY { get; set; }
        public double locationZ { get; set; }
        public double scaleX { get; set; }
        public double scaleY { get; set; }
        public double scaleZ { get; set; }
    }
}
