using Domain.Order.Suppport.Vlaidators;
using Microsoft.EntityFrameworkCore;

namespace Domain.Order.Suppport;

[Owned]
public class ClientData
{
    private ClientData()
    {
        
    }

    public ClientData(
        string clientFullName, 
        string clientPhoneNumber, 
        string countryForDelivery, 
        string city,
        string email
    )
    {
        ClientDetailsValidator.Validate(
            clientFullName, 
            clientPhoneNumber, 
            countryForDelivery, 
            city, 
            email
        );
        this.ClientFullName = clientFullName;
        this.ClientPhoneNumber = clientPhoneNumber;
        this.CountryForDelivery = countryForDelivery;
        this.City = city;
        this.Email = email;
    }
    
    public string ClientFullName { get; private set; }
    public string ClientPhoneNumber { get; private set; }
    public string CountryForDelivery { get; private set; }
    public string City { get; private set; }
    public string Email { get; private set; }
    
}