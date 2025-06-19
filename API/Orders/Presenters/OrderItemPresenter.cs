using API.Components.Products;
using API.Components.Products.Attachments.Presenters;
using API.Components.Products.CompletedSheaths.Presenters;
using API.Components.Products.Knives.Presenter;
using Application.Components.Prices;
using Application.Currencies;
using Domain.Component.Product.Attachments;
using Domain.Component.Product.CompletedSheath;
using Domain.Component.Product.Knife;
using Domain.Orders;

namespace API.Orders.Presenters;

public class OrderItemPresenter
{
    public Guid ProductId { get; set; }
    public Guid OrderId { get; set; }
    public int Quantity { get; set; }
    public AbstractProductPresenter Product { get; set; }

    public static async Task<OrderItemPresenter> Present(
        OrderItem orderItem, 
        string locale, 
        string currency,
        IGetComponentPrice getComponentPriceService,
        IPriceService priceService)
    {
        var presenter = new OrderItemPresenter
        {
            OrderId = orderItem.Order.Id,
            ProductId = orderItem.Product.Id,
            Quantity = orderItem.Quantity
        };

        if (orderItem.Product is Knife knife)
        {
            presenter.Product = await KnifePresenter.Present(knife, locale, currency, getComponentPriceService);
        }
        else if (orderItem.Product is CompletedSheath completedSheath)
        {
            presenter.Product = await CompletedSheathPresenter.Present(completedSheath, locale, currency, getComponentPriceService, priceService);
        }
        else if (orderItem.Product is Attachment attachment)
        {
            presenter.Product = await AttachmentPresenter.Present(attachment, locale, currency, getComponentPriceService);
        }
        
        return presenter;
    }

    public static async Task<List<OrderItemPresenter>> PresentList(
        List<OrderItem> orderItems, 
        string locale, 
        string currency,
        IGetComponentPrice getComponentPriceService,
        IPriceService priceService)
    {
        var orderItemsPresenters = new List<OrderItemPresenter>();
        foreach (OrderItem orderItem in orderItems)
        {
            OrderItemPresenter orderItemPresenter = await Present(
                orderItem, 
                locale, 
                currency,
                getComponentPriceService,
                priceService
            );
            orderItemsPresenters.Add(orderItemPresenter);
        }
        
        return orderItemsPresenters;
    }
}