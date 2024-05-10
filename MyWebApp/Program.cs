using Hangfire;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddCors();

            builder.Services.AddHangfire((opt) =>
            {

            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            GlobalConfiguration.Configuration.UseSqlServerStorage("Server=JINPC;Database=HangfireSample;Trusted_Connection=True;");

            //// Change `Back to site` link URL
            //var options = new DashboardOptions { AppPath = "http://your-app.net" };
            //// Make `Back to site` link working for subfolder applications
            //var options = new DashboardOptions { AppPath = VirtualPathUtility.ToAbsolute("~") };

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new MyHangfireAuthorizationFilter() }
            });

            app.UseCors((opt) =>
            {
                opt
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
            });

            app.MapRazorPages();

            app.Run();
        }
    }

    public class MyHangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            //return httpContext.User.Identity?.IsAuthenticated ?? false;

            // check more perrmisison here

            return true;
        }
    }
}
