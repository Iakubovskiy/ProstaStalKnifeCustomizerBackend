using System.Data.Entity;
using Domain;
using Domain.Component;
using Infrastructure.Data;

namespace Infrastructure.Components;

public class ComponentRepository<T> : BaseRepository<T>, IComponentRepository<T> 
where T : class, IEntity, IUpdatable<T>, IComponent
{
    public ComponentRepository(DBContext context)
    : base(context)
    {
        
    }
    
    public virtual async Task<List<T>> GetAllActive()
    {
        return await this.Set.Where(component => component.IsActive).ToListAsync();
    }
}