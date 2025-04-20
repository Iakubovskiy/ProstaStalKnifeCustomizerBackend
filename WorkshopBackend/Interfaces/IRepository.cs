namespace WorkshopBackend.Interfaces
{
    public interface IRepository<T,TKey>
    {
        Task<List<T>> GetAll();
        Task<T> GetById(TKey id);
        Task<T> Create(T order);
        Task<T> Update(TKey id, T updatedObject);
        Task<bool> Delete(TKey id);
    }
}
