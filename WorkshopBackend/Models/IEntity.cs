using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WorkshopBackend.Models
{
    public interface IEntity
    {
        [BindNever]
        Guid Id { get; set; }
    }
}
