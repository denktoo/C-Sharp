using Microsoft.EntityFrameworkCore;
using JkuatUniversity.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Registering the DbContext with MySQL configuration
builder.Services.AddDbContext<JkuatContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("JkuatContext"),
                     new MySqlServerVersion(new Version(8, 0))));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();