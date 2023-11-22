using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Lab4_1.ModelVIew
{
    public class Paginador
    {
        public int PaginaActual { get; set; }
        public int TotalRegistros { get; set; }
        public int RegistrosPorPagina { get; set; }
        public int TotalPaginas => (int)Math.Ceiling((decimal)TotalRegistros / RegistrosPorPagina);

        public Dictionary<string, string> ValoresQueryString { get; set; } = new Dictionary<string, string>();
    }
}
