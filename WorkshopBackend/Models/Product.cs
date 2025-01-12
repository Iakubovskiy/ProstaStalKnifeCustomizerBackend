using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WorkshopBackend.Models
{
    public class Product : IEntity
    {
        [BindNever]
        public Guid Id { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
