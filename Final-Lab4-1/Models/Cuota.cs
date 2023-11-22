using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Lab4_1.Models
{
    public class Cuota
    {
        public int Id { get; set; }
        public string EstadoCuota { get; set; }

        public List<Alumno> Alumnos { get; set; }
    }
}
