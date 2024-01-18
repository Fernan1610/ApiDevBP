using ApiDevBP.Data;
using ApiDevBP.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SQLite;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using ApiDevBP.Entities;
namespace ApiDevBP
{
    public static class ProgramsExtensions
    {
        public static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
                 options.UseSqlite(configuration.GetConnectionString("DefaultConnection"))
            );
        }
        public static void InjectDependencies(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<SQLiteConnection>(_ =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                return new SQLiteConnection(connectionString);
            });
            services.AddScoped<UserProfile>();




        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {

            var mappingConfig = new MapperConfiguration(mc =>
            {
               
                mc.AddProfile(new UserProfile());
            });
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

        }
    }
}
