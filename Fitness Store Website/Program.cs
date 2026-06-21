using Fitness_Store_Website.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Fitness_Store_Website
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireNonAlphanumeric=false;
                options.Password.RequireUppercase=false;
                options.Password.RequireLowercase=false;
                options.Password.RequireDigit=false;

            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            
            builder.Services.AddControllersWithViews();
            builder.Services.AddControllers();
            // Add session support for shopping cart
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
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

            app.UseSession();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            // Seed admin role and user (only if enabled in configuration)
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

                var enableSeed = app.Configuration.GetValue<bool>("AdminSeed:Enable");
                if (enableSeed)
                {
                    var adminRole = roleManager.FindByNameAsync("Admin").Result;
                    if (adminRole == null)
                    {
                        roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
                    }

                    var adminEmail = app.Configuration.GetValue<string>("AdminSeed:Email") ?? "admin@fitness.local";
                    var adminPassword = app.Configuration.GetValue<string>("AdminSeed:Password") ?? "Admin123!";
                    var adminUser = userManager.FindByEmailAsync(adminEmail).Result;
                    if (adminUser == null)
                    {
                        adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
                        userManager.CreateAsync(adminUser, adminPassword).Wait();
                        userManager.AddToRoleAsync(adminUser, "Admin").Wait();
                    }
                }
                // Ensure database is up to date: apply migrations if present, otherwise fallback to create tables for development
                var db = services.GetRequiredService<ApplicationDbContext>();
                try
                {
                    db.Database.Migrate();
                }
                catch
                {
                    // If migrations cannot be applied, attempt to create missing tables as a fallback (development only)
                    try
                    {
                        var createOrders = @"
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Orders]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Orders](
        [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [UserId] NVARCHAR(450) NOT NULL,
        [CreatedOn] DATETIME2 NOT NULL,
        [Total] DECIMAL(18,2) NOT NULL,
        [PaymentMethod] NVARCHAR(100) NOT NULL,
        [PaymentStatus] NVARCHAR(100) NOT NULL
    );
END
";

                        var createOrderItems = @"
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderItems]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[OrderItems](
        [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [OrderId] INT NOT NULL,
        [ProductId] INT NOT NULL,
        [ProductName] NVARCHAR(200) NOT NULL,
        [UnitPrice] DECIMAL(18,2) NOT NULL,
        [Quantity] INT NOT NULL,
        CONSTRAINT FK_OrderItems_Orders FOREIGN KEY (OrderId) REFERENCES [dbo].[Orders](Id) ON DELETE CASCADE
    );
END
";

                        db.Database.ExecuteSqlRaw(createOrders);
                        db.Database.ExecuteSqlRaw(createOrderItems);
                    }
                    catch
                    {
                        // ignore SQL creation errors in case database provider is different or permissions missing
                    }
                }
            }

            app.Run();
        }
    }
}
