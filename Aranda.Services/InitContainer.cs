using Aranda.Data;
using Aranda.Services.Profiles;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Aranda.Services
{
    /// <summary>
    /// Contenedor de repositorios y servicios
    /// </summary>
    public static class InitContainer
    {
        /// <summary>
        /// Contenedor de repositorios y servicios
        /// </summary>
        /// <param name="services"></param>
        public static void AddServices(IServiceCollection services)
        {
            //AutoMapper
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MenuProfile());
                cfg.AddProfile(new RolMenuProfile());
                cfg.AddProfile(new RolProfile());
                cfg.AddProfile(new UserAppProfile());
            });

            IMapper mapper = configuration.CreateMapper();
            services.AddSingleton(mapper);

            ///Repositorios
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IRolMenuRepository, RolMenuRepository>();
            services.AddScoped<IRolRepository, RolRepository>();
            services.AddScoped<IUserAppRepository, UserAppRepository>();
            //Servicios
            services.AddScoped<IMenuServices, MenuServices>();
            services.AddScoped<IRolMenuServices, RolMenuServices>();
            services.AddScoped<IRolServices, RolServices>();
            services.AddScoped<IUserAppServices, UserAppServices>();
        }
    }
}