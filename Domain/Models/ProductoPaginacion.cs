using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ProductoPaginacion
    {
        public Paginacion Paginacion { get; set; }
        public List<Producto> Producto { get; set; }
    }
}
