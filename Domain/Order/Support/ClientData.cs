using System.ComponentModel.DataAnnotations;
using Domain.Order.Support.Validators;
using Microsoft.EntityFrameworkCore;

namespace Domain.Order.Support;

[Owned]
public class ClientData
{
    public ClientData(
        string clientFullName, 
        string clientPhoneNumber, 
        string countryForDelivery, 
        string city,
        string email, 
        string address, 
        string? zipCode
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
        this.Address = address;
        this.ZipCode = zipCode;
    }
    
    [MaxLength(150)]
    public string ClientFullName { get; private set; }
    [MaxLength(20)]
    public string ClientPhoneNumber { get; private set; }
    [MaxLength(70)]
    public string CountryForDelivery { get; private set; }
    [MaxLength(70)]
    public string City { get; private set; }
    [MaxLength(100)]
    public string Email { get; private set; }
    [MaxLength(50)]
    public string Address { get; private set; }
    [MaxLength(15)]
    public string? ZipCode { get; private set; }
    
}