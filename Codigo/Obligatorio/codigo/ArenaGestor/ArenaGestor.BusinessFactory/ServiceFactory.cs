using ArenaGestor.Business;
using ArenaGestor.Business.Helpers;
using ArenaGestor.BusinessInterface;
using Microsoft.Extensions.DependencyInjection;

namespace ArenaGestor.BusinessFactory
{
    public class ServiceFactory
    {
        private readonly IServiceCollection services;

        public ServiceFactory(IServiceCollection services)
        {
            this.services = services;
            AddCustomServices();
        }

        private void AddCustomServices()
        {
            services.AddScoped<ICountrysService, CountrysService>();
            services.AddScoped<IGendersService, GendersService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IArtistsService, ArtistsService>();
            services.AddScoped<IBandsService, BandsService>();
            services.AddScoped<ISoloistsService, SoloistsService>();
            services.AddScoped<IMusicalProtagonistService, MusicalProtagonistService>();
            services.AddScoped<IConcertsService, ConcertsService>();
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<IImportExportService, ImportExportService>();
            services.AddSingleton<IReflectionHelpers, ReflectionHelpers>();
        }
    }
}
