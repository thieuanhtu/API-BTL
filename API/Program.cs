using API.Hubs;
using BLL;
using DAL;
using DAL.Helper;
using DAL.Helper.Interfaces;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình xác thực JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };

        // SignalR gửi token qua query string (?access_token=...)
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    });

// Thêm Controllers
builder.Services.AddControllers();

// Thêm SignalR (realtime)
builder.Services.AddSignalR();

// Cho phép trang test gọi vào API
builder.Services.AddCors(o => o.AddPolicy("DevCors", p =>
    p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

// Thêm Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Đăng ký DatabaseHelper
builder.Services.AddSingleton<IDatabaseHelper>(sp =>
{
    var helper = new DatabaseHelper();
    helper.SetConnectionString(builder.Configuration.GetConnectionString("DefaultConnection"));
    return helper;
});

// Đăng ký Repository + Business
builder.Services.AddTransient<IHoaDonRepository, HoaDonRepository>();
builder.Services.AddTransient<IHoaDonBusiness, HoaDonBusiness>();

builder.Services.AddTransient<IItemGroupRepository, ItemGroupRepository>();
builder.Services.AddTransient<IItemGroupBusiness, ItemGroupBusiness>();

builder.Services.AddTransient<IItemRepository, ItemRepository>();
builder.Services.AddTransient<IItemBusiness, ItemBusiness>();

builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<ICustomerBusiness, CustomerBusiness>();

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserBusiness, UserBusiness>();

builder.Services.AddTransient<INewsRepository, NewsRepository>();
builder.Services.AddTransient<INewsBusiness, NewsBusiness>();

builder.Services.AddTransient<IKitchenTicketRepository, KitchenTicketRepository>();
builder.Services.AddTransient<IKitchenTicketBusiness, KitchenTicketBusiness>();

builder.Services.AddTransient<IIngredientRepository, IngredientRepository>();
builder.Services.AddTransient<IIngredientBusiness, IngredientBusiness>();

builder.Services.AddTransient<IRecipeRepository, RecipeRepository>();
builder.Services.AddTransient<IRecipeBusiness, RecipeBusiness>();

builder.Services.AddTransient<IPromotionRepository, PromotionRepository>();
builder.Services.AddTransient<IPromotionBusiness, PromotionBusiness>();

builder.Services.AddTransient<IReportRepository, ReportRepository>();
builder.Services.AddTransient<IReportBusiness, ReportBusiness>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("DevCors");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<KitchenHub>("/hubs/kitchen");

app.Run();