using API.Components.Products;
using API.Components.Products.Attachments.Presenters;
using API.Components.Products.CompletedSheaths.Presenters;
using API.Components.Products.Knives.Presenter;
using Domain.Component.Product.Attachments;
using Domain.Component.Product.CompletedSheath;
using Domain.Component.Product.Knife;
using Domain.Orders;

namespace API.Orders.Presenters;

public class OrderItemPresenter
{
    private readonly KnifePresenter _knifePresenter;
    private readonly CompletedSheathPresenter _completedSheathPresenter;
    private readonly AttachmentPresenter _attachmentPresenter;

    public OrderItemPresenter(
        KnifePresenter knifePresenter,
        CompletedSheathPresenter completedSheathPresenter,
        AttachmentPresenter attachmentPresenter
    )
    {
        this._knifePresenter = knifePresenter;
        this._completedSheathPresenter = completedSheathPresenter;
        this._attachmentPresenter = attachmentPresenter;
    }
    
    public Guid ProductId { get; set; }
    public Guid OrderId { get; set; }
    public int Quantity { get; set; }
    public AbstractProductPresenter Product { get; set; }

    public async Task<OrderItemPresenter> Present(OrderItem orderItem, string locale, string currency)
    {
        this.OrderId = orderItem.Order.Id;
        this.ProductId = orderItem.Product.Id;
        this.Quantity = orderItem.Quantity;

        if (orderItem.Product is Knife knife)
        {
            this.Product = await this._knifePresenter.Present(knife, locale, currency);
        }
        else if (orderItem.Product is CompletedSheath completedSheath)
        {
            this.Product = await this._completedSheathPresenter.Present(completedSheath, locale, currency);
        }
        else if (orderItem.Product is Attachment attachment)
        {
            this.Product = await this._attachmentPresenter.Present(attachment, locale, currency);
        }
        
        return this;
    }

    public async Task<List<OrderItemPresenter>> PresentList(List<OrderItem> orderItems, string locale, string currency)
    {
        List<OrderItemPresenter> orderItemsPresenters = new List<OrderItemPresenter>();
        foreach (OrderItem orderItem in orderItems)
        {
            OrderItemPresenter orderItemPresenter =
                new OrderItemPresenter(this._knifePresenter,this._completedSheathPresenter, this._attachmentPresenter );
            await orderItemPresenter.Present(orderItem, locale, currency);
            orderItemsPresenters.Add(orderItemPresenter);
        }
        
        return orderItemsPresenters;
    }
}