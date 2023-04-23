using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Data.Services;
// using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerUI;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TestTask.Data.Static;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEstateService, EstateService>();
builder.Services.AddScoped<IBankingService, BankingService>();
builder.Services.AddScoped<INotifyService, NotifyService>();
builder.Services.AddSingleton(builder.Configuration);


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromHours(12);
            options.Cookie.HttpOnly = true; // Set HttpOnly flag on session cookie
            options.Cookie.Name = "TestTask"; 
            options.Cookie.IsEssential = true;
        })
        ;


builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnectionSQLite")
        );
    // options.UseMySql(
    //     builder.Configuration.GetConnectionString("DefaultConnectionMySQL"), 
    //     ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnectionMySQL"))
    //     );
});

builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
    {
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    }
);


builder.Services.AddAuthorization();
// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
// })
//     .AddJwtBearer(options =>
//     {
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = true,
//             ValidIssuer = AuthOptions.ISSUER,
//             ValidateAudience = true,
//             ValidAudience = AuthOptions.AUDIENCE,
//             ValidateLifetime = true,
//             IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
//             ValidateIssuerSigningKey = true
//          };
//     }
// );
builder.Services.AddAuthentication(options =>
            {
                // custom scheme defined in .AddPolicyScheme() below
                options.DefaultScheme = "JWT_OR_COOKIE";
                options.DefaultChallengeScheme = "JWT_OR_COOKIE";
            })
           // Adding Jwt Bearer  
           .AddJwtBearer(options =>
           {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = AuthOptions.ISSUER,
                    ValidateAudience = true,
                    ValidAudience = AuthOptions.AUDIENCE,
                    ValidateLifetime = true,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true
                };
           }).AddCookie("Cookies", options =>
           {
               options.LoginPath = "/account/login";
               options.ExpireTimeSpan = TimeSpan.FromDays(1);
           }).AddPolicyScheme("JWT_OR_COOKIE", "JWT_OR_COOKIE", options =>
           {
               // runs on each request
               options.ForwardDefaultSelector = context =>
               {
                   // filter by auth type
                   string authorization = context.Request.Headers[HeaderNames.Authorization];
                   if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
                       return "Bearer";

                   // otherwise always check for cookie auth
                   return "Cookies";
               };
});





// builder.Services

builder.Services.AddMvcCore();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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
app.UseSession();



app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("v1/swagger.json", "MyAPI V1");
    options.DocExpansion(DocExpansion.None);
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});




app.Run();



