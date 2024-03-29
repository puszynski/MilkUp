using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MilkUp.Areas.Identity;
using MilkUp.Data;
using MilkUp.Models;
using MilkUp.Repositories;
using MilkUp.Repositories.Interfaces;
using MilkUp.Services;
using MilkUp.ViewModels;
using MilkUp.ViewModels.Interfaces;

namespace MilkUp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //more info https://www.youtube.com/watch?v=3gJoO6RFVkQ&list=PL8h4jt35t1wjvwFnvcB2LlYL4jLRzRmoz&index=9

        // This method gets called by the runtime. Use this method to add services to the container / IoC.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("LocalConnection")));
            
            services.AddDefaultIdentity<ApplicationUser>(options => 
                { 
                    options.SignIn.RequireConfirmedAccount = false; 

                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 4;

                    options.User.RequireUniqueEmail = true;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddRazorPages();
            services.AddServerSideBlazor();
            //if you need y can expand maxiumum mesage send via signalR
            //services.AddServerSideBlazor().AddHubOptions(x => { x.MaximumReceiveMessageSize = 50000; })

            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<ApplicationUser>>();         
            services.AddScoped<ICowsViewModel, CowsViewModel>(); //AddScoped - one instance per user
            services.AddScoped<ISuperUserPanelViewModel, SuperUserPanelViewModel>();
            services.AddScoped<ISignInViewModel, SignInViewModel>();
            services.AddScoped<IAdminPanelViewModel, AdminPanelViewModel>();
            services.AddScoped<IAddCowViewModel, AddCowViewModel>();

            services.AddScoped<ICowRepository, CowRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ILactationRepository, LactationRepository>();
            services.AddScoped<ICowGroupRepository, CowGroupRepository>();
            services.AddScoped<IFarmRepository, FarmRepository>();

            services.AddScoped<ApplicationStateService>();
            services.AddScoped<CowDeleteService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline/ add middleweare - stuff between requests.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) //set  "ASPNETCORE_ENVIRONMENT": "Production" vs "Development"
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                //todo - undo!
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                //app.UseExceptionHandler("/Error");

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
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
