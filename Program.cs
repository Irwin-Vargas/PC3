using Microsoft.EntityFrameworkCore;
using NewsPortalMVC.Rest;
using NewsPortalMVC.Services;

var builder = WebApplication.CreateBuilder(args);

// ✅ Conexión a PostgreSQL
builder.Services.AddDbContext<FeedbackContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Servicios HTTP para consumir JSONPlaceholder
builder.Services.AddHttpClient<PostService>();
builder.Services.AddHttpClient<UserService>();
builder.Services.AddHttpClient<CommentService>();

// ✅ MVC y API Controllers
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

var app = builder.Build();

// ✅ Middleware para errores y seguridad
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// ✅ Rutas para MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ✅ Rutas para API REST (Feedback)
app.MapControllers();

app.Run();
