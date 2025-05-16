using Domain.Models;

namespace Application.Services
{
    public class OrderDTO
    {
        public string Number { get; set; }
        public double Total { get; set; }
        public string Status { get; set; }
        public string ProductIdsJson { get; set; }
        public string ProductQuantitiesJson { get; set; }
        public Guid DeliveryTypeId { get; set; }
        public string ClientFullName { get; set; }
        public string ClientPhoneNumber { get; set; }
        public string CountryForDelivery { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string? Comment { get; set; }
    }
}
