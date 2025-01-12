using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WorkshopBackend.Data;
using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;
using WorkshopBackend.Repositories;
using WorkshopBackend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DBContext")
    ));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
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

var clodinaryUrl = builder.Configuration["Cloudinary:CLOUDINARY_URL"];
builder.Services.Configure<IOptions<CloudinarySettings>>(builder.Configuration.GetSection("Cloudinary"));

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

#region interfaces
builder.Services.AddScoped<Repository<BladeCoatingColor, Guid>, BladeCoatingColorRepository>();
builder.Services.AddScoped<Repository<BladeShape, Guid>, BladeShapeRepository>();
builder.Services.AddScoped<Repository<DeliveryType, Guid>, DeliveryTypeRepository>();
builder.Services.AddScoped<Repository<Engraving, Guid>, EngravingRepository>();
builder.Services.AddScoped<Repository<EngravingPrice, Guid>, EngravingPriceRepository>();
builder.Services.AddScoped<Repository<Fastening, Guid>, FasteningRepository>();
builder.Services.AddScoped<Repository<HandleColor, Guid>, HandleColorRepository>();
builder.Services.AddScoped<Repository<Knife, Guid>, KnifeRepository>();
builder.Services.AddScoped<Repository<Order, Guid>, OrderRepository>();
builder.Services.AddScoped<Repository<OrderStatuses, Guid>, OrderStatusesRepository>();
builder.Services.AddScoped<Repository<SheathColor, Guid>, SheathColorRepository>();
builder.Services.AddScoped<ICustomEmailSender, EmailSenderService>();
builder.Services.AddScoped<IFileService, CloudinaryService>();
#endregion

#region services
builder.Services.AddScoped<BladeCoatingColorService>();
builder.Services.AddScoped<BladeShapeService>();
builder.Services.AddScoped<CloudinarySettings>();
builder.Services.AddScoped<CloudinaryService>();
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
#endregion

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
