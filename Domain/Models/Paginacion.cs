using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Paginacion
    {
        public int NumeroPagina { get; set; }
        public int RegistrosPagina { get; set; }
        public int NumeroPaginas { get; set; }
        public int Filter { get; set; }
    }
}
