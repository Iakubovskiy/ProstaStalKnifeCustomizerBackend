namespace Domain;

public interface IUpdatable<T> where T : class, IEntity
{
    public void Update(T entity);
}