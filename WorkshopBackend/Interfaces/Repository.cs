namespace WorkshopBackend.Interfaces
{
    public interface Repository<T,Tkey>
    {
        Task<List<T>> GetAll();
        Task<T> GetById(Tkey id);
        Task<T> Create(T newObject);
        Task<T> Update(Tkey id, T updatedObject);
        Task<bool> Delete(Tkey id);
    }
}
