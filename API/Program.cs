using System.Text;
using API.Components.BladeShapes.Presenters;
using API.Components.Products.AllProducts.Presenters;
using Application;
using Application.Components.Activate;
using Application.Components.ComponentsWithType.SheathColors;
using Application.Components.ComponentsWithType.SheathColors.Activate;
using Application.Components.ComponentsWithType.SheathColors.Deactivate;
using Application.Components.ComponentsWithType.UseCases.Create;
using Application.Components.ComponentsWithType.UseCases.Deactivate;
using Application.Components.ComponentsWithType.UseCases.Update;
using Application.Components.Deactivate;
using Application.Components.Prices;
using Application.Components.Prices.Engravings;
using Application.Components.Products;
using Application.Components.Products.Attachments;
using Application.Components.Products.Attachments.Type;
using Application.Components.Products.CompletedSheaths;
using Application.Components.Products.Knives;
using Application.Components.Products.UseCases.Activate;
using Application.Components.Products.UseCases.Create;
using Application.Components.Products.UseCases.Deactivate;
using Application.Components.Products.UseCases.Update;
using Application.Components.SimpleComponents.BladeShapes;
using Application.Components.SimpleComponents.Engravings;
using Application.Components.SimpleComponents.Engravings.EngravingTags;
using Application.Components.SimpleComponents.Products.ProductTags;
using Application.Components.SimpleComponents.Sheaths;
using Application.Components.SimpleComponents.Textures;
using Application.Components.SimpleComponents.UseCases;
using Application.Components.SimpleComponents.UseCases.Create;
using Application.Components.SimpleComponents.UseCases.Update;
using Application.Components.TexturedComponents.Data;
using Application.Components.TexturedComponents.Data.Dto.BladeCoatings;
using Application.Components.TexturedComponents.Data.Dto.HandleColors;
using Application.Files;
using Application.Orders.Support.DeliveryTypes;
using Application.Orders.Support.DeliveryTypes.Data;
using Application.Orders.Support.PaymentMethods;
using Application.Orders.Support.PaymentMethods.Data;
using Application.Users.UseCases.Authentication;
using Application.Users.UseCases.Registration;
using Application.Users.UseCases.Update;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Domain.Component.BladeCoatingColors;
using Domain.Component.BladeShapes;
using Domain.Component.BladeShapes.BladeShapeTypes;
using Domain.Component.Engravings;
using Domain.Component.Engravings.Support;
using Domain.Component.Handles;
using Domain.Component.Product;
using Domain.Component.Product.Attachments;
using Domain.Component.Product.CompletedSheath;
using Domain.Component.Product.Knife;
using Domain.Component.Sheaths;
using Domain.Component.Textures;
using Domain.Files;
using Domain.Order.Support;
using Domain.Users;
using Infrastructure;
using Infrastructure.Components;
using Infrastructure.Components.BladeCoatingColors;
using Infrastructure.Components.BladeShapes;
using Infrastructure.Components.Engravings;
using Infrastructure.Components.Handles;
using Infrastructure.Components.Products;
using Infrastructure.Components.Products.Attachments;
using Infrastructure.Components.Sheaths;
using Infrastructure.Components.Sheaths.Color;
using Infrastructure.Components.Textures;
using Infrastructure.Currencies;
using Infrastructure.Orders;
using Infrastructure.Orders.Support.DeliveryTypes;
using Infrastructure.Orders.Support.PaymentMethods;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Application.Components.TexturedComponents.UseCases.Create;
using Application.Components.TexturedComponents.UseCases.Update;
using Application.Orders;
using Application.Orders.Dto;
using Application.Orders.UseCases.ChangeClientData;
using Application.Orders.UseCases.Create;
using Application.Orders.UseCases.UpdateStatus;
using Domain.Component.Sheaths.Color;
using Infrastructure.Components.Products.CompletedSheaths;
using Infrastructure.Components.Products.Filters.Characteristics;
using Infrastructure.Components.Products.Filters.Colors;
using Infrastructure.Components.Products.Filters.Price;
using Infrastructure.Components.Products.Filters.Styles;
using Infrastructure.Components.Products.Knives;
using Infrastructure.Users;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsEnvironment("Test"))
{
    // builder.Services.AddDbContext<DBContext>(options =>
    //     options.UseInMemoryDatabase("TestDb"));
}
else
{

    var connectionString = builder.Configuration.GetValue<string>("DATABASE_URL");
    if (connectionString != null)
    {
        var databaseUrl = new Uri(connectionString);
        var userInfo = databaseUrl.UserInfo.Split(':');

        connectionString = $"Host={databaseUrl.Host};" +
                           $"Port={databaseUrl.Port};" +
                           $"Database={databaseUrl.AbsolutePath.Trim('/')};" +
                           $"Username={userInfo[0]};" +
                           $"Password={userInfo[1]};" +
                           $"SSL Mode=Require;Trust Server Certificate=True;";
    }
    else
    {
        connectionString = builder.Configuration.GetConnectionString("DBContext");
    }
    builder.Services.AddDbContext<DBContext>(options =>
        options.UseNpgsql(connectionString));
}
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 512 * 1024 * 1024;
});

