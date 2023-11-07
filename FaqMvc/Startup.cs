using FaqMvc.Data;
using GptWeb.Models;
using GptWeb.Services;
using GptWeb.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            // Configuring Entity Framework Core
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            // Configuring Identity
            services.AddDefaultIdentity<UserModel>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<AppDbContext>();
                
            // Adding controllers with views and Razor Pages
            services.AddControllersWithViews();
            services.AddRazorPages();

            // Additional configurations as needed...
            services.AddHttpClient();
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

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
