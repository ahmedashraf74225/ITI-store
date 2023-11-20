using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Online_ITI_Book_Store.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Online_ITI_Book_StoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Online_ITI_Book_StoreContext") ?? throw new InvalidOperationException("Connection string 'Online_ITI_Book_StoreContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
//session
builder.Services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(1); });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();
    

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=useraccounts}/{action=login}/{id?}");

app.Run();
