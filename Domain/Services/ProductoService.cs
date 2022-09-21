using Domain.Contracts.Repositories;
using Domain.Contracts.Services;
using Domain.Models;
using System;
using System.Collections.Generic;

namespace Domain.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepositorie _productoRepositorie;
        public ProductoService(IProductoRepositorie productoRepositorie)
        {
            _productoRepositorie = productoRepositorie;
        }
        public ProductoPaginacion GetAll(Paginacion p)
        {
            return _productoRepositorie.GetAll(p);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
