using Domain;

namespace Infrastructure
{
    public interface IRepository<T> where T : class, IEntity, IUpdatable<T>
    {
        IQueryable<T> List();
        Task<List<T>> GetAll();
        Task<T> GetById(Guid id);
        Task<T> Create(T newObject);
        Task<T> Update(Guid id, T updatedObject);
        Task<bool> Delete(Guid id);
    }
}
