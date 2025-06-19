using Domain.Orders.Support;

namespace API.Orders.Support.PaymentMethods.Presenters;

public class PaymentMethodPresenter
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Dictionary<string, string> Names { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }

    public static Task<PaymentMethodPresenter> Present(PaymentMethod paymentMethod, string locale)
    {
        var presenter = new PaymentMethodPresenter
        {
            Id = paymentMethod.Id,
            Name = paymentMethod.Name.GetTranslation(locale),
            Description = paymentMethod.Description.GetTranslation(locale),
            IsActive = paymentMethod.IsActive
        };
        
        return Task.FromResult(presenter);
    }
    
    public static async Task<PaymentMethodPresenter> PresentWithTranslations(PaymentMethod paymentMethod, string locale)
    {
        PaymentMethodPresenter presenter = await Present(paymentMethod, locale);
        presenter.Names = paymentMethod.Name.TranslationDictionary;
        return presenter;
    }

    public static async Task<List<PaymentMethodPresenter>> PresentList(List<PaymentMethod> paymentMethods, string locale)
    {
        var paymentMethodsPresenters = new List<PaymentMethodPresenter>();
        foreach (PaymentMethod paymentMethod in paymentMethods)
        {
            PaymentMethodPresenter paymentMethodPresenter = await Present(paymentMethod, locale);
            paymentMethodsPresenters.Add(paymentMethodPresenter);
        }
        return paymentMethodsPresenters;
    }
}