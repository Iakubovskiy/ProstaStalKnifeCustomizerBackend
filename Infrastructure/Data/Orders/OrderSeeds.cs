using Domain.Component.Product;
using Domain.Order;
using Domain.Order.Support;
using Infrastructure.Components.Products;
using Infrastructure.Orders;

namespace Infrastructure.Data.Orders;

public class OrderSeeder : ISeeder
{
    public int Priority => 6;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IRepository<DeliveryType> _deliveryTypeRepository;
    private readonly IRepository<PaymentMethod> _paymentMethodRepository;

    public OrderSeeder(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IRepository<DeliveryType> deliveryTypeRepository,
        IRepository<PaymentMethod> paymentMethodRepository
    )
    {
        this._orderRepository = orderRepository;
        this._productRepository = productRepository;
        this._deliveryTypeRepository = deliveryTypeRepository;
        this._paymentMethodRepository = paymentMethodRepository;
    }

    public async Task SeedAsync()
    {
        if ((await _orderRepository.GetAll()).Any())
        {
            return;
        }
        
        var productIds = new List<Guid>
        {
            new Guid("11111111-aaaa-4aaa-aaaa-aaaaaaaaaaaa"),
            new Guid("4a7e35ec-57fa-4efb-bba6-3ef27ed4d168"),
            new Guid("77777777-a1a1-4a1a-a1a1-a1a1a1a1a1a1"),
            new Guid("ca047152-c3f2-4747-8fa6-caf3b73cd693"),
            new Guid("e1b84a52-6e7e-42ab-979f-abcb41f3bd92"),
            new Guid("67b58778-cded-46e0-91e6-a3abf1b8970e"),
            new Guid("5e4efd9a-ef73-47bc-8a00-ab71f2e1ea34"),
            new Guid("b174742f-29be-4476-8cb2-20e860a19e8a"),
            new Guid("85a3330f-a882-4ed3-beb5-75501d48b9e7") 
        };

        var products = (await _productRepository.GetProductsByIds(productIds)).ToDictionary(p => p.Id);

        var deliveryNovaPoshta = await _deliveryTypeRepository.GetById(new Guid("b6d15e84-3d69-4b74-b270-cb5e9fd0d2d3"));
        var deliveryCourier = await _deliveryTypeRepository.GetById(new Guid("79beccad-8373-4f94-935d-43dd8d97975c"));
        var deliverySelfPickup = await _deliveryTypeRepository.GetById(new Guid("3fc6562b-4e9b-4530-9895-d01de959bafc"));

        var paymentCash = await _paymentMethodRepository.GetById(new Guid("a1e7b8c9-d0f1-4a2b-8c3d-4e5f6a7b8c9d"));
        var paymentCard = await _paymentMethodRepository.GetById(new Guid("b2f8c9d0-e1f2-4b3c-9d4e-5f6a7b8c9d0e"));
        
        if (products.Count != productIds.Count || deliveryNovaPoshta == null || deliveryCourier == null || 
            deliverySelfPickup == null || paymentCash == null || paymentCard == null)
        {
            return;
        }

        var client1 = new ClientData("Іван Петренко", "+380991234567", "Україна", "Київ", "ivan.p@example.com", "вул. Хрещатик, 1, кв. 5", "01001");
        var products1 = new List<Product> { products[new Guid("ca047152-c3f2-4747-8fa6-caf3b73cd693")], products[new Guid("67b58778-cded-46e0-91e6-a3abf1b8970e")] };
        var order1 = new Order(new Guid("4b80179b-478f-48ca-b8af-9c8f6545f6b9"), 1001, products1.Sum(p => p.GetPrice(1.0)), products1, deliveryNovaPoshta, client1, "Будь ласка, зателефонуйте перед доставкою.", OrderStatuses.New.ToString(), paymentCard);
        
        var client2 = new ClientData("Марія Коваленко", "+380679876543", "Україна", "Львів", "maria.k@example.com", "пл. Ринок, 10", "79008");
        var products2 = new List<Product> { products[new Guid("e1b84a52-6e7e-42ab-979f-abcb41f3bd92")] };
        var order2 = new Order(new Guid("5eedab0a-6291-4b9e-a3a3-0db4ddb3ba37"), 1002, products2.Sum(p => p.GetPrice(1.0)), products2, deliveryCourier, client2, null, OrderStatuses.Completed.ToString(), paymentCard);
        
        var client3 = new ClientData("Олександр Мельник", "+380501112233", "Україна", "Одеса", "alex.m@example.com", "вул. Дерибасівська, 22", "65026");
        var products3 = new List<Product> { products[new Guid("b174742f-29be-4476-8cb2-20e860a19e8a")], products[new Guid("5e4efd9a-ef73-47bc-8a00-ab71f2e1ea34")] };
        var order3 = new Order(new Guid("55500c78-0d73-4655-b299-1c624f90a452"), 1003, products3.Sum(p => p.GetPrice(1.0)), products3, deliveryNovaPoshta, client3, "Не турбувати до 11:00.", OrderStatuses.Pending.ToString(), paymentCash);
        
        var client4 = new ClientData("Анна Шевченко", "+380934445566", "Україна", "Харків", "anna.s@example.com", "пр. Науки, 14", "61022");
        var products4 = new List<Product> { products[new Guid("11111111-aaaa-4aaa-aaaa-aaaaaaaaaaaa")], products[new Guid("77777777-a1a1-4a1a-a1a1-a1a1a1a1a1a1")] };
        var order4 = new Order(new Guid("f93254eb-d5a3-4d8e-a0fc-a7eba734bba8"), 1004, products4.Sum(p => p.GetPrice(1.0)), products4, deliverySelfPickup, client4, "Заберу завтра.", OrderStatuses.New.ToString(), paymentCash);

        var client5 = new ClientData("Дмитро Бондаренко", "+380687778899", "Україна", "Дніпро", "dmytro.b@example.com", "вул. Яворницького, 55", "49000");
        var products5 = new List<Product> { products[new Guid("85a3330f-a882-4ed3-beb5-75501d48b9e7")] };
        var order5 = new Order(new Guid("250e6c1c-207e-41bd-9f30-e70f1f3fe0f9"), 1005, products5.Sum(p => p.GetPrice(1.0)), products5, deliveryCourier, client5, null, OrderStatuses.Canceled.ToString(), paymentCard);
        
        var client6 = new ClientData("Юлія Ткаченко", "+380976543210", "Україна", "Запоріжжя", "yulia.t@example.com", "пр. Соборний, 150", "69000");
        var products6 = new List<Product> { products[new Guid("4a7e35ec-57fa-4efb-bba6-3ef27ed4d168")] };
        var order6 = new Order(new Guid("a5f340e8-f72f-4d55-b36a-680f38fe8645"), 1006, products6.Sum(p => p.GetPrice(1.0)), products6, deliveryNovaPoshta, client6, "Подарункова упаковка, будь ласка.", OrderStatuses.Completed.ToString(), paymentCard);
        
        var client7 = new ClientData("Сергій Ковальчук", "+380665554433", "Україна", "Вінниця", "serhii.k@example.com", "вул. Соборна, 50", "21000");
        var products7 = new List<Product> { products[new Guid("67b58778-cded-46e0-91e6-a3abf1b8970e")] };
        var order7 = new Order(new Guid("78052ebc-0ff7-40db-bc0f-d9d31f16bf20"), 1007, products7.Sum(p => p.GetPrice(1.0)), products7, deliveryCourier, client7, null, OrderStatuses.New.ToString(), paymentCash);

        var client8 = new ClientData("Наталія Кравченко", "+380981212121", "Україна", "Полтава", "natalia.k@example.com", "вул. Жовтнева, 29", "36000");
        var products8 = new List<Product> { products[new Guid("e1b84a52-6e7e-42ab-979f-abcb41f3bd92")] };
        var order8 = new Order(new Guid("ef69453f-dcf6-4682-ae9b-41bfe383d218"), 1008, products8.Sum(p => p.GetPrice(1.0)), products8, deliverySelfPickup, client8, null, OrderStatuses.Pending.ToString(), paymentCard);

        var client9 = new ClientData("Андрій Поліщук", "+380633232323", "Україна", "Чернігів", "andrii.p@example.com", "пр. Миру, 20", "14000");
        var products9 = new List<Product> { products[new Guid("ca047152-c3f2-4747-8fa6-caf3b73cd693")] };
        var order9 = new Order(new Guid("1049b9ef-5ad6-4524-8e30-0b1e4fd4ff63"), 1009, products9.Sum(p => p.GetPrice(1.0)), products9, deliveryNovaPoshta, client9, "Пакування має бути надійним.", OrderStatuses.Completed.ToString(), paymentCash);

        var client10 = new ClientData("Оксана Лисенко", "+380954343434", "Україна", "Київ", "oksana.l@example.com", "бул. Лесі Українки, 26", "01133");
        var products10 = new List<Product> { products[new Guid("b174742f-29be-4476-8cb2-20e860a19e8a")], products[new Guid("85a3330f-a882-4ed3-beb5-75501d48b9e7")] };
        var order10 = new Order(new Guid("d8537db5-ddc2-4d4e-b9e6-1afe816c6ee2"), 1010, products10.Sum(p => p.GetPrice(1.0)), products10, deliveryCourier, client10, "Доставка після 18:00.", OrderStatuses.New.ToString(), paymentCard);
        
        await _orderRepository.Create(order1);
        await _orderRepository.Create(order2);
        await _orderRepository.Create(order3);
        await _orderRepository.Create(order4);
        await _orderRepository.Create(order5);
        await _orderRepository.Create(order6);
        await _orderRepository.Create(order7);
        await _orderRepository.Create(order8);
        await _orderRepository.Create(order9);
        await _orderRepository.Create(order10);
    }
}