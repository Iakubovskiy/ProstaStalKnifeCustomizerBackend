using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Domain.Models
{
    public class DeliveryType : IEntity
    {
        [BindNever]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string? Comment { get; set; }
        public bool IsActive { get; set; }
    }
}
