using Domain.Order.Support;

namespace Domain.Users
{
    public class Admin : User
    {
        protected Admin() {}
        public Admin(
            Guid userId, 
            string email, 
            ClientData? clientData
        ) : base(userId, email, clientData)
        {
        }
    }
}
