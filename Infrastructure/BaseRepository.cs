using System.Data.Entity.Core;
using Domain;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class BaseRepository<TEntity> : IRepository <TEntity>
where TEntity : class, IEntity, IUpdatable<TEntity>
{
    protected readonly DBContext Context;
    protected readonly DbSet<TEntity> Set;
    
    public BaseRepository(DBContext context)
    {
        this.Context = context;
        this.Set = context.Set<TEntity>();
    }
    
    public virtual IQueryable<TEntity> List()
    {
        return Set;
    }

    public virtual async Task<List<TEntity>> GetAll()
    {
        return await Set.ToListAsync();
    }
    
    public virtual async Task<TEntity> GetById (Guid id)
    {
        return await Set.FindAsync(id) ?? throw new ObjectNotFoundException($"Entity not found {nameof(TEntity)}");
    }

    public virtual async Task<TEntity> Create(TEntity entity)
    {
        await Set.AddAsync(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<TEntity> Update(Guid id, TEntity entity)
    {
        TEntity existingEntity = await this.GetById(id);
        existingEntity.Update(entity);
        await Context.SaveChangesAsync();
        return existingEntity;
    }

    public virtual async Task<bool> Delete(Guid id)
    {
        TEntity entity = await this.GetById(id);
        Set.Remove(entity);
        await Context.SaveChangesAsync();
        return true;
    }
    
}