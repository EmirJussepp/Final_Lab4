using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Lab4_1.Models
{
    public class AppDBcontext : IdentityDbContext<IdentityUser>
    {
        public AppDBcontext(DbContextOptions<AppDBcontext> options) : base(options)
        {

        }

        public DbSet<Alumno> alumnos { get; set; }
        public DbSet<Profesor> profesores { get; set; }
        public DbSet<Categoria> categorias { get; set; }
        public DbSet<Localidad> localidades { get; set; }
        public DbSet<Provincia> provincias { get; set; }
        public DbSet<Cuota> cuotas { get; set; }
        public DbSet<Turno> turnos { get; set; }
    }
}
