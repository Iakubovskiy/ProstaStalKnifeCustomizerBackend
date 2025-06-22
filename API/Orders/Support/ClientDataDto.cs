namespace API.Orders.Support;

public class ClientDataDto
{
    public string ClientFullName { get; set; }
    public string ClientPhoneNumber { get; set; }
    public string CountryForDelivery { get; set; }
    public string City { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string? ZipCode { get; set; }
}
