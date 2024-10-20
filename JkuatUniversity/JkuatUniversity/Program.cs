using Microsoft.EntityFrameworkCore;
using JkuatUniversity.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add logging services if needed
builder.Services.AddLogging();

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

// Map controller routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Initialize the database with seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<JkuatContext>();
    JkuatInitializer.Initialize(context);
}

app.Run();