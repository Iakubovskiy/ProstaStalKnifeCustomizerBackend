using API.Orders.Support.DeliveryTypes.Presenters;
using API.Orders.Support.PaymentMethods.Presenters;
using Application.Components.Prices;
using Application.Currencies;
using Domain.Orders;

namespace API.Orders.Presenters;

public class OrderPresenter
{
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

    public static async Task<OrderPresenter> Present(
        Order order, 
        string locale, 
        string currency,
        IPriceService priceService,
        IGetComponentPrice getComponentPriceService)
    {
        return new OrderPresenter
        {
            Id = order.Id,
            Number = order.Number,
            Total = order.Total,
            DeliveryType = await DeliveryTypePresenter.Present(order.DeliveryType, locale, currency, priceService),
            ClientFullName = order.ClientData.ClientFullName,
            ClientPhoneNumber = order.ClientData.ClientPhoneNumber,
            CountryForDelivery = order.ClientData.CountryForDelivery,
            City = order.ClientData.City,
            Address = order.ClientData.Address,
            ZipCode = order.ClientData.ZipCode,
            Email = order.ClientData.Email,
            Comment = order.Comment,
            Status = order.Status,
            PaymentMethod = await PaymentMethodPresenter.Present(order.PaymentMethod, locale),
            OrderItems = await OrderItemPresenter.PresentList(order.OrderItems, locale, currency, getComponentPriceService, priceService)
        };
    }

    public static async Task<List<OrderPresenter>> PresentList(
        List<Order> orders, 
        string locale, 
        string currency,
        IPriceService priceService,
        IGetComponentPrice getComponentPriceService)
    {
        var orderPresenters = new List<OrderPresenter>();
        foreach (var order in orders)
        {
            OrderPresenter orderPresenter = await Present(order, locale, currency, priceService, getComponentPriceService);
            orderPresenters.Add(orderPresenter);
        }
        
        return orderPresenters;
    }
}