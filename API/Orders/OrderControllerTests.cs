using System.Net;
using System.Text;
using Application.Orders.Dto;
using Domain.Orders;
using Domain.Orders.Support;
using Domain.Orders;
using FluentAssertions;
using Xunit;

namespace API.Orders;

public class OrderControllerTests : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client;

    private class CreateOrderResponse { public Order NewOrder { get; set; } }
    private class DeleteOrderResponse { public bool IsDeleted { get; set; } }

    public OrderControllerTests(CustomWebAppFactory factory)
    {
        this._client = factory.CreateClient();
    }
    
    private static readonly Guid ExistingOrderId = new("4b80179b-478f-48ca-b8af-9c8f6545f6b9");
    private static readonly Guid OrderToDeleteId = new("5eedab0a-6291-4b9e-a3a3-0db4ddb3ba37");
    private static readonly Guid NonExistentOrderId = Guid.NewGuid();

    [Fact]
    public async Task GetAllOrders_ShouldReturnOkAndListOfOrders()
    {
        var response = await this._client.GetAsync("/api/orders");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var orders = await response.Content.ReadFromJsonAsync<List<OrderDto>>();
        orders.Should().NotBeNull();
        orders.Should().HaveCount(10);
    }

    [Fact]
    public async Task GetOrderById_WithExistingId_ShouldReturnOkAndOrder()
    {
        var response = await this._client.GetAsync($"/api/orders/{ExistingOrderId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var order = await response.Content.ReadFromJsonAsync<OrderDto>();
        order.Should().NotBeNull();
        order.Id.Should().Be(ExistingOrderId);
    }
    
    [Fact]
    public async Task GetOrderById_WithNonExistentId_ShouldReturnNotFound()
    {
        var response = await this._client.GetAsync($"/api/orders/{NonExistentOrderId}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task CreateOrder_WithLocaleHeader_ShouldSucceed()
    {
        var newOrderDto = new OrderDto(
            id: null,
            orderItems: new List<OrderItemDto> { new OrderItemDto(Guid.Parse("bffee4ad-d3cf-4e9f-8876-6bbc120100a4"), 1) },
            total: 150.50,
            deliveryTypeId: new Guid("b6d15e84-3d69-4b74-b270-cb5e9fd0d2d3"),
            clientData: new ClientData("Тест Клієнт", "+380123456789", "Україна", "Тест Місто", "test@test.com", "вул. Тестова, 1", "12345"),
            comment: "Тестове замовлення з локаллю",
            status: "New",
            paymentMethodId: new Guid("a1e7b8c9-d0f1-4a2b-8c3d-4e5f6a7b8c9d")
        );

        var request = new HttpRequestMessage(HttpMethod.Post, "/api/orders");
    
        request.Headers.Add("locale", "ua");
    
        request.Content = JsonContent.Create(newOrderDto);

        var response = await this._client.SendAsync(request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var createResponse = await response.Content.ReadFromJsonAsync<CreateOrderResponse>();
        createResponse.NewOrder.Should().NotBeNull();
        createResponse.NewOrder.Id.Should().NotBeEmpty();
        createResponse.NewOrder.Comment.Should().Be("Тестове замовлення з локаллю");
        createResponse.NewOrder.Number.Should().Be(1010);

        var getResponse = await this._client.GetAsync($"/api/orders/{createResponse.NewOrder.Id}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task UpdateOrderStatus_WithExistingId_ShouldReturnOk()
    {
        var newStatus = "Completed";
        var content = new StringContent($"\"{newStatus}\"", Encoding.UTF8, "application/json");

        var response = await this._client.PatchAsync($"/api/orders/update/status/{ExistingOrderId}", content);
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var updatedOrder = await response.Content.ReadFromJsonAsync<OrderDto>();
        updatedOrder.Should().NotBeNull();
        updatedOrder.Status.Should().Be(newStatus);
    }

    [Fact]
    public async Task UpdateOrderStatus_WithNonExistentId_ShouldReturnNotFound()
    {
        var newStatus = "Completed";
        var content = new StringContent($"\"{newStatus}\"", Encoding.UTF8, "application/json");

        var response = await this._client.PatchAsync($"/api/orders/update/status/{NonExistentOrderId}", content);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateOrderDeliveryData_WithExistingId_ShouldReturnOk()
    {
        var newClientData = new ClientData("Оновлений Клієнт", "+380987654321", "Україна", "Оновлене Місто", "updated@test.com", "вул. Оновлена, 100", "54321");

        var response = await this._client.PatchAsJsonAsync($"/api/orders/update/delivery-data/{ExistingOrderId}", newClientData);
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var updatedOrder = await response.Content.ReadFromJsonAsync<OrderDto>();
        updatedOrder.Should().NotBeNull();
        updatedOrder.ClientData.ClientFullName.Should().Be("Оновлений Клієнт");
        updatedOrder.ClientData.City.Should().Be("Оновлене Місто");
    }

    [Fact]
    public async Task DeleteOrder_WithExistingId_ShouldReturnOkAndConfirmation()
    {
        var response = await this._client.DeleteAsync($"/api/orders/{OrderToDeleteId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var deleteResponse = await response.Content.ReadFromJsonAsync<DeleteOrderResponse>();
        deleteResponse.Should().NotBeNull();
        deleteResponse.IsDeleted.Should().BeTrue();

        var getResponse = await this._client.GetAsync($"/api/orders/{OrderToDeleteId}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task UpdateOrderItemQuantity_ShouldUpdateCorrectly()
    {
        var newOrderDto = new OrderDto(
            id: null,
            orderItems: new List<OrderItemDto> 
            { 
                new OrderItemDto(Guid.Parse("bffee4ad-d3cf-4e9f-8876-6bbc120100a4"), 1) 
            },
            total: 150.50,
            deliveryTypeId: Guid.Parse("b6d15e84-3d69-4b74-b270-cb5e9fd0d2d3"),
            clientData: new ClientData("Тест", "+380000000000", "UA", "Київ", "test@prostasta.com", "вул. Тестова", "00000"),
            comment: "Тест",
            status: "New",
            paymentMethodId: Guid.Parse("a1e7b8c9-d0f1-4a2b-8c3d-4e5f6a7b8c9d")
        );

        var request = new HttpRequestMessage(HttpMethod.Post, "/api/orders");
        request.Headers.Add("locale", "ua");
        request.Content = JsonContent.Create(newOrderDto);

        var createResponse = await this._client.SendAsync(request);
        var createdOrder = (await createResponse.Content.ReadFromJsonAsync<CreateOrderResponse>())?.NewOrder;
        var orderId = createdOrder!.Id;
        var productId = new Guid("bffee4ad-d3cf-4e9f-8876-6bbc120100a4");

        var newQuantity = 3;
        var patchContent = JsonContent.Create(newQuantity); 
        var patchResponse = await this._client.PatchAsync($"/api/orders/{orderId}/items/{productId}/quantity", patchContent);

        patchResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        
        await _client.DeleteAsync($"/api/orders/{orderId}");
    }

    [Fact]
    public async Task RemoveOrderItem_ShouldRemoveSuccessfully()
    {
        var newOrderDto = new OrderDto(
            id: null,
            orderItems: new List<OrderItemDto>
            {
                new OrderItemDto(Guid.Parse("bffee4ad-d3cf-4e9f-8876-6bbc120100a4"), 1)
            },
            total: 150.50,
            deliveryTypeId: Guid.Parse("b6d15e84-3d69-4b74-b270-cb5e9fd0d2d3"),
            clientData: new ClientData("Тест", "+380000000000", "UA", "Київ", "test@prostasta.com", "вул. Тестова", "00000"),
            comment: "Тест",
            status: "New",
            paymentMethodId: Guid.Parse("a1e7b8c9-d0f1-4a2b-8c3d-4e5f6a7b8c9d")
        );

        var request = new HttpRequestMessage(HttpMethod.Post, "/api/orders");
        request.Headers.Add("locale", "ua");
        request.Content = JsonContent.Create(newOrderDto);

        var createResponse = await _client.SendAsync(request);
        var createdOrder = (await createResponse.Content.ReadFromJsonAsync<CreateOrderResponse>())?.NewOrder;
        var orderId = createdOrder!.Id;
        var productId = new Guid("bffee4ad-d3cf-4e9f-8876-6bbc120100a4");

        var deleteItemResponse = await _client.DeleteAsync($"/api/orders/{orderId}/items/{productId}");

        deleteItemResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var getResponse = await _client.GetAsync($"/api/orders/{orderId}");
        var updatedOrder = await getResponse.Content.ReadFromJsonAsync<Order>();
        updatedOrder!.OrderItems.Should().NotContain(i => i.Product.Id == productId);

        await _client.DeleteAsync($"/api/orders/{orderId}");
    }

}