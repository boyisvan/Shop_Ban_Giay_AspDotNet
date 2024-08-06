using Microsoft.EntityFrameworkCore;
using ShoeStore.Data;

var builder = WebApplication.CreateBuilder(args);

// Thêm dịch vụ cho DbContext sử dụng SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Đặt trang đăng nhập làm trang mặc định khi khởi động
//app.MapGet("/", context => Task.Run(() => context.Response.Redirect("/Account/Login")));

app.MapRazorPages();

app.Run();
