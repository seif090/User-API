using Microsoft.EntityFrameworkCore;
using User.Application.Services;
using User.Core.Interfaces;
using User.Infrastructure.Data;
using User.Infrastructure.Repositories;

namespace User.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
