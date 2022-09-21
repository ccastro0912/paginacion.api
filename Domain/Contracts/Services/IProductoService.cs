using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Contracts.Services
{
    public interface IProductoService : IDisposable
    {
        ProductoPaginacion GetAll(Paginacion p);
    }
}
