using BaytonicECommerce.Data;
using BaytonicECommerce.Models;
using BaytonicECommerce.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddMvc()
         .AddDataAnnotationsLocalization(options =>
         {
             options.DataAnnotationLocalizerProvider = (type, factory) =>
                 factory.Create(typeof(Resource));
         });
            services.Configure<RequestLocalizationOptions>(option =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("ar-EG")

                };
                option.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(culture: "en", uiCulture: "en");
                option.SupportedCultures = supportedCultures;
                option.SupportedUICultures = supportedCultures;
            });
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddRazorPages();
            services.AddSession();
            services.AddDbContext<EcommerceContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            

            //google and facebook login
            #region google and facebook login

            services.AddAuthentication()
               .AddGoogle(options =>
               {
                   IConfigurationSection googleAuthSection = Configuration.GetSection("Authentication:Google");
                   options.ClientId = googleAuthSection["ClientId"];
                   options.ClientSecret = googleAuthSection["ClientSecret"];
               })
                .AddFacebook(options =>
                {
                    IConfigurationSection facebookAuthSection = Configuration.GetSection("Authentication:Facebook");
                    options.AppId = facebookAuthSection["AppId"];
                    options.AppSecret = facebookAuthSection["AppSecret"];
                });

            #endregion

            
            services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 0;

            }).AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            services.Configure<DataProtectionTokenProviderOptions>(o =>
        o.TokenLifespan = TimeSpan.FromMinutes(20));
            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromHours(20);
            });
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICouponRepository, CouponRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderProductRepository, OrderProductRepository>();
            services.AddScoped<IProductDescriptionRepository, ProductDescriptionRepository>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            services.AddScoped<IWishListRepository, WishListRepository>();
            services.AddScoped<IGovernmentShippingRepository, GovernmentShippingRepository>();
            services.AddScoped<IAccountManagementRepository, AccountManagementRepository>();
            services.AddScoped<INewsLetterRepository, NewsLetterRepository>();
            services.AddHttpContextAccessor();



        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<IdentityUser> userManager)
        {
            app.UseRequestLocalization(
           app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            ApplicationDbInitializer.SeedUsers(userManager);
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            var lockOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(lockOptions.Value);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
