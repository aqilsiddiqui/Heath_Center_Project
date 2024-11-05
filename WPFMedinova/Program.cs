using Microsoft.EntityFrameworkCore;
using WPFMedinova.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<AccountDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddSession();

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

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;



app.UseSession();

//builder.Services.AddDistributedMemoryCache();
//builder.Services.Configure<CookiePolicyOptions>(options =>
//{
//    // This lambda determines whether user consent for non-essential cookies is needed for a given request.  
//    options.CheckConsentNeeded = context => false;
//    options.MinimumSameSitePolicy = SameSiteMode.None;
//});


//builder.Services.AddSession(options =>
//{
//    //options.Cookie.Name = "ephr";  
//    options.IdleTimeout = TimeSpan.FromMinutes(10);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});







app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
