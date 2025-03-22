using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Data.Stores;
using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Service;
using LuckysDepartmentStore.Service.Interfaces;
using LuckysDepartmentStore.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
//using LuckysDepartmentStore.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.AddDbContext<LuckysContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'LuckysContext' not found."),
         sqlServerOptions =>
         {
             sqlServerOptions.EnableRetryOnFailure(
                 maxRetryCount: 5,              // Retry up to 5 times
                 maxRetryDelay: TimeSpan.FromSeconds(10), // Wait 10 seconds between retries
                 errorNumbersToAdd: null        // Optionally specify SQL error codes to retry
             );
         }
    )
);



builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<LuckysContext>()
    .AddDefaultTokenProviders();

builder.Services.AddRazorPages();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IColorService, ColorService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddScoped<ICheckoutService, CheckoutService>();
builder.Services.AddScoped<IConsumerService, ConsumerService>();
builder.Services.AddScoped<IProductStore, ProductStore>();
builder.Services.AddScoped<IColorStore, ColorsStore>();
builder.Services.AddScoped<IRatingsStore, RatingsStore>();
builder.Services.AddScoped<IDiscountStore, DiscountStore>();
builder.Services.AddScoped<IBrandStore, BrandStore>();
builder.Services.AddSingleton<IUtility, Utility>();

builder.Services.AddSignalR();
builder.Services.AddHttpContextAccessor();
//builder.Services.AddSingleton<Utility>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddApplicationInsightsTelemetry(options =>
{
    options.ConnectionString = builder.Configuration["ApplicationInsights:ConnectionString"];
});

//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add Identity services
//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//	.AddEntityFrameworkStores<LuckysContext>()
//	.AddDefaultTokenProviders();

var app = builder.Build();
    
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<CartHub>("/CartHub");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();


app.Run();
