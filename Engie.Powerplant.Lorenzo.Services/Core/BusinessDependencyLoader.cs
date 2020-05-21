using Microsoft.Extensions.DependencyInjection;
using Engie.Powerplant.Lorenzo.Business.Interfaces;
using Engie.Powerplant.Lorenzo.Business.Services;

namespace Engie.Powerplant.Lorenzo.Business.Core
{
    public static class BusinessDependencyLoader
    {
        public static void LoadDependencies(IServiceCollection services)
        {
            services.AddSingleton<IProductionplanService, ProductionplanService>();
        }
    }
}
