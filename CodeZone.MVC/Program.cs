using CodeZone.BLL.Extensions;
using CodeZone.DAL.Extensions;
using CodeZone.DAL.Persistence;
using CodeZone.DAL.Seed;

namespace CodeZone.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDataAccessLayer();
            builder.Services.AddBusinessLogicLayer();
            var app = builder.Build();

            // Seed database
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                DbInitializer.Seed(context);
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Warehouse}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
