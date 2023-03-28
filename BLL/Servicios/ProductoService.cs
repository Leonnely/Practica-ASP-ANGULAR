using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using BLL.Servicios.Contrato;
using DAL.Repositorios.Contrato;
using DTO;
using Microsoft.EntityFrameworkCore;
using Model;

namespace BLL.Servicios
{
    public class ProductoService : IProductoService
    {
        private readonly IGenericRepository<Producto> _productoRepository;
        private readonly IMapper _mapper;

        public ProductoService(IGenericRepository<Producto> productoRepository, IMapper mapper)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductoDTO>> Lista()
        {
            try
            {
                var queryProducto = await _productoRepository.Consultar();
                var listaProductos = queryProducto.Include(categoria => categoria.IdCategoriaNavigation).ToList();

                return _mapper.Map<List<ProductoDTO>>(listaProductos.ToList());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductoDTO> Crear(ProductoDTO modelo)
        {
            try
            {
                //Recibo un DTO por lo tanto debo de utilizar mapper para mandar solo 'Producto'
                var productoCreado = await _productoRepository.Crear(_mapper.Map<Producto>(modelo));
                if (productoCreado.IdProducto == 0)
                {
                    throw new TaskCanceledException("No pudo crearse");
                }
                return _mapper.Map<ProductoDTO>(productoCreado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Editar(ProductoDTO modelo)
        {
            try
            {
                var productoModelo = _mapper.Map<Producto>(modelo);
                var productoEncontrado = await _productoRepository.Obtener(u => u.IdProducto == productoModelo.IdProducto);

                if (productoEncontrado == null)
                {
                    throw new TaskCanceledException("no existe");
                }

                productoEncontrado.Nombre = productoModelo.Nombre;
                productoEncontrado.IdCategoria = productoModelo.IdCategoria;
                productoEncontrado.Stock = productoModelo.Stock;
                productoEncontrado.Precio = productoModelo.Precio;
                productoEncontrado.EsActivo = productoModelo.EsActivo;

                bool response = await _productoRepository.Editar(productoEncontrado);

                if (response)
                    throw new TaskCanceledException("No se pudo editar");

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var productoEncontrado = await _productoRepository.Obtener(p => p.IdProducto == id);
                
                if(productoEncontrado == null)
                    throw new TaskCanceledException("error");

                bool response = await _productoRepository.Eliminar(productoEncontrado);

                if (response)
                    throw new TaskCanceledException("No se pudo eliminar");

                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
