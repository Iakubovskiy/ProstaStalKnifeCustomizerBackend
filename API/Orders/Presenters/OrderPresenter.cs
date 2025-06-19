using API.Orders.Support.DeliveryTypes.Presenters;
using API.Orders.Support.PaymentMethods.Presenters;
using Domain.Orders;

namespace API.Orders.Presenters;

public class OrderPresenter
{
    private readonly DeliveryTypePresenter _deliveryTypePresenter;
    private readonly PaymentMethodPresenter _paymentMethodPresenter;
    private readonly OrderItemPresenter _orderItemPresenter;

    public OrderPresenter(
        DeliveryTypePresenter deliveryTypePresenter,
        PaymentMethodPresenter paymentMethodPresenter,
        OrderItemPresenter orderItemPresenter
    )
    {
        this._deliveryTypePresenter = deliveryTypePresenter;
        this._paymentMethodPresenter = paymentMethodPresenter;
        this._orderItemPresenter = orderItemPresenter;
    }
    
    public Guid Id { get; set; }
    public int Number { get; set; }
    public double Total { get; set; }
    public DeliveryTypePresenter DeliveryType { get; set; }
    public string ClientFullName { get; set; }
    public string ClientPhoneNumber { get; set; }
    public string CountryForDelivery { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string? ZipCode { get; set; }
    public string Email { get; set; }
    public string? Comment { get; set; }
    public string Status { get; set; }
    public PaymentMethodPresenter PaymentMethod { get; set; }
    public List<OrderItemPresenter> OrderItems { get; set; }

    public async Task<OrderPresenter> Present(Order order, string locale, string currency)
    {
        this.Id = order.Id;
        this.Number = order.Number;
        this.Total = order.Total;
        this.DeliveryType = await this._deliveryTypePresenter.Present(order.DeliveryType, locale, currency);
        this.ClientFullName = order.ClientData.ClientFullName;
        this.ClientPhoneNumber = order.ClientData.ClientPhoneNumber;
        this.CountryForDelivery = order.ClientData.CountryForDelivery;
        this.City = order.ClientData.City;
        this.Address = order.ClientData.Address;
        this.ZipCode = order.ClientData.ZipCode;
        this.Email = order.ClientData.Email;
        this.Comment = order.Comment;
        this.Status = order.Status;
        this.PaymentMethod = await this._paymentMethodPresenter.Present(order.PaymentMethod, locale);
        
        this.OrderItems = await this._orderItemPresenter.PresentList(order.OrderItems, locale, currency);
        return this;
    }

    public async Task<List<OrderPresenter>> PresentList(List<Order> orders, string locale, string currency)
    {
        List<OrderPresenter> orderPresenters = new List<OrderPresenter>();
        foreach (var order in orders)
        {
            OrderPresenter orderPresenter = new OrderPresenter(
                this._deliveryTypePresenter, 
                this._paymentMethodPresenter,
                this._orderItemPresenter
            );
            await orderPresenter.Present(order, locale, currency);
            orderPresenters.Add(orderPresenter);
        }
        
        return orderPresenters;
    }
}