using ArenaGestor.DataAccess;
using ArenaGestor.DataAccess.Managements;
using ArenaGestor.DataAccessInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ArenaGestor.DataAccessFactory
{
    public class ManagementFactory
    {
        private readonly IServiceCollection services;

        public ManagementFactory(IServiceCollection services)
        {
            this.services = services;
            AddCustomManagement();
        }

        private void AddCustomManagement()
        {
            services.AddScoped<IGendersManagement, GendersManagement>();
            services.AddScoped<IUsersManagement, UsersManagement>();
            services.AddScoped<IArtistsManagement, ArtistsManagement>();
            services.AddScoped<IBandsManagement, BandsManagement>();
            services.AddScoped<ISoloistsManagement, SoloistsManagement>();
            services.AddScoped<IMusicalProtagonistManagement, MusicalProtagonistManagement>();
            services.AddScoped<IConcertsManagement, ConcertsManagement>();
            services.AddScoped<ISessionManagement, SessionManagement>();
            services.AddScoped<ITicketManagement, TicketManagement>();
            services.AddScoped<ITicketStatusManagement, TicketStatusManagement>();
            services.AddScoped<ICountrysManagement, CountrysManagement>();
            services.AddScoped<IRolesManagement, RolesManagement>();
        }

        public void AddDbContextService(string connectionString)
        {
            services.AddDbContext<DbContext, ArenaGestorContext>(options => options.UseSqlServer(connectionString), optionsLifetime: ServiceLifetime.Scoped);
        }
    }
}
