using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Domain.Models
{
    public class EngravingPrice : IEntity
    {
        [BindNever]
        public Guid Id { get; set; }
        public double Price { get; set; }
    }
}
