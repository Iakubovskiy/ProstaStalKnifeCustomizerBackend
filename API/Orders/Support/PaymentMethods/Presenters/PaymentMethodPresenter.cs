using Domain.Orders.Support;

namespace API.Orders.Support.PaymentMethods.Presenters;

public class PaymentMethodPresenter
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }

    public async Task<PaymentMethodPresenter> Present(PaymentMethod paymentMethod, string locale)
    {
        this.Id = paymentMethod.Id;
        this.Name = paymentMethod.Name.GetTranslation(locale);
        this.Description = paymentMethod.Description.GetTranslation(locale);
        this.IsActive = paymentMethod.IsActive;
        
        return this;
    }

    public async Task<List<PaymentMethodPresenter>> PresentList(List<PaymentMethod> paymentMethods, string locale)
    {
        List<PaymentMethodPresenter> paymentMethodsPresenters = new List<PaymentMethodPresenter>();
        foreach (PaymentMethod paymentMethod in paymentMethods)
        {
            PaymentMethodPresenter paymentMethodPresenter = new PaymentMethodPresenter();
            await paymentMethodPresenter.Present(paymentMethod, locale);
            paymentMethodsPresenters.Add(paymentMethodPresenter);
        }
        return paymentMethodsPresenters;
    }
}