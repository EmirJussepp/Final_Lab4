using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Lab4_1.Models
{
    public class Alumno
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Dni { get; set; }
        public int Telefono { get; set; }
        public string Foto { get; set; }


        public int ProfesorId { get; set; }
        public Profesor Profesor { get; set; }


        public int CuotaId { get; set; }
        public Cuota cuotas { get; set; }


        public int CategoriaId { get; set; }
        public Categoria categorias { get; set; }

        public int LocalidadId { get; set; }
        public Localidad localidades { get; set; }

        public int ProvinciaId { get; set; }
        public Provincia provincias { get; set; }
    }
}
