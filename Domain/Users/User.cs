using Domain.Order.Support;
using Microsoft.AspNetCore.Identity;

namespace Domain.Users
{
    public class User : IdentityUser<Guid>, IEntity, IUpdatable<User>
    {
        protected User() {}

        public User(
            Guid id,
            string email,
            ClientData? userData
        )
        {
            this.Id = id;
            this.Email = email;
            this.UserName = email;
            this.UserData = userData;
        }

        public ClientData? UserData { get; private set; }

        public void Update(User entity)
        {
            this.UserData = entity.UserData;
            this.Email = entity.Email;
            this.UserName = entity.Email;
        }
    }

}
