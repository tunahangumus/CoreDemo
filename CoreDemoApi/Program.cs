using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using CoreDemoApi.Models;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBlogService,BlogManager >();
builder.Services.AddScoped<IBlogDal,EfBlogRepository>();
builder.Services.AddScoped<ICategoryService,CategoryManager>();
builder.Services.AddScoped<ICategoryDal,EfCategoryRepository>();
builder.Services.AddScoped<ICommentService,CommentManager>();
builder.Services.AddScoped<ICommentDal,EfCommentRepository>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper();
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
