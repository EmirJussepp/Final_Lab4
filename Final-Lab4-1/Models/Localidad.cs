﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Lab4_1.Models
{
    public class Localidad
    {
        public int Id { get; set; }
        public string NombreLocalidades { get; set; }

        public List<Alumno> Alumnos { get; set; }
    }
}
