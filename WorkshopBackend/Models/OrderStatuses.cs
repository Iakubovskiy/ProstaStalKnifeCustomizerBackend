using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WorkshopBackend.Models
{
    public class OrderStatuses : IEntity
    {
        [BindNever]
        public Guid Id { get; set; }
        public string Status { get; set; }
    }
}
