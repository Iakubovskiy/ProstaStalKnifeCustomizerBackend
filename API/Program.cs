using System.Text;
using Application;
using Application.Components.TexturedComponents.Data;
using Application.Components.TexturedComponents.Data.Dto.BladeCoatings;
using Application.Files;
using Application.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Domain.Component.BladeCoatingColors;
using Domain.Component.BladeShapes;
using Domain.Component.BladeShapeTypes;
using Domain.Component.Engravings;
using Domain.Component.Engravings.Support;
using Domain.Component.Handles;
using Domain.Component.Product.Attachments;
using Domain.Component.Product.CompletedSheath;
using Domain.Component.Product.Knife;
using Domain.Component.Sheaths;
using Domain.Component.Sheaths.Color;
using Domain.Component.Textures;
using Domain.Order;
using Domain.Order.Suppport;
using Domain.Users;
using Infrastructure;
using Infrastructure.Components;
using Infrastructure.Components.BladeCoatingColors;
using Infrastructure.Components.BladeShapes;
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
builder.Services.AddScoped<IRepository<BladeShapeType>, BaseRepository<BladeShapeType>>();
builder.Services.AddScoped<IRepository<Engraving>, BaseRepository<Engraving>>();
builder.Services.AddScoped<IRepository<EngravingTag>, BaseRepository<EngravingTag>>();
builder.Services.AddScoped<IRepository<EngravingPrice>, BaseRepository<EngravingPrice>>();
builder.Services.AddScoped<IRepository<Handle>, BaseRepository<Handle>>();
builder.Services.AddScoped<IRepository<Sheath>, BaseRepository<Sheath>>();
builder.Services.AddScoped<IRepository<SheathColor>, BaseRepository<SheathColor>>();
builder.Services.AddScoped<IRepository<Attachment>, BaseRepository<Attachment>>();
builder.Services.AddScoped<IRepository<Knife>, BaseRepository<Knife>>();
builder.Services.AddScoped<IRepository<CompletedSheath>, BaseRepository<CompletedSheath>>();
builder.Services.AddScoped<IRepository<Texture>, BaseRepository<Texture>>();
builder.Services.AddScoped<IRepository<Order>, BaseRepository<Order>>();
builder.Services.AddScoped<IRepository<DeliveryType>, BaseRepository<DeliveryType>>();
builder.Services.AddScoped<IRepository<PaymentMethod>, BaseRepository<PaymentMethod>>();


//it should be moved to services
    builder.Services.AddScoped<ICustomEmailSender, EmailSenderService>();
    builder.Services.AddScoped<IFileService, AwsService>();
//
#endregion

#region Mappers

builder.Services.AddScoped<ITexturedComponentDtoMapper<BladeCoatingColor, BladeCoatingDto>, BladeCoatingMapper>();

#endregion

#region Dtos

builder.Services.AddScoped<ITexturedComponentDto<BladeCoatingColor>, BladeCoatingDto>();

#endregion


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
