using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Lab4_1.Models
{public class Profesor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Foto { get; set; }

        public int Telefono { get; set; }

        public int TurnoId { get; set; }
        public Turno Turno { get; set; }
       
        public List<Alumno> Alumnos { get; set; }
    }
    
}
