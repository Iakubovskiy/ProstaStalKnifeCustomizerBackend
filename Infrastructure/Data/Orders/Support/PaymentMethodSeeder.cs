using Domain.Order.Support;
using Domain.Translation;

namespace Infrastructure.Data.Orders.Support;

public class PaymentMethodSeeder : ISeeder
{
    public int Priority => 0;
    private readonly IRepository<PaymentMethod> _paymentMethodRepository;

    public PaymentMethodSeeder(IRepository<PaymentMethod> paymentMethodRepository)
    {
        this._paymentMethodRepository = paymentMethodRepository;
    }

    public async Task SeedAsync()
    {
        int count = (await this._paymentMethodRepository.GetAll()).Count;
        if (count > 0)
        {
            return;
        }

        PaymentMethod paymentMethod1 = new PaymentMethod(
            new Guid("a1e7b8c9-d0f1-4a2b-8c3d-4e5f6a7b8c9d"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Cash on Delivery" },
                { "ua", "Оплата при отриманні" },
            }),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Pay with cash upon receiving your order" },
                { "ua", "Оплатіть готівкою при отриманні замовлення" },
            }),
            true
        );

        PaymentMethod paymentMethod2 = new PaymentMethod(
            new Guid("b2f8c9d0-e1f2-4b3c-9d4e-5f6a7b8c9d0e"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Card Payment Online" },
                { "ua", "Оплата карткою онлайн" },
            }),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Pay with your credit or debit card via our secure gateway" },
                { "ua", "Оплатіть кредитною або дебетовою карткою через наш безпечний шлюз" },
            }),
            true
        );

        PaymentMethod paymentMethod3 = new PaymentMethod(
            new Guid("c3a9d0e1-f2a3-4c4d-ae5f-6a7b8c9d0e1f"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Bank Transfer" },
                { "ua", "Банківський переказ" },
            }),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Transfer funds directly to our bank account. Details will be provided after checkout." },
                { "ua", "Перекажіть кошти безпосередньо на наш банківський рахунок. Реквізити будуть надані після оформлення замовлення." },
            }),
            true
        );

        PaymentMethod paymentMethod4 = new PaymentMethod(
            new Guid("d4b0e1f2-a3b4-4d5e-bf6a-7b8c9d0e1f2a"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "PayPal" },
                { "ua", "PayPal" },
            }),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Pay securely using your PayPal account" },
                { "ua", "Сплачуйте безпечно за допомогою вашого облікового запису PayPal" },
            }),
            false
        );
        
        PaymentMethod paymentMethod5 = new PaymentMethod(
            new Guid("e5c1f2a3-b4c5-4e6f-c07b-8c9d0e1f2a3b"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Google Pay / Apple Pay" },
                { "ua", "Google Pay / Apple Pay" }
            }),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Fast and secure payment using your mobile device" },
                { "ua", "Швидка та безпечна оплата за допомогою вашого мобільного пристрою" }
            }),
            true
        );

        await this._paymentMethodRepository.Create(paymentMethod1);
        await this._paymentMethodRepository.Create(paymentMethod2);
        await this._paymentMethodRepository.Create(paymentMethod3);
        await this._paymentMethodRepository.Create(paymentMethod4);
        await this._paymentMethodRepository.Create(paymentMethod5);
    }
}