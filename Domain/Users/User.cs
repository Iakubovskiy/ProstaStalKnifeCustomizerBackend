using Domain.Orders.Support;
using OrderEntity = Domain.Orders.Order;
using Microsoft.AspNetCore.Identity;

namespace Domain.Users
{
    public class User : IdentityUser<Guid>, IEntity, IUpdatable<User>
    {
        protected User() {}

        public User(
            Guid id,
            string email,
            ClientData? userData = null
        )
        {
            this.Id = id;
            this.Email = email;
            this.UserName = email;
            this.UserData = userData;
        }

        public ClientData? UserData { get; private set; }
        public List<OrderEntity> Orders { get; private set; } = new List<OrderEntity>();

        public void Update(User entity)
        {
            this.UserData = entity.UserData;
            this.Email = entity.Email;
            this.UserName = entity.Email;
        }

        public void AddOrder(OrderEntity order)
        {
            this.Orders.Add(order);
        }

        public bool Equals(User other)
        {
            if (other is null) return false;
            return this.Id == other.Id;
        }

        public static bool operator == (User first, User second)
        {
            return first.Equals(second);
        }
    
        public static bool operator != (User first, User second)
        {
            return !first.Equals(second);
        }
    }

}
