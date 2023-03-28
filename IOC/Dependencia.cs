using DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using DAL.Repositorios.Contrato;
using DAL.Repositorios;
using Utility;
using BLL.Servicios.Contrato;
using BLL.Servicios;

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
            service.AddAutoMapper(typeof(AutoMapperProfile));


            service.AddScoped<IRolService,RolService>();
            service.AddScoped<IUsuarioService, UsuarioService>();
            service.AddScoped<ICategoriaService,CategoriaService>();
            service.AddScoped<IProductoService, ProductoService>();
            service.AddScoped<IMenuService, MenuService>();
            service.AddScoped<IVentaService, VentaService>();
            service.AddScoped<IDashBoardService, DashBoardService>();
        }

    }
}
