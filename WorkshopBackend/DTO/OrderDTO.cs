using WorkshopBackend.Models;

namespace WorkshopBackend.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public double Total { get; set; }
        public int StatusId { get; set; }
        public string KnivesIdsJson { get; set; }
        public int DeliveryTypeId { get; set; }
        public string ClientFullName { get; set; }
        public string ClientPhoneNumber { get; set; }
        public string CountryForDelivery { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string? Comment { get; set; }
    }
}
