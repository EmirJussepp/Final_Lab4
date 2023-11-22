using Final_Lab4_1.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Lab4_1.ModelVIew
{
    public class AlumnosLIsta
    {
        public List<Alumno> alumnos { get; set; }
        public SelectList ListProfresores { get; set; }
        public string BusquedaNombre { get; set; }
        public int? ProfesorId { get; set; }
        public Paginador paginador { get; set; }
    }
}
