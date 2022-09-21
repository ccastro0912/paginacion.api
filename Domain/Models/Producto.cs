using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Producto
    {
        public int PKID { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public string FechaCreacion { get; set; }
    }
}
