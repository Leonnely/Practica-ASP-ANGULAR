using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.DBContext;
using DAL.Repositorios.Contrato;
using Model;

namespace DAL.Repositorios
{
    public class VentaRepository : GenericRepository<Venta>, IVentaRepository
    {
        private readonly DbventaContext _dbventaContext;

        public VentaRepository(DbventaContext dbventaContext) : base(dbventaContext)
        {
        }

        public async Task<Venta> Registrar(Venta modelo)
        {
            Venta ventaGenerada = new Venta();
            
            //Transaccion
            using (var transaction = _dbventaContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (DetalleVenta dv in modelo.DetalleVenta)
                    {
                        Producto producto_encontrado = _dbventaContext.Productos.Where( p => p.IdProducto == dv.IdProducto ).First();
                        producto_encontrado.Stock = producto_encontrado.Stock - dv.Cantidad;
                        _dbventaContext.Productos.Update(producto_encontrado);

                    }
                    await _dbventaContext.SaveChangesAsync();

                    NumeroDocumento correlativo = _dbventaContext.NumeroDocumentos.First();

                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaRegistro = DateTime.Now;

                    _dbventaContext.NumeroDocumentos.Update(correlativo);
                    await _dbventaContext.SaveChangesAsync();

                    //generacion de codigo
                    int CantidadDigitos = 4;
                    string ceros = string.Concat(Enumerable.Repeat("0", CantidadDigitos));
                    string numeroVenta = ceros + correlativo.UltimoNumero.ToString();

                    numeroVenta = numeroVenta.Substring(numeroVenta.Length - CantidadDigitos,CantidadDigitos);

                    modelo.NumeroDocumento = numeroVenta;

                    await _dbventaContext.Venta.AddAsync(modelo);
                    await _dbventaContext.SaveChangesAsync();

                    ventaGenerada = modelo;

                    transaction.Commit();
                
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }

                return ventaGenerada;
            }

        }

    }
}