var secretKey = builder.Configuration["Jwt:Key"];
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            Console.WriteLine("Authorization Header: " + context.Request.Headers["Authorization"]);
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            Console.WriteLine("Token Validated");
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = c =>
        {
            c.NoResult();

            c.Response.StatusCode = 401;
            c.Response.ContentType = "text/plain";

            return c.Response.WriteAsync("There was an issue authorizing you.");
        }
    };

});
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: 'Bearer 12345abcdef'",
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
    
    options.EnableAnnotations();
});
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 1;
});

builder.Services.AddIdentity<User, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<DBContext>()
    .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddHttpClient("Api", client =>
{
    client.BaseAddress = new Uri("https://localhost:7034/");
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region repositories

builder.Services.AddScoped<IComponentRepository<BladeCoatingColor>, BladeCoatingColorRepository>();
builder.Services.AddScoped<IComponentRepository<BladeShape>, BladeShapeRepository>();
builder.Services.AddScoped<IComponentRepository<Handle>, HandleRepository>();
builder.Services.AddScoped<IComponentRepository<Sheath>, SheathRepository>();
builder.Services.AddScoped<ISheathColorRepository, SheathColorRepository>();
builder.Services.AddScoped<IRepository<BladeShapeType>, BaseRepository<BladeShapeType>>();
builder.Services.AddScoped<IComponentRepository<Engraving>, EngravingRepository>();
builder.Services.AddScoped<IRepository<EngravingTag>, BaseRepository<EngravingTag>>();
builder.Services.AddScoped<IRepository<EngravingPrice>, BaseRepository<EngravingPrice>>();
builder.Services.AddScoped<IComponentRepository<Attachment>, AttachmentRepository>();
builder.Services.AddScoped<IComponentRepository<Knife>, KnifeRepository>();
builder.Services.AddScoped<IComponentRepository<CompletedSheath>, CompletedSheathRepository>();
builder.Services.AddScoped<IRepository<Texture>, TextureRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IRepository<DeliveryType>, BaseRepository<DeliveryType>>();
builder.Services.AddScoped<IRepository<PaymentMethod>, BaseRepository<PaymentMethod>>();
builder.Services.AddScoped<IRepository<FileEntity>, BaseRepository<FileEntity>>();
builder.Services.AddScoped<IRepository<ProductTag>, BaseRepository<ProductTag>>();
builder.Services.AddScoped<IRepository<AttachmentType>, BaseRepository<AttachmentType>>();
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IDeliveryTypeRepository, DeliveryTypeRepository>();
builder.Services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
builder.Services.AddScoped<IRepository<User>, BaseRepository<User>>();
builder.Services.AddScoped<IFilterStylesRepository, FilterStylesRepository>();
builder.Services.AddScoped<IGetBladeShapeCharacteristicsFilterRepository, GetBladeShapeCharacteristicsFilterRepository>();
builder.Services.AddScoped<IColorsFilterRepository, ColorsFilterRepository>();
builder.Services.AddScoped<IPriceFilterRepository, PriceFilterRepository>();
builder.Services.AddScoped<IGetProductPaginatedList, ProductRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
#endregion

#region Mappers

builder.Services.AddScoped<ITexturedComponentDtoMapper<BladeCoatingColor, BladeCoatingDto>, BladeCoatingMapper>();
builder.Services.AddScoped<ITexturedComponentDtoMapper<Handle, HandleColorDto>, HandleColorDtoMapper>();
builder.Services.AddScoped<IComponentDtoMapper<Sheath, SheathDto>, SheathDtoMapper>();
builder.Services.AddScoped<IComponentWithTypeDtoMapper<SheathColor, SheathColorDto>, SheathColorDtoMapper>();
builder.Services.AddScoped<IOrderDtoMapper, OrderDtoMapper>();

#endregion

#region Dtos

builder.Services.AddScoped<ITexturedComponentDto<BladeCoatingColor>, BladeCoatingDto>();

#endregion

builder.Services.AddHttpContextAccessor();

#region services

builder.Services.AddScoped<ICustomEmailSender, EmailSenderService>();
builder.Services.AddScoped<IFileService, AwsService>();
builder.Services.AddScoped<IGetEngravingPrice, GetEngravingPriceService>();

builder.Services.AddScoped<ISimpleCreateService<EngravingTag, EngravingTagDto>, SimpleCreateService<EngravingTag, EngravingTagDto>>();
builder.Services.AddScoped<IComponentDtoMapper<EngravingTag, EngravingTagDto>, EngravingTagDtoMapper>();
builder.Services.AddScoped<ISimpleUpdateService<EngravingTag, EngravingTagDto>, SimpleUpdateService<EngravingTag, EngravingTagDto>>();

builder.Services.AddScoped<ISimpleCreateService<ProductTag, ProductTagDto>, SimpleCreateService<ProductTag, ProductTagDto>>();
builder.Services.AddScoped<IComponentDtoMapper<ProductTag, ProductTagDto>, ProductTagDtoMapper>();
builder.Services.AddScoped<ISimpleUpdateService<ProductTag, ProductTagDto>, SimpleUpdateService<ProductTag, ProductTagDto>>();

builder.Services.AddScoped<ISimpleCreateService<AttachmentType, AttachmentTypeDto>, SimpleCreateService<AttachmentType, AttachmentTypeDto>>();
builder.Services.AddScoped<IComponentDtoMapper<AttachmentType, AttachmentTypeDto>, AttachmentTypeDtoMapper>();

builder.Services.AddScoped<ISimpleCreateService<Texture, TextureDto>, SimpleCreateService<Texture, TextureDto>>();
builder.Services.AddScoped<ISimpleUpdateService<Texture, TextureDto>, SimpleUpdateService<Texture, TextureDto>>();
builder.Services.AddScoped<IComponentDtoMapper<Texture, TextureDto>, TextureDtoMapper>();

builder.Services.AddScoped<IDeliveryTypeService, DeliveryTypeService>();
builder.Services.AddScoped<IDeliveryTypeDtoMapper, DeliveryTypeDtoMapper>();

builder.Services.AddScoped<IPaymentMethodService, PaymentMethodService>();
builder.Services.AddScoped<IPaymentMethodDtoMapper, PaymentMethodDtoMapper>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<IUpdateUserService, UpdateUserService>();

builder.Services.AddScoped<ICreateProductService<Attachment, AttachmentDto>, CreateProductService<Attachment, AttachmentDto>>();
builder.Services.AddScoped<IUpdateProductService<Attachment, AttachmentDto>, UpdateProductService<Attachment, AttachmentDto>>();
builder.Services.AddScoped<IProductDtoMapper<Attachment, AttachmentDto>, AttachmentDtoMapper>();
builder.Services.AddScoped<IActivateProduct<Attachment>, ActivateProductService<Attachment>>();
builder.Services.AddScoped<IDeactivateProduct<Attachment>, DeactivateProductService<Attachment>>();

builder.Services.AddScoped<ICreateProductService<CompletedSheath, CompletedSheathDto>, CreateProductService<CompletedSheath, CompletedSheathDto>>();
builder.Services.AddScoped<IUpdateProductService<CompletedSheath, CompletedSheathDto>, UpdateProductService<CompletedSheath, CompletedSheathDto>>();
builder.Services.AddScoped<IProductDtoMapper<CompletedSheath, CompletedSheathDto>, CompletedSheathDtoMapper>();
builder.Services.AddScoped<IActivateProduct<CompletedSheath>, ActivateProductService<CompletedSheath>>();
builder.Services.AddScoped<IDeactivateProduct<CompletedSheath>, DeactivateProductService<CompletedSheath>>();

builder.Services.AddScoped<ICreateProductService<Knife, KnifeDto>, CreateProductService<Knife, KnifeDto>>();
builder.Services.AddScoped<IUpdateProductService<Knife, KnifeDto>, UpdateProductService<Knife, KnifeDto>>();
builder.Services.AddScoped<IProductDtoMapper<Knife, KnifeDto>, KnifeDtoMapper>();
builder.Services.AddScoped<IActivateProduct<Knife>, ActivateProductService<Knife>>();
builder.Services.AddScoped<IDeactivateProduct<Knife>, DeactivateProductService<Knife>>();


builder.Services.AddScoped<IActivate<BladeCoatingColor>, ActivateComponentService<BladeCoatingColor>>();
builder.Services.AddScoped<IDeactivate<BladeCoatingColor>, DeactivateComponentService<BladeCoatingColor>>();
builder.Services.AddScoped<ICreateTexturedComponent<BladeCoatingColor, BladeCoatingDto>, 
    CreateTexturedComponent<BladeCoatingColor, BladeCoatingDto>>();
builder.Services.AddScoped<IUpdateTexturedComponent<BladeCoatingColor, BladeCoatingDto>, 
    UpdateTexturedComponent<BladeCoatingColor, BladeCoatingDto>>();

builder.Services.AddScoped<IComponentDtoMapper<BladeShape, BladeShapeDto>, BladeShapeDtoMapper>();
builder.Services.AddScoped<IActivate<BladeShape>, ActivateComponentService<BladeShape>>();
builder.Services.AddScoped<IDeactivate<BladeShape>, DeactivateComponentService<BladeShape>>();
builder.Services.AddScoped<ICreateService<BladeShape, BladeShapeDto>, 
    CreateComponent<BladeShape, BladeShapeDto>>();
builder.Services.AddScoped<IUpdateService<BladeShape, BladeShapeDto>, 
    UpdateService<BladeShape, BladeShapeDto>>();

builder.Services.AddScoped<IComponentDtoMapper<Engraving, EngravingDto>, EngravingDtoMapper>();
builder.Services.AddScoped<IActivate<Engraving>, ActivateComponentService<Engraving>>();
builder.Services.AddScoped<IDeactivate<Engraving>, DeactivateComponentService<Engraving>>();
builder.Services.AddScoped<ICreateService<Engraving, EngravingDto>, 
    CreateComponent<Engraving, EngravingDto>>();
builder.Services.AddScoped<IUpdateService<Engraving, EngravingDto>, 
    UpdateService<Engraving, EngravingDto>>();

builder.Services.AddScoped<IActivate<Handle>, ActivateComponentService<Handle>>();
builder.Services.AddScoped<IDeactivate<Handle>, DeactivateComponentService<Handle>>();
builder.Services.AddScoped<ICreateTexturedComponent<Handle, HandleColorDto>, 
    CreateTexturedComponent<Handle, HandleColorDto>>();
builder.Services.AddScoped<IUpdateTexturedComponent<Handle, HandleColorDto>, 
    UpdateTexturedComponent<Handle, HandleColorDto>>();


builder.Services.AddScoped<IActivate<Sheath>, ActivateComponentService<Sheath>>();
builder.Services.AddScoped<IDeactivate<Sheath>, DeactivateComponentService<Sheath>>();
builder.Services.AddScoped<ICreateService<Sheath, SheathDto>, 
    CreateComponent<Sheath, SheathDto>>();
builder.Services.AddScoped<IUpdateService<Sheath, SheathDto>, 
    UpdateService<Sheath, SheathDto>>();

builder.Services.AddScoped<IActivateSheathColorService, ActivateSheathColor>();
builder.Services.AddScoped<IDeactivateSheathColorService, DeactivateSheathColor>();
builder.Services.AddScoped<ICreateTypeDependencyComponentService<SheathColor, SheathColorDto>, 
    CreateSheathColor>();
builder.Services.AddScoped<IUpdateTypeDependencyComponentService<SheathColor, SheathColorDto>, 
    UpdateSheathColorService>();

builder.Services.AddScoped<ISheathColorRepository, SheathColorRepository>();
builder.Services.AddScoped<IGetComponentPrice, GetComponentPriceService>();

builder.Services.AddScoped<ICreateOrderService, CreateOrderService>();
builder.Services.AddScoped<IUpdateOrderStatusService, UpdateOrderStatusService>();
builder.Services.AddScoped<IChangeClientDataService, ChangeClientDataService>();
#endregion

#region Presenters

builder.Services.AddScoped<ProductPresenter>();
builder.Services.AddScoped<BladeShapePresenter>();

#endregion

#region Seeder
builder.Services.Scan(scan => scan
    .FromAssemblyOf<ISeeder>()
    .AddClasses(classes => classes.AssignableTo<ISeeder>())
    .AsImplementedInterfaces()
    .WithTransientLifetime());

builder.Services.AddTransient<MainSeeder>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

if (!app.Environment.IsEnvironment("Test"))
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        var roles = new[] { "User", "Admin" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(role));
            }
        }
    }
}

// #region Seeder
// using var seederScope = app.Services.CreateScope();
// var mainSeeder = seederScope.ServiceProvider.GetRequiredService<MainSeeder>();
// await mainSeeder.SeedAsync();
// #endregion

app.Run();

app.Run();

public partial class Program {}
