using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Domain
{
    public interface IEntity
    {
        [BindNever]
        Guid Id { get; }
    }
}
