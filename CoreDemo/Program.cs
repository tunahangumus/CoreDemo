using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc(config =>
{
	var policy = new AuthorizationPolicyBuilder()
					.RequireAuthenticatedUser()
					.Build();
	config.Filters.Add(new AuthorizeFilter(policy));
});
builder.Services.AddHttpClient("BlogApi", (cfg) =>
{
    cfg.BaseAddress = new Uri("https://localhost:7071/");
});
builder.Services.AddHttpClient("AdminApi", (cfg) =>
{
    cfg.BaseAddress = new Uri("https://localhost:7071/");
});

builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<ICategoryDal, EfCategoryRepository>();



builder.Services.AddMvc();
builder.Services.AddDbContext<Context>();
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddIdentity<AppUser, AppRole>(options =>
	options.Password.RequireNonAlphanumeric = false
	).AddErrorDescriber<CustomPasswordValidation>().AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider).AddUserValidator<CustomUserValidation>().AddEntityFrameworkStores<Context>();



builder.Services.ConfigureApplicationCookie(options =>
{
	options.LoginPath = new PathString("/Login/Login");
	options.AccessDeniedPath = new PathString("/Login/Login");

	options.Cookie = new()
	{
		Name = "IdentityCookie",
		HttpOnly = true,
		SameSite = SameSiteMode.Lax,
		SecurePolicy = CookieSecurePolicy.Always
	};
	options.SlidingExpiration = true;
	options.ExpireTimeSpan = TimeSpan.FromMinutes(2);
});


builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(5); 
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/ErrorPage/Error1","?code={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseCookiePolicy();


app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Blog}/{action=Index}/{id?}");

app.Run();
