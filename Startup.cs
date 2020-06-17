using System.Text;
using Core.Helpers;
using Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace core {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddDbContext<BloggingContext> ();
            services.AddMvc (option => option.EnableEndpointRouting = false).AddRazorPagesOptions (options => {
             
            });

            // services.AddMvc (option => option.EnableEndpointRouting = false).AddRazorPagesOptions (options => {
            //     //options.AllowAreas = true;
            //     options.Conventions.AuthorizeAreaFolder ("Identity", "/Account/Manage");
            //     options.Conventions.AuthorizeAreaPage ("Identity", "/Account/Logout");
            // });
            // services.AddIdentity<IdentityUser, IdentityRole> ()
            //     // services.AddDefaultIdentity<IdentityUser>()
            //     .AddEntityFrameworkStores<BloggingContext> ()
            //     .AddDefaultTokenProviders ();
            // services.ConfigureApplicationCookie (options => {
            //     options.LoginPath = $"/Identity/Account/Login";
            //     options.LogoutPath = $"/Identity/Account/Logout";
            //     options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            // });
            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection ("AppSettings");
            services.Configure<AppSettings> (appSettingsSection);
            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings> ();
            var key = Encoding.ASCII.GetBytes (appSettings.Secret);
            services.AddAuthentication (x => {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer (x => {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey (key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            // configure DI for application services
            services.AddScoped<IUserService, UserService> ();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseExceptionHandler ("/Home/Error");
            }
            // global cors policy
            app.UseCors (x => x
                .AllowAnyOrigin ()
                .AllowAnyMethod ()
                .AllowAnyHeader ());
            app.UseStaticFiles ();
            app.UseAuthentication ();
            app.UseMvc (routes => {
               //  routes.MapRoute ("areaRoute", "{area:exists}/{controller=Users}/{action=Index}/{id?}");
                routes.MapRoute (
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseRouting();
        }
    }
}