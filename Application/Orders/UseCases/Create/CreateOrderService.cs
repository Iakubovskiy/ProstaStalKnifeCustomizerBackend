using Application.Orders.Dto;
using Domain.Order;
using Domain.Users;
using Infrastructure.Orders;
using Infrastructure.Users;

namespace Application.Orders.UseCases.Create;

public class CreateOrderService : ICreateOrderService
{
    private readonly IOrderDtoMapper _orderDtoMapper;
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomEmailSender _emailSender;
    private readonly IAdminRepository _adminRepository;

    public CreateOrderService(
        IOrderDtoMapper orderDtoMapper,
        IOrderRepository orderRepository,
        ICustomEmailSender emailSender,
        IAdminRepository adminRepository
    )
    {
        this._orderDtoMapper = orderDtoMapper;
        this._orderRepository = orderRepository;
        this._emailSender = emailSender;
        this._adminRepository = adminRepository;
    }

    public async Task<Order> Create(OrderDto orderDto, string locale)
    {
        Order order = await this._orderDtoMapper.Map(orderDto);
        List<Task> emailTasks = new List<Task>();
        Order createdOrder = await this._orderRepository.Create(order);
        
        EmailDTO customerEmail = new EmailDTO();
        customerEmail.EmailTo = "diman.150106@gmail.com";
        string emailSubject;
        string emailBody;
        if (locale == "en")
        {
            emailSubject = $"New Order on Prostasta website № {order.Number}";
            emailBody = "Your order has been created but not payed yet, our manager will contact you and will provide payment data";
        }
        else
        {
            emailSubject = $"Нове замовлення на сайті Простасталь №{order.Number}";
            emailBody = "Ваше замовлення створено, менеджер звʼяжиться з вами для уточнення деталей";
        }
        customerEmail.EmailSubject = emailSubject;
        customerEmail.EmailBody = emailBody;
        emailTasks.Add(this._emailSender.SendEmailAsync(customerEmail));

        
        string adminEmailSubject;
        string adminEmailBody;
        if (locale == "en")
        {
            adminEmailSubject = $"New Order on Prostasta website № {order.Number}";
            adminEmailBody = $"Full name: {order.ClientData.ClientFullName} \n" +
                             $"Contact Email: {order.ClientData.Email} \n" +
                             $"Phone number: {order.ClientData.ClientPhoneNumber} \n" +
                             $"Address: {order.ClientData.Address} \n" +
                             $"Zip code: {order.ClientData.ZipCode} \n" +
                             $"City: {order.ClientData.City} \n" +
                             $"Country: {order.ClientData.CountryForDelivery}";
        }
        else
        {
            adminEmailSubject = $"Нове замовлення на сайті Простасталь №{order.Number}";
            adminEmailBody = $"ПІБ: {order.ClientData.ClientFullName} \n" +
                             $"Email: {order.ClientData.Email} \n" +
                             $"Номер телефону: {order.ClientData.ClientPhoneNumber} \n" +
                             $"Адреса доставки: {order.ClientData.Address} \n" +
                             $"Місто: {order.ClientData.City} \n";
        }
        
        foreach (Admin admin in await this._adminRepository.GetAdmins())
        {
            if (admin.Email == null)
            {
                continue;
            }
            EmailDTO adminEmail = new EmailDTO();
            adminEmail.EmailTo = "diman.150106@gmail.com";
            adminEmail.EmailSubject = adminEmailSubject;
            adminEmail.EmailBody = adminEmailBody;
        
            Task adminEmailTask = this._emailSender.SendEmailAsync(adminEmail);
            emailTasks.Add(adminEmailTask);
        }
        
        await Task.WhenAll(emailTasks);
        
        return createdOrder;
    }
}