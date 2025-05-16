using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Domain.Models
{
    public class OrderStatuses : IEntity
    {
        [BindNever]
        public Guid Id { get; set; }
        public string Status { get; set; }
    }
}
