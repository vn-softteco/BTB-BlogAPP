using System.Text;
using BlogApp.API;
using BlogApp.API.Middleware;
using BlogApp.Business;
using BlogApp.Business.Helpers;
using BlogApp.Business.Services.Auth;
using BlogApp.Business.Services.BlogPosts;
using BlogApp.Business.Services.Comments;
using BlogApp.DataModel;
using BlogApp.DataModel.Entities;
using BlogApp.DataModel.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var environment = builder.Environment;
var configuration = builder.Configuration;

var separator = Path.DirectorySeparatorChar;

configuration
	.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
	.AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
	.AddEnvironmentVariables();

services
	.AddDbContext<BlogAppDbContext>(options =>
		options
			.UseSqlServer(
				configuration.GetConnectionString("DefaultConnection"),
				s => s.MigrationsAssembly("BlogApp.DataModel"))
			.LogTo(s => System.Diagnostics.Debug.WriteLine(s))
			.EnableDetailedErrors(true)
			.EnableSensitiveDataLogging(true));

services
	.AddIdentity<User, Role>(options =>
	{
		// Default Password settings.
		options.Password.RequireDigit = true;
		options.Password.RequireLowercase = true;
		options.Password.RequireNonAlphanumeric = true;
		options.Password.RequireUppercase = true;
		options.Password.RequiredLength = 8;
		options.Password.RequiredUniqueChars = 1;
		options.User.RequireUniqueEmail = true;
	})
	.AddEntityFrameworkStores<BlogAppDbContext>()
	.AddDefaultTokenProviders();

services
	.AddAuthentication(options =>
	{
		options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
		options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	})
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidIssuer = configuration["Jwt:Issuer"],
			ValidAudience = configuration["Jwt:Audience"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = false,
			ValidateIssuerSigningKey = true
		};
	});

services.AddScoped<DbContext, BlogAppDbContext>();

services.AddAutoMapper(typeof(ApiAutoMapperProfile).Assembly);
services.AddAutoMapper(typeof(DtoAutoMapperProfile).Assembly);

services.AddTransient<IUserInfoProvider, UserInfoProvider>();
services.AddTransient<IBlogPostService, BlogPostService>();
services.AddTransient<ICommentService, CommentService>();
services.AddTransient<IAuthService, AuthService>();

services.AddHttpContextAccessor();

builder.Services.AddTransient<ApiCallsLoggingMiddleware>();
builder.Services.AddControllers();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo { Title = "BlogApp API", Version = "v1" });

	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Description = "JWT Authorization header using the Bearer scheme.",
		Type = SecuritySchemeType.Http,
		Scheme = "bearer"
	});

	options.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
			},
			new string[] { }
		}
	});

	options.EnableAnnotations();
});

services.Configure<RouteOptions>(options =>
{
	options.LowercaseUrls = true;
});
builder.Services.AddExceptionHandler<BlogAppExceptionHandler>();
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAllOrigins",
		policyBuilder =>
		{
			policyBuilder
				.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader();
		});
});

var app = builder.Build();

JWTHelper.Configure(configuration);

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<BlogAppDbContext>();
dbContext.Database.Migrate();

app.UseExceptionHandler(builder =>
{

});

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

app.UseMiddleware<ApiCallsLoggingMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
