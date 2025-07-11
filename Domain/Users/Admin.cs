﻿using Domain.Orders.Support;

namespace Domain.Users
{
    public class Admin : User
    {
        protected Admin() {}
        public Admin(
            Guid userId, 
            string email, 
            ClientData? clientData = null
        ) : base(userId, email, clientData)
        {
        }
    }
}
