using Business.Abstract;
using Business.Concrete;
using Core.Helper;
using Core.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Hosting;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5000);
    serverOptions.ListenAnyIP(5001, listenOptions =>
    {
        listenOptions.UseHttps();
    });
});
builder.Services.AddControllers();

// Add JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["TokenOptions:Issuer"],
            ValidAudience = builder.Configuration["TokenOptions:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenOptions:SecurityKey"]))
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<IProductDal, EfProductDal>();
builder.Services.AddScoped<ICategoryDal, EfCategoryDal>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<IProductImageDal, EfProductImageDal>();
builder.Services.AddScoped<IProductImageService, ProductImagesManager>();
builder.Services.AddScoped<IUserDal, EfUserDal>();
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IAuthService, AuthManager>();
builder.Services.AddScoped<IDistrictService, DistrictManager>();
builder.Services.AddScoped<ICityService, CityManager>();
builder.Services.AddScoped<INeighborhoodService, NeighborhoodManager>();

builder.Services.AddScoped<ICityDal, EfCityDal>();
builder.Services.AddScoped<IDistrictDal, EfDistrict>();
builder.Services.AddScoped<INeighborhoodDal, EfNeighborhood>();

builder.Services.AddScoped<ITokenHelper, JwtHelper>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseStaticFiles();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

// Add Authentication middleware
app.UseAuthentication();  // This will add authentication to the pipeline
app.UseAuthorization();
var env = app.Services.GetRequiredService<IHostEnvironment>();

ImageUploader.Initialize(env);
app.MapControllers();

app.Run();
