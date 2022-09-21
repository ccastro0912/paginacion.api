using Domain.Models;
using System;
using System.Collections.Generic;

namespace Domain.Contracts.Repositories
{
    public interface IRepositorieProducto<out T> : IDisposable where T: class
    {
        T GetAll(Paginacion p);
    }
}
