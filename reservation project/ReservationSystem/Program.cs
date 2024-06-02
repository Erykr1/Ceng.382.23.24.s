using Microsoft.EntityFrameworkCore;
using ReservationSystem.Data;

var builder = WebApplication.CreateBuilder(args);

// Servisleri container'a ekle.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ReservationContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Oturum yönetimi ekle
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Oturum zaman aşım süresi
    options.Cookie.HttpOnly = true;                  // Cookie sadece HTTP tarafından erişilebilir
    options.Cookie.IsEssential = true;               // Cookie esastır
});

// IHttpContextAccessor ekle
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// HTTP istek yönlendirmesini yapılandır.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");  // Hata yönetimi
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Oturum kullanımını etkinleştir
app.UseSession();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
