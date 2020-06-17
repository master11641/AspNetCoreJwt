
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(core.Areas.Identity.IdentityHostingStartup))]
namespace core.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<BloggingContext>(options =>                
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("coreIdentityDbContextConnection")));
                // services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                //     .AddEntityFrameworkStores<BloggingContext>();
            });
        }
    }
}