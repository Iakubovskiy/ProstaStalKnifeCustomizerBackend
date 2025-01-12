using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WorkshopBackend.Models
{
    public class OrderItem : IEntity
    {
        [BindNever]
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
    }
}
