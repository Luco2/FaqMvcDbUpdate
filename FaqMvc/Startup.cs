using FaqMvc.Data;
using GptWeb.Models;
using GptWeb.Services;
using GptWeb.Services.Interfaces;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace GptWeb
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
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] { "en-US", "es-ES" };
                options.SetDefaultCulture(supportedCultures[0])
                       .AddSupportedCultures(supportedCultures)
                       .AddSupportedUICultures(supportedCultures);
            });

            // Configuring Entity Framework Core
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            // Configuring Identity
            services.AddDefaultIdentity<UserModel>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<AppDbContext>();

            // Adding controllers with views and Razor Pages
            services.AddControllersWithViews()
            .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization();

            services.AddRazorPages();

            // Additional configurations:
            services.AddHttpClient();
            services.AddScoped<IFaqService, FaqService>();
            services.AddScoped<IChatService>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                bool useMock = bool.Parse(Configuration["USE_MOCK_OPENAI"]);

                if (useMock)
                {
                    return new MockChatService(configuration, provider.GetRequiredService<ILogger<MockChatService>>());
                }
                else
                {
                    return new FineTunedChatService(
                        provider.GetRequiredService<IHttpClientFactory>(),
                        configuration,
                        provider.GetRequiredService<ILogger<FineTunedChatService>>()
                    );
                }
            });

            services.AddMemoryCache();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // In development, the Vue CLI server will be serving files and enabling HMR (Hot Module Replacement)
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(); // Needed to serve the Vue.js static files

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();

                // Map the fallback route for serving the Vue.js app
                endpoints.MapFallbackToFile("index.html"); // Make sure this points to the correct file path for your Vue.js app's entry point
            });
        }
    }
}
