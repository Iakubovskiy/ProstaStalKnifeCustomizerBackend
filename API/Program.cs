using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Infrastructure.Data;
using Domain.Component.BladeCoatingColors;
using Domain.Component.BladeShapes;
using Domain.Component.Engravings;
using Domain.Component.Engravings.Support;
using Domain.Component.Handles;
using Domain.Component.Product.Attachments;
using Domain.Component.Product.Knife;
using Domain.Component.Sheaths.Color;
using Domain.Order;
using Domain.Order.Suppport;
using Domain.Users;

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
/*builder.Services.AddAuthentication(options =>
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

});*/
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

/*#region interfaces
builder.Services.AddScoped<IRepository<BladeCoatingColor, Guid>, BladeCoatingColorRepository>();
builder.Services.AddScoped<IRepository<BladeShape, Guid>, BladeShapeRepository>();
builder.Services.AddScoped<IRepository<DeliveryType, Guid>, DeliveryTypeRepository>();
builder.Services.AddScoped<IRepository<Engraving, Guid>, EngravingRepository>();
builder.Services.AddScoped<IRepository<EngravingPrice, Guid>, EngravingPriceRepository>();
builder.Services.AddScoped<IRepository<Attachment, Guid>, FasteningRepository>();
builder.Services.AddScoped<IRepository<Handle, Guid>, HandleRepository>();
builder.Services.AddScoped<IRepository<Knife, Guid>, KnifeRepository>();
builder.Services.AddScoped<IRepository<Order, Guid>, OrderRepository>();
builder.Services.AddScoped<IRepository<OrderStatuses, Guid>, OrderStatusesRepository>();
builder.Services.AddScoped<IRepository<SheathColor, Guid>, SheathColorRepository>();
builder.Services.AddScoped<ICustomEmailSender, EmailSenderService>();
builder.Services.AddScoped<IFileService, AwsService>();
#endregion*/

/*#region services
builder.Services.AddScoped<BladeCoatingColorService>();
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
#endregion*/

var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

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
