using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WorkshopBackend.Models
{
    public class EngravingPrice : IEntity
    {
        [BindNever]
        public Guid Id { get; set; }
        public double Price { get; set; }
    }
}
