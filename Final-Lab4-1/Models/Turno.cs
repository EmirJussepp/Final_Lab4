using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Lab4_1.Models
{
    public class Turno
    {
        public int Id { get; set; }
        public string TurnosClase { get; set; }

        public List<Profesor> Profesores { get; set; }
    }
}
