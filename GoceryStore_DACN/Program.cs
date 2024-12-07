using GoceryStore_DACN.Data;
using GoceryStore_DACN.Entities;
using GoceryStore_DACN.Models;
using GoceryStore_DACN.Repositories;
using GoceryStore_DACN.Services;
using GoceryStore_DACN.Services.Interface;
using GroceryStore_DACN.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GoceryStore_DACN.Middlewares.Authentication;
using GoceryStore_DACN.Middlewares.Authorization;
using GoceryStore_DACN.Repositories.Interface;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; 
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });


//Cấu hình Identtiy cho dự án 
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = false; // Không yêu cầu chứa số 
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 6;
        options.SignIn.RequireConfirmedAccount = true;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
//Config Authentication
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer ?? "https://localhost:5000",
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey ?? "dE15Vb9DmPP6gYlmr5FanlB/PBz3l2tuahjOLuSn+HI="))
        };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMemoryCache();
//Config Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Config Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Grocery Store API",
        Description = "An ASP.NET Core Web API for managing grocery store items",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Nguyễn Phúc Đạt",
            Email = "nguyenphucdat@gmail.com",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Use under LICX",
            Url = new Uri("https://example.com/license")
        }
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter into field the word 'Bearer' following by space and JWT",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
//Config CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

//Config Auto Mapper
builder.Services.AddAutoMapper(typeof(Program));



// Register configuration settings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

// Register Repositories
builder.Services.AddScoped<ICheDoAnRepository, CheDoAnRepository>();
builder.Services.AddScoped<IChiTietBuoiAnRepository, ChiTietBuoiAnRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IThucPhamRepository, ThucPhamRepository>();
builder.Services.AddScoped<IMonAnRepository, MonAnRepository>();
builder.Services.AddScoped<ILoaiThucPhamRepository, LoaiThucPhamRepository>();
builder.Services.AddScoped<ILoaiMonAnRepository, LoaiMonAnRepository>();
builder.Services.AddScoped<IThanhPhanDinhDuongRepository, ThanhPhanDinhDuongRepository>();

// Register Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IThucPhamServices, ThucPhamsService>();
builder.Services.AddScoped<ICT_BuoiAnServices,CT_BuoiAnService>();
builder.Services.AddScoped<IMonAnServices, MonAnService>();
builder.Services.AddScoped<ILoaiThucPhamServices, LoaiThucPhamService>();
builder.Services.AddScoped<ILoaiMonAnServices, LoaiMonAnService>();
builder.Services.AddScoped<IThucDonTuanService, ThucDonTuanService>();
builder.Services.AddScoped<ICheDoAnServices, CheDoAnService>();
builder.Services.AddScoped<IThanhPhanDinhDuongServices, ThanhPhanDinhDuongService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IEmailTemplateService, EmailTemplateService>();
builder.Services.AddScoped<IUploadService, UploadService>();
builder.Services.AddScoped<IHoaDonService, HoaDonService>();
builder.Services.AddScoped<IUserContextService, UserContextService>();

// Configure Cloudinary
var cloudinaryAccount = new Account(
    builder.Configuration["CloudinarySettings:CloudName"],
    builder.Configuration["CloudinarySettings:ApiKey"],
    builder.Configuration["CloudinarySettings:ApiSecret"]);
var cloudinary = new Cloudinary(cloudinaryAccount);
builder.Services.AddSingleton(cloudinary);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Seeder Data
//HinhThucThanhToanSeeder.SeedData(app);

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.Use(async (context, next) =>
{
    Console.WriteLine($"Request Path: {context.Request.Path}");
    Console.WriteLine($"Request Method: {context.Request.Method}");
    Console.WriteLine($"Authorization Header: {context.Request.Headers["Authorization"]}");

    await next();

    Console.WriteLine($"Response Status Code: {context.Response.StatusCode}");
});
/*app.UseMiddleware<CustomAuthenticationMiddleware>(); 
app.UseMiddleware<CustomAuthorizationMiddleware>();*/
app.UseAuthentication();
app.Use(async (context, next) =>
{
    await next();

    // Kiểm tra trạng thái lỗi nếu người dùng không được xác thực (401)
    if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
    {
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync("{\"error\": \"Unauthorized - You must be logged in to access this resource\"}");
    }

    // Kiểm tra trạng thái lỗi nếu người dùng không có quyền truy cập (403)
    if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
    {
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync("{\"error\": \"Forbidden - You do not have permission to access this resource\"}");
    }
});
app.UseAuthorization();
app.MapControllers();
app.Run();
