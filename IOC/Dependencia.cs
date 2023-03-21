using DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Repositorios.Contrato;
using DAL.Repositorios;

namespace IOC
{
    public static class Dependencia
    {
        //Metodo de extension
        public static void InyectarDependencias(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<DbventaContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("cadenaSQL"));
            });


            service.AddTransient(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            service.AddScoped<IVentaRepository, VentaRepository>();
        }

    }
}
