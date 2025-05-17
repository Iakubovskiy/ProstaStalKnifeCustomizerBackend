using System.ComponentModel.DataAnnotations;
using Domain.Component.Translation;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Domain.Component.BladeShape
{
    public class BladeShape : IEntity, IComponent
    {
        private BladeShape()
        {
            
        }
        public BladeShape(
            Guid id,
            Translations name,
            string? bladeShapePhotoUrl,
            double price,
            double totalLength,
            double bladeLength,
            double bladeWidth,
            double bladeWeight,
            double sharpeningAngle,
            double rockwellHardnessUnits,
            string bladeShapeModelUrl,
            string sheathModelUrl,
            bool isActive
        )
        {
            this.Id = id;
            this.Name = name;
            this.BladeShapePhotoUrl = bladeShapePhotoUrl;
            this.Price = price;
            this.TotalLength = totalLength;
            this.BladeLength = bladeLength;
            this.BladeWidth = bladeWidth;
            this.BladeWeight = bladeWeight;
            this.SharpeningAngle = sharpeningAngle;
            this.RockwellHardnessUnits = rockwellHardnessUnits;
            this.BladeShapeModelUrl = bladeShapeModelUrl;
            this.SheathModelUrl = sheathModelUrl;
            this.IsActive = isActive;
        }
        [BindNever]
        public Guid Id { get;  private set; }
        public Translations Name { get;  private set; }
        [MaxLength(255)]
        public string? BladeShapePhotoUrl { get;  private set; }
        public double Price { get;  private set; }
        public double TotalLength { get;  private set; }
        public double BladeLength { get;  private set; }
        public double BladeWidth { get;  private set; }
        public double BladeWeight { get;  private set; }
        public double SharpeningAngle { get;  private set; }
        public double RockwellHardnessUnits { get;  private set; }    
        [MaxLength(255)]
        public string BladeShapeModelUrl { get;  private set; }
        [MaxLength(255)]
        public string SheathModelUrl { get;  private set; }
        public bool IsActive { get;  private set; }
        
        public double GetPrice()
        {
            return this.Price;
        }

        public void Update(BladeShape bladeShape)
        {
            
        }
    }
}
