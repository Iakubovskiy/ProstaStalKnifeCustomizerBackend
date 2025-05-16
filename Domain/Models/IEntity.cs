using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Domain.Models
{
    public interface IEntity
    {
        [BindNever]
        Guid Id { get; set; }
    }
}
