using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Data.Services;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEstateService, EstateService>();
builder.Services.AddScoped<IBankingService, BankingService>();
builder.Services.AddScoped<INotifyService, NotifyService>();
builder.Services.AddSingleton(builder.Configuration);


builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnectionSQLite")
        );
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers().AddJsonOptions(options =>
    {
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    }
);

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie("Cookies", options =>
           {
               options.LoginPath = "/home/login";
               options.ExpireTimeSpan = TimeSpan.FromHours(1);
           });



builder.Services.AddMvcCore();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("v1/swagger.json", "MyAPI V1");
    options.DocExpansion(DocExpansion.None);
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});



app.Run();
