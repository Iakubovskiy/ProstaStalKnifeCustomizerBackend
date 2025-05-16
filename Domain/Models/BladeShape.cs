using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Domain.Models
{
    public class BladeShape : IEntity
    {
        [BindNever]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? BladeShapePhotoUrl { get; set; }
        public double Price { get; set; }
        public double totalLength { get; set; }
        public double bladeLength { get; set; }
        public double bladeWidth { get; set; }
        public double bladeWeight { get; set; }
        public double sharpeningAngle { get; set; }
        public double? rockwellHardnessUnits { get; set; }        
        public string bladeShapeModelUrl { get; set; }
        public string sheathModelUrl { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
