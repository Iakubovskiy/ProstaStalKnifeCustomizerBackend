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
            BladeShapeType.BladeShapeType type,
            Translations name,
            string? bladeShapePhotoUrl,
            double price,
            BladeCharacteristics.BladeCharacteristics bladeCharacteristics,
            string bladeShapeModelUrl,
            string sheathModelUrl,
            bool isActive)
        {
            this.Id = id;
            Type = type;
            this.Name = name;
            this.BladeShapePhotoUrl = bladeShapePhotoUrl;
            this.Price = price;
            this.BladeCharacteristics = bladeCharacteristics;
            this.BladeShapeModelUrl = bladeShapeModelUrl;
            this.SheathModelUrl = sheathModelUrl;
            this.IsActive = isActive;
        }
        [BindNever]
        public Guid Id { get;  private set; }
        public BladeShapeType.BladeShapeType Type { get;  private set; }
        public Translations Name { get;  private set; }
        [MaxLength(255)]
        public string? BladeShapePhotoUrl { get;  private set; }
        public double Price { get;  private set; }
        public BladeCharacteristics.BladeCharacteristics BladeCharacteristics { get;  private set; }
        [MaxLength(255)]
        public string BladeShapeModelUrl { get;  private set; }
        [MaxLength(255)]
        public string SheathModelUrl { get;  private set; }

        public bool IsActive { get; set; }

        public double GetPrice()
        {
            return this.Price;
        }

        public void Update(BladeShape bladeShape)
        {
            
        }
    }
}
