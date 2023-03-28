using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using DTO;
using Model;


namespace Utility
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            #region Rol
            CreateMap<Rol, RolDTO>().ReverseMap();
            #endregion

            #region Menu
            CreateMap<Menu, MenuDTO>().ReverseMap();
            #endregion

            #region Usuario
            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(destino =>
                    destino.RolDescripcion,
                    option => option.MapFrom(origen => origen.IdRolNavigation.Nombre)
                ).ForMember(destino =>
                    destino.EsActivo,
                    option => option.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                );


            CreateMap<Usuario, SesionDTO>()
                .ForMember(destino =>
                    destino.RolDescripcion,
                    option => option.MapFrom(origen => origen.IdRolNavigation.Nombre)
                );

            CreateMap<UsuarioDTO, Usuario>()
                .ForMember(destino =>
                    destino.IdRolNavigation,
                    option => option.Ignore()
                ).ForMember(destino =>
                    destino.EsActivo,
                    option => option.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                ); ;
            #endregion

            #region Categoria
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            #endregion

            #region Producto
            CreateMap<Producto, ProductoDTO>()
                .ForMember(destino => destino.DescripcionCategoria,
                    option => option.MapFrom(origen => origen.IdCategoriaNavigation.Nombre)
                )
                .ForMember(destino => destino.Precio,
                    option => option.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-AR")))
                )
                .ForMember(destino =>
                    destino.EsActivo,
                    option => option.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                );

            CreateMap<ProductoDTO, Producto>()
                .ForMember(destino => destino.IdCategoriaNavigation,
                    option => option.Ignore()
                )
                .ForMember(destino => destino.Precio,
                    option => option.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-AR")))
                )
                .ForMember(destino =>
                    destino.EsActivo,
                    option => option.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                );
            #endregion

            #region Venta
            CreateMap<Venta, VentaDTO>()
                .ForMember(destino => destino.TotalTexto, option => option.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-AR")))
                )
                .ForMember(destino => destino.FechaRegistro, option => option.MapFrom(origen => origen.FechaRegistro.Value.ToString("dd/MM/yyyy"))
                );

            CreateMap<VentaDTO, Venta>()
                .ForMember(destino => destino.Total, option => option.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-AR")))
                );
            #endregion

            #region DetalleVenta
            CreateMap<DetalleVenta, DetalleVentaDTO>()
                .ForMember(destino =>
                    destino.DescripcionProducto,
                    option => option.MapFrom(origen => origen.IdProductoNavigation.Nombre)
                )
                .ForMember(destino => destino.PrecioTexto,
                    option => option.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-AR")))
                )
                .ForMember(destino => destino.TotalTexto,
                    option => option.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-AR")))
                );


            CreateMap<DetalleVentaDTO, DetalleVenta>()
                .ForMember(destino => destino.Precio,
                    option => option.MapFrom(origen => Convert.ToString(origen.PrecioTexto, new CultureInfo("es-AR")))
                )
                .ForMember(destino => destino.Total,
                    option => option.MapFrom(origen => Convert.ToString(origen.TotalTexto, new CultureInfo("es-AR")))
                );


            #endregion

            #region Reporte
            CreateMap<DetalleVenta, ReporteDTO>()
                .ForMember(destino => destino.FechaRegistro, option => option.MapFrom(origen => origen.IdVentaNavigation.FechaRegistro.Value.ToString("dd/MM/yyyy"))
                )
                .ForMember(destino => destino.NumeroDocumento, option => option.MapFrom(origen => origen.IdVentaNavigation.NumeroDocumento)
                )
                .ForMember(destino => destino.TipoPago, option => option.MapFrom(origen => origen.IdVentaNavigation.TipoPago)
                )
                .ForMember(destino => destino.TotalVenta, option => option.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.Total.Value, new CultureInfo("es-AR")))
                )
                .ForMember(destino => destino.Producto, option => option.MapFrom(origen => origen.IdProductoNavigation.Nombre)
                )
                .ForMember(destino => destino.Precio, option => option.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-AR")))
                )
                .ForMember(destino => destino.Total, option => option.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-AR")))
                );

            #endregion
        }
    }
}
