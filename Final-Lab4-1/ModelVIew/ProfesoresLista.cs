using Final_Lab4_1.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Lab4_1.ModelVIew
{
    public class ProfesoresLista
    {    
            public List<Profesor> profesores { get; set; }
            public SelectList turnos { get; set; }
            public string BusquedaNombre { get; set; }
            public int? TurnoId{ get; set; }
            public Paginador paginador { get; set; }
    }
}
