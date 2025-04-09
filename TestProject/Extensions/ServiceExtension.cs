using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TestProject.BaseService.IServices;
using TestProject.BaseService.Services;
using TestProject.Data.Models.Entities;
using TestProject.Data.Models.Entities.Roles;
using TestProject.Data.Repositories;

namespace TestProject.Extensions
{
    public static class ServiceExtension
    {
        public static void AddCustomExtenstion(this IServiceCollection Services)
        {
            Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

            Services.AddScoped<IRepository<Role, int>, Repository<Role, int>>();
            Services.AddScoped<IRepository<UserRole, int>, Repository<UserRole, int>>();
            Services.AddScoped<IRepository<User, Guid>, Repository<User, Guid>>();

            Services.AddScoped<IUserService, UserService>();
            Services.AddScoped<IAuthService, AuthService>();
            Services.AddScoped<IGroupService, GroupService>();
            Services.AddScoped<IStudentService, StudentService>();
            Services.AddScoped<IStudentGroupService, StudentGroupService>();
            Services.AddScoped<IRoleService, RoleService>();

            Services.AddHttpContextAccessor();
        }

        public static void AddSwaggerService(this IServiceCollection Services)
        {
            Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestProject", Version = "v1" });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                new string[]{ }
            }
        });
            });

        }

        public static void AddJwtService(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var jwtSettings = configuration.GetSection("JWT");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings["Key"])
                    ),
                    ClockSkew = TimeSpan.Zero
                };
            });


        }
    }
}
