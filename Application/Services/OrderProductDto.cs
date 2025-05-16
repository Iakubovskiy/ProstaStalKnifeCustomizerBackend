namespace Application.Services
{
    public class OrderProductDto
    {
        public Guid ProductId { get; set; }
        public string ProductType { get; set; }
        public int Quantity { get; set; }
    }
}
