using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using web.Data;
using web.Models;

var builder = WebApplication.CreateBuilder(args);

var GetConnectionString = builder.Configuration.GetConnectionString("ShopContext");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<web.Data.ShopContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("AzureContext")));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ShopContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;
app.MapRazorPages();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
