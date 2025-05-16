using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Domain.Models
{
    public class SheathColor : IEntity
    {
        [BindNever]
        public Guid Id { get; set; }
        public string Color { get; set; }
        public string ColorCode { get; set; }
        public string Material { get; set; }
        public double Price { get; set; }
        public string EngravingColorCode { get; set; }
        public bool IsActive { get; set; } = true;
        public string? ColorMapUrl { get; set; }
        public string? NormalMapUrl { get; set; }
        public string? RoughnessMapUrl { get; set; }
    }
}
