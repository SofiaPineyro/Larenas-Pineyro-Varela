using ArenaGestor.Business;
using ArenaGestor.BusinessInterface;
using ArenaGestor.DataAccess;
using ArenaGestor.DataAccess.Managments;
using ArenaGestor.DataAccessInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ArenaGestor.Factory.Factories
{
    public class ServiceFactory
    {
        private readonly IServiceCollection services;

        public ServiceFactory(IServiceCollection services)
        {
            this.services = services;
            AddCustomServices();
            AddCustomManagment();
        }

        /// <summary>
        /// Business
        /// </summary>
        private void AddCustomServices()
        {
            services.AddScoped<IGendersService, GendersService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IArtistsService, ArtistsService>();
            services.AddScoped<IBandsService, BandsService>();
            services.AddScoped<ISoloistsService, SoloistsService>();
        }

        /// <summary>
        /// /Data access
        /// </summary>
        private void AddCustomManagment()
        {
            services.AddScoped<IGendersManagment, GendersManagment>();
            services.AddScoped<IUsersManagment, UsersManagment>();
            services.AddScoped<IArtistsManagment, ArtistsManagment>();
            services.AddScoped<IBandsManagment, BandsManagment>();
            services.AddScoped<ISoloistsManagment, SoloistsManagment>();
        }

        public void AddDbContextService(string connectionString)
        {
            services.AddDbContext<DbContext, ArenaGestorContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
