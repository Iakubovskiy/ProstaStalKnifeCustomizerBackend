using System.Data.Entity.Infrastructure;
using System.Net;
using Domain.Component.Product.Knife;
using FluentAssertions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Xunit;

namespace API.Components.Products.AllProducts;

public class GetAllProductsControllerTests: IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebAppFactory _factory;

    public GetAllProductsControllerTests(CustomWebAppFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }
    
    #region Response DTOs
    
    private class PaginatedProductsResponse
    {
        [JsonProperty("filters")]
        public object Filters { get; set; }

        [JsonProperty("products")]
        public PaginatedResult<ProductViewModel> Products { get; set; }
    }

    private class PaginatedResult<T>
    {
        [JsonProperty("items")]
        public List<T> Items { get; set; }

        [JsonProperty("totalItems")]
        public int TotalItems { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("pageSize")]
        public int PageSize { get; set; }

        [JsonProperty("totalPages")]
        public int TotalPages { get; set; }
    }

    private class ProductViewModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }
    }

    #endregion

    #region Helper Methods

    private async Task<PaginatedProductsResponse> GetAndDeserializeResponse(string uri, string locale = "ua", string currency = "uah")
    {
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        request.Headers.Add("Locale", locale);
        request.Headers.Add("Currency", currency);

        var response = await _client.SendAsync(request);
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<PaginatedProductsResponse>(content);
        
        result.Should().NotBeNull();
        result.Products.Should().NotBeNull();

        return result;
    }

    #endregion

    #region Basic Scenarios (Happy Path)
    
    [Fact]
    public async Task NoFilters_ShouldReturnAllActiveProducts()
    {
        var result = await GetAndDeserializeResponse("/api/products/catalog?page=1");

        const int expectedItemsInResponse = 20;
        const int expectedTotalActive = 25;
        result.Products.TotalItems.Should().Be(expectedTotalActive);
        result.Products.Items.Count.Should().Be(expectedItemsInResponse);
        
        var inactiveKnifeId = new Guid("6ab86bca-df8a-43a4-8b75-ed44c741ae10");
        var inactiveSheathId = new Guid("5e4efd9a-ef73-47bc-8a00-ab71f2e1ea34");
        var inactiveAttachmentId = new Guid("33333333-cccc-4ccc-cccc-cccccccccccc");

        result.Products.Items.Should().NotContain(p => p.Id == inactiveKnifeId);
        result.Products.Items.Should().NotContain(p => p.Id == inactiveSheathId);
        result.Products.Items.Should().NotContain(p => p.Id == inactiveAttachmentId);
    }
    
    [Theory]
    [InlineData("knife", 9)]
    [InlineData("sheath", 8)]
    [InlineData("attachment", 8)]
    public async Task FilterByProductType_ShouldReturnOnlyProductsOfThatType(string productType, int expectedCount)
    {
        var result = await GetAndDeserializeResponse($"/api/products/catalog?productType={productType}");
        
        result.Products.TotalItems.Should().Be(expectedCount);
        result.Products.Items.Count.Should().Be(expectedCount);
    }

    [Fact]
    public async Task FilterBySingleStyle_Military_ShouldReturnCorrectProducts()
    {
        var result = await GetAndDeserializeResponse("/api/products/catalog?styles=Військове");

        var expectedIds = new[]
        {
            new Guid("4a7e35ec-57fa-4efb-bba6-3ef27ed4d168"),
            new Guid("ca047152-c3f2-4747-8fa6-caf3b73cd693"),
            new Guid("2f5f6373-1d0d-4b1a-992d-8a3d5326a6a7")
        };
        
        result.Products.TotalItems.Should().Be(expectedIds.Length);
        result.Products.Items.Select(p => p.Id).Should().BeEquivalentTo(expectedIds);
    }

    [Fact]
    public async Task FilterByMultipleStyles_MilitaryOrCeltic_ShouldReturnUnionOfProducts()
    {
        var result = await GetAndDeserializeResponse("/api/products/catalog?styles=Військове&styles=Кельтське");
        
        var expectedIds = new[]
        {
            new Guid("4a7e35ec-57fa-4efb-bba6-3ef27ed4d168"),
            new Guid("ca047152-c3f2-4747-8fa6-caf3b73cd693"),
            new Guid("e1b84a52-6e7e-42ab-979f-abcb41f3bd92"),
            new Guid("8ad7812a-72d2-4836-916d-0e98621f0f95"),
            new Guid("2f5f6373-1d0d-4b1a-992d-8a3d5326a6a7")
        };
        
        result.Products.TotalItems.Should().Be(expectedIds.Length);
        result.Products.Items.Select(p => p.Id).Should().BeEquivalentTo(expectedIds);
    }

    [Fact]
    public async Task FilterByMultipleStyles_ProductWithBothStyles_ShouldNotBeDuplicated()
    {
        var result = await GetAndDeserializeResponse("/api/products/catalog?styles=Військове&styles=Кельтське");
        
        var idOfProductWithBothTags = new Guid("2f5f6373-1d0d-4b1a-992d-8a3d5326a6a7");

        result.Products.Items
            .Where(p => p.Id == idOfProductWithBothTags)
            .Should().HaveCount(1);
    }
    
    [Fact]
    public async Task FilterByBladeLength_ShouldReturnMatchingKnives()
    {
        var result = await GetAndDeserializeResponse("/api/products/catalog?minBladeLength=100&maxBladeLength=110");
        
        var expectedIds = new[]
        {
            new Guid("4a7e35ec-57fa-4efb-bba6-3ef27ed4d168"),
            new Guid("c785ecbe-7f3c-4b2b-bf9d-1cd0df319377") 
        };
        
        result.Products.TotalItems.Should().Be(expectedIds.Length);
        result.Products.Items.Select(p => p.Id).Should().BeEquivalentTo(expectedIds);
    }
    
    #endregion

    #region Complex & Combined Scenarios

    [Fact]
    public async Task FilterByProductTypeAndStyle_ShouldReturnCorrectSubset()
    {
        var result = await GetAndDeserializeResponse("/api/products/catalog?productType=knife&styles=Військове");

        var expectedIds = new[]
        {
            new Guid("4a7e35ec-57fa-4efb-bba6-3ef27ed4d168"), // knife1
            new Guid("ca047152-c3f2-4747-8fa6-caf3b73cd693")  // knife6
        };
        
        result.Products.TotalItems.Should().Be(expectedIds.Length);
        result.Products.Items.Select(p => p.Id).Should().BeEquivalentTo(expectedIds);
    }

    [Fact]
    public async Task FilterByStyleAndColor_ShouldReturnIntersectionOfProducts()
    {
        var result = await GetAndDeserializeResponse("/api/products/catalog?styles=Військове&colors=Чорний", locale: "ua");
        
        var expectedIds = new[]
        {
            new Guid("4a7e35ec-57fa-4efb-bba6-3ef27ed4d168"), 
            new Guid("ca047152-c3f2-4747-8fa6-caf3b73cd693"), 
            new Guid("2f5f6373-1d0d-4b1a-992d-8a3d5326a6a7"), 
        };
        
        result.Products.TotalItems.Should().Be(expectedIds.Length);
        result.Products.Items.Select(p => p.Id).Should().BeEquivalentTo(expectedIds);
    }

    #endregion
    
    #region Edge Cases & Negative Scenarios
    
    [Fact]
    public async Task FilterByProductTypeSheathAndBladeLength_ShouldReturnEmptyList()
    {
       var response = await this._client.GetAsync("/api/products/catalog?productType=sheath&minBladeLength=100");
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task FilterByNonExistentStyle_ShouldReturnEmptyList()
    {
        var result = await GetAndDeserializeResponse("/api/products/catalog?styles=НеіснуючийСтиль");

        result.Products.Items.Should().BeEmpty();
        result.Products.TotalItems.Should().Be(0);
    }
    
    [Fact]
    public async Task FilterByEmptyStylesArray_ShouldReturnEmptyList()
    {
        var result = await GetAndDeserializeResponse("/api/products/catalog?styles=");

        result.Products.Items.Should().BeEmpty();
        result.Products.TotalItems.Should().Be(0);
    }

    [Fact]
    public async Task FilterByNonExistentCurrency_ShouldThrowExceptionOrReturnError()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/products/catalog");
        request.Headers.Add("Locale", "ua");
        request.Headers.Add("Currency", "nonexistent");

        var response = await _client.SendAsync(request);
        
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task Pagination_ShouldReturnCorrectPageAndSize()
    {
        var pageSize = 20;
        var result = await GetAndDeserializeResponse($"/api/products/catalog?page=2");

        result.Products.Page.Should().Be(2);
        result.Products.PageSize.Should().Be(pageSize);
        result.Products.Items.Should().HaveCount(5);
    }

    #endregion
}