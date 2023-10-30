[assembly: HostingStartup(typeof(GptWeb.Areas.Identity.IdentityHostingStartup))]
namespace GptWeb.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}
