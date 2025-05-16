using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models
{
    [Index(nameof(ColorCode))]
    public class BladeCoatingColor : IEntity
    {
        [BindNever]
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public string? ColorCode { get; set; }
        public string EngravingColorCode { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; } = true;
        public string? ColorMapUrl { get; set; }
        public string? NormalMapUrl { get; set; }
        public string? RoughnessMapUrl { get; set; }
    }
}
