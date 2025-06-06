using System.Text;
using Application;
using Application.Components.Prices.Engravings;
using Application.Components.TexturedComponents.Data;
using Application.Components.TexturedComponents.Data.Dto.BladeCoatings;
using Application.Files;
using Domain.Component;
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
using Domain.Component.Sheaths.Color;
using Domain.Component.Textures;
using Domain.Files;
using Domain.Order;
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
using Infrastructure.Currencies;
using Infrastructure.Orders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

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
});
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 1;
});

builder.Services.AddIdentity<User, IdentityRole>()
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
builder.Services.AddScoped<IComponentRepository<Knife>, ComponentRepository<Knife>>();
builder.Services.AddScoped<IComponentRepository<CompletedSheath>, ComponentRepository<CompletedSheath>>();
builder.Services.AddScoped<IRepository<Texture>, BaseRepository<Texture>>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IRepository<DeliveryType>, BaseRepository<DeliveryType>>();
builder.Services.AddScoped<IRepository<PaymentMethod>, BaseRepository<PaymentMethod>>();
builder.Services.AddScoped<IRepository<FileEntity>, BaseRepository<FileEntity>>();
builder.Services.AddScoped<IRepository<ProductTag>, BaseRepository<ProductTag>>();
builder.Services.AddScoped<IRepository<AttachmentType>, BaseRepository<AttachmentType>>();
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
#endregion

#region Mappers

builder.Services.AddScoped<ITexturedComponentDtoMapper<BladeCoatingColor, BladeCoatingDto>, BladeCoatingMapper>();

#endregion

#region Dtos

builder.Services.AddScoped<ITexturedComponentDto<BladeCoatingColor>, BladeCoatingDto>();

#endregion

builder.Services.AddHttpContextAccessor();

#region services
builder.Services.AddScoped<ICustomEmailSender, EmailSenderService>();
builder.Services.AddScoped<IFileService, AwsService>();
builder.Services.AddScoped<IGetEngravingPrice, GetEngravingPriceService>();
/*builder.Services.AddScoped<BladeCoatingColorService>();
builder.Services.AddScoped<BladeShapeService>();
builder.Services.AddScoped<CloudinarySettings>();
builder.Services.AddScoped<DeliveryTypeService>();
builder.Services.AddScoped<EngravingService>();
builder.Services.AddScoped<EngravingPriceService>();
builder.Services.AddScoped<FasteningService>();
builder.Services.AddScoped<HandleColorService>();
builder.Services.AddScoped<KnifeService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<OrderStatusesService>();
builder.Services.AddScoped<SheathColorService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UserService>();
*/
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

#region Seeder
using var seederScope = app.Services.CreateScope();
var mainSeeder = seederScope.ServiceProvider.GetRequiredService<MainSeeder>();
await mainSeeder.SeedAsync();
#endregion

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "User", "Admin" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

app.Run();
