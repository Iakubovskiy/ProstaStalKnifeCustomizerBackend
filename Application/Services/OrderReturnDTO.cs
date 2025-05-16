using Domain.Models;

namespace Application.Services
{
    public class OrderReturnDTO
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public double Total { get; set; }
        public string Status { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public string ClientFullName { get; set; }
        public string ClientPhoneNumber { get; set; }
        public string CountryForDelivery { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string? Comment { get; set; }
        public List<OrderProductDto> Products { get; set; }
    }
}
