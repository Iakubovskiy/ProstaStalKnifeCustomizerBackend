namespace Domain.Interfaces
{
    public interface IRepository<T,TKey>
    {
        Task<List<T>> GetAll();
        Task<T> GetById(TKey id);
        Task<T> Create(T newObject);
        Task<T> Update(TKey id, T updatedObject);
        Task<bool> Delete(TKey id);
    }
}
