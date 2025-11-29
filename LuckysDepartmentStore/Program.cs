using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Data.Stores;
using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Service;
using LuckysDepartmentStore.Service.Interfaces;
using LuckysDepartmentStore.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
//using LuckysDepartmentStore.Data;

internal class Program
{
    private static void Main(string[] args)
    {
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

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<LuckysContext>()
            .AddDefaultTokenProviders();

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Identity/Account/Login";
        });

        builder.Services.AddTransient<IEmailSender, NullEmailSender>();

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
        builder.Services.AddScoped<IPaymentStore, PaymentStore>();
        builder.Services.AddScoped<IShippingStore, ShippingStore>();
        builder.Services.AddScoped<ICustomerStore, CustomerStore>();
        builder.Services.AddScoped<ICategoryStore, CategoryStore>();
        builder.Services.AddScoped<IShoppingCartStore, ShoppingCartStore>();
        builder.Services.AddScoped<ISubcategoryStore, SubcategoryStore>();
        builder.Services.AddScoped<ICustomerOrderItemsStore, CustomerOrderItemsStore>();

        builder.Services.AddSignalR();
        builder.Services.AddHttpContextAccessor();
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

        var app = builder.Build();

        // Do not currently have access to this feature in Azure do to being on the Free Tier
        //if (app.Environment.IsDevelopment())
        //{
        //    app.UseDeveloperExceptionPage();
        //}
        //else
        //{
        //    app.UseExceptionHandler(errorApp =>
        //    {
        //        errorApp.Run(async context =>
        //        {
        //            var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        //            var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        //            logger.LogError(exception, "Unhandled exception occurred");
        //            context.Response.Redirect("/Error");
        //        });
        //    });
        //    app.UseStatusCodePagesWithReExecute("/Error", "?statusCode={0}");
        //}

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
    }
}