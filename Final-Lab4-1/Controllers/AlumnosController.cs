        using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Final_Lab4_1.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Final_Lab4_1.ModelVIew;
using Microsoft.AspNetCore.Authorization;

namespace Final_Lab4_1.Controllers
{
    [Authorize]
    public class AlumnosController : Controller
    {
        private readonly AppDBcontext _context;
        private readonly IWebHostEnvironment env;

        public AlumnosController(AppDBcontext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }
        // GET: Alumnos
        [AllowAnonymous]
        public async Task<IActionResult> Index(string busquedaNombre, int?ProfesorId, int pagina=1)
        {
            Paginador paginas = new Paginador();
            paginas.PaginaActual = pagina;
            paginas.RegistrosPorPagina = 3;
            var appDBcontext = _context.alumnos.Include(a => a.Profesor).Select(a=>a).Include(a => a.categorias).Select(a => a).Include(a => a.cuotas).Select(a => a).Include(a => a.localidades).Select(a => a).Include(a => a.provincias).Select(a => a);
            
            if(!string.IsNullOrEmpty(busquedaNombre))
            {
                appDBcontext = appDBcontext.Where(c => c.Nombre.Contains(busquedaNombre) || c.Apellido.Contains(busquedaNombre) || c.Dni.ToString().Contains(busquedaNombre));
                paginas.ValoresQueryString.Add("BusquedaNombre", busquedaNombre);
            }
            if (ProfesorId.HasValue)
            {
                appDBcontext = appDBcontext.Where(c => c.ProfesorId == ProfesorId.Value);
                paginas.ValoresQueryString.Add("ProfesorId", ProfesorId.ToString());
            }
            paginas.TotalRegistros = appDBcontext.Count();
            var registrosMostrar = appDBcontext
                        .Skip((pagina - 1) * paginas.RegistrosPorPagina)
                        .Take(paginas.RegistrosPorPagina);
            AlumnosLIsta data = new AlumnosLIsta()
            {
                alumnos = registrosMostrar.ToList(),

                BusquedaNombre = busquedaNombre,
                ListProfresores = new SelectList(_context.profesores, "Id", "Nombre"),
                ProfesorId = ProfesorId,
                paginador = paginas
            };


            ViewData["ProfesorId"] = new SelectList(_context.profesores, "Id", "Nombre");
            ViewData["CategoriaId"] = new SelectList(_context.categorias, "Id", "Nombre");
            ViewData["CuotaId"] = new SelectList(_context.cuotas, "Id", "EstadoCuota");
            ViewData["LocalidadId"] = new SelectList(_context.localidades, "Id", "NombreLocalidades");
            ViewData["ProvinciaId"] = new SelectList(_context.provincias, "Id", "NombreProvincia");
            return View(data);
            //return View(await appDBcontext.ToListAsync());
        }




        public async Task<IActionResult> Importar()
        {
            var archivos = HttpContext.Request.Form.Files;
            if (archivos != null && archivos.Count > 0)
            {
                var archivo = archivos[0];
                if (archivo.Length > 0)
                {

                    var pathCompatible = Path.Combine(env.WebRootPath, "importar");
                    var archivoDestino = Guid.NewGuid().ToString(); //Genera numeros aleatorios convirtiendo a string
                    archivoDestino = archivoDestino.Replace("-", "");//Le saca los guiones a los archivos para q no haya errores
                    archivoDestino += Path.GetExtension(archivo.FileName);//Concatenamos la extension del archivo 
                    var rutaDestino = Path.Combine(pathCompatible, archivoDestino);
                    using (var fileStream = new FileStream(rutaDestino, FileMode.Create))
                    {
                        archivo.CopyTo(fileStream);

                    };

                    using (var file = new FileStream(rutaDestino, FileMode.Open))
                    {
                        List<string> renglones = new List<string>();
                        List<Alumno> alumnoArchivo = new List<Alumno>();

                        StreamReader fileContent = new StreamReader(file, System.Text.Encoding.Default);
                        do
                        {
                            renglones.Add(fileContent.ReadLine());

                        }
                        while (!fileContent.EndOfStream);

                        if (renglones.Count() > 0)
                        {
                            foreach (var renglon in renglones)
                            {

                                string[] data = renglon.Split(';');
                                if (data.Length == 10)
                                {
                                    Alumno cliente = new Alumno();
                                    cliente.Nombre = data[0].Trim();
                                    cliente.Apellido = data[1].Trim();
                                    cliente.Dni = int.Parse(data[2].Trim());
                                    cliente.Telefono = int.Parse(data[3].Trim());
                                    cliente.Foto = data[4].Trim();
                                    cliente.ProfesorId = int.Parse(data[5].Trim());
                                    cliente.CuotaId = int.Parse(data[6].Trim());
                                    cliente.CategoriaId = int.Parse(data[7].Trim());
                                    cliente.LocalidadId = int.Parse(data[8].Trim());
                                    cliente.ProvinciaId = int.Parse(data[9].Trim());
                                    alumnoArchivo.Add(cliente);
                                }

                            }
                            if (alumnoArchivo.Count() > 0)
                            {
                                _context.AddRange(alumnoArchivo);
                                await _context.SaveChangesAsync();

                            }
                        }


                    };

                    
                }
            }

            //var appDBcontext = _context.alumnos.Include(a => a.Profesor).Include(a => a.categorias).Include(a => a.cuotas).Include(a => a.localidades).Include(a => a.provincias);
            //return View(await appDBcontext.ToListAsync());
            return RedirectToAction("Index");
        }

        // GET: Alumnos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = await _context.alumnos
                .Include(a => a.Profesor)
                .Include(a => a.categorias)
                .Include(a => a.cuotas)
                .Include(a => a.localidades)
                .Include(a => a.provincias)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }

        // GET: Alumnos/Create
        public IActionResult Create()
        {
            ViewData["ProfesorId"] = new SelectList(_context.profesores, "Id", "Nombre");
            ViewData["CategoriaId"] = new SelectList(_context.categorias, "Id", "Nombre");
            ViewData["CuotaId"] = new SelectList(_context.cuotas, "Id", "EstadoCuota");
            ViewData["LocalidadId"] = new SelectList(_context.localidades, "Id", "NombreLocalidades");
            ViewData["ProvinciaId"] = new SelectList(_context.provincias, "Id", "NombreProvincia");
            return View();
        }

        // POST: Alumnos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Dni,Telefono,Foto,ProfesorId,CuotaId,CategoriaId,LocalidadId,ProvinciaId")] Alumno alumno)
        {



            if (ModelState.IsValid)
            {
                var archivos = HttpContext.Request.Form.Files;
                if (archivos != null && archivos.Count > 0)
                {
                    var archivosFoto = archivos[0];
                    if (archivosFoto.Length > 0)
                    {
                        var pathCompatible = Path.Combine(env.WebRootPath, "images\\alumnos");
                        var archivoDestino = Guid.NewGuid().ToString(); //Genera numeros aleatorios convirtiendo a string
                        archivoDestino = archivoDestino.Replace("-", "");//Le saca los guiones a los archivos para q no haya errores
                        archivoDestino += Path.GetExtension(archivosFoto.FileName);//Concatenamos la extension del archivo 
                        var rutaDestino = Path.Combine(pathCompatible, archivoDestino);
                        using (var fileStream = new FileStream(rutaDestino, FileMode.Create))
                        {
                            archivosFoto.CopyTo(fileStream);
                            alumno.Foto = archivoDestino;
                        };
                    }
                }

                _context.Add(alumno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProfesorId"] = new SelectList(_context.profesores, "Id", "Nombre", alumno.ProfesorId);
            ViewData["CategoriaId"] = new SelectList(_context.categorias, "Id", "Id", alumno.CategoriaId);
            ViewData["CuotaId"] = new SelectList(_context.cuotas, "Id", "Id", alumno.CuotaId);
            ViewData["LocalidadId"] = new SelectList(_context.localidades, "Id", "Id", alumno.LocalidadId);
            ViewData["ProvinciaId"] = new SelectList(_context.provincias, "Id", "Id", alumno.ProvinciaId);
            return View(alumno);
        }

        // GET: Alumnos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = await _context.alumnos.FindAsync(id);
            if (alumno == null)
            {
                return NotFound();
            }
            ViewData["ProfesorId"] = new SelectList(_context.profesores, "Id", "Id", alumno.ProfesorId);
            ViewData["CategoriaId"] = new SelectList(_context.categorias, "Id", "Id", alumno.CategoriaId);
            ViewData["CuotaId"] = new SelectList(_context.cuotas, "Id", "Id", alumno.CuotaId);
            ViewData["LocalidadId"] = new SelectList(_context.localidades, "Id", "Id", alumno.LocalidadId);
            ViewData["ProvinciaId"] = new SelectList(_context.provincias, "Id", "Id", alumno.ProvinciaId);
            return View(alumno);
        }

        // POST: Alumnos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Dni,Telefono,Foto,ProfesorId,CuotaId,CategoriaId,LocalidadId,ProvinciaId")] Alumno alumno)
        {
            if (id != alumno.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var archivos = HttpContext.Request.Form.Files;
                if (archivos != null && archivos.Count > 0)
                {
                    var archivosFoto = archivos[0];
                    if (archivosFoto.Length > 0)
                    {
                        var pathCompatible = Path.Combine(env.WebRootPath, "images\\alumnos");
                        var archivoDestino = Guid.NewGuid().ToString(); //Genera numeros aleatorios convirtiendo a string
                        archivoDestino = archivoDestino.Replace("-", "");//Le saca los guiones a los archivos para q no haya errores
                        archivoDestino += Path.GetExtension(archivosFoto.FileName);//Concatenamos la extension del archivo 
                        var rutaDestino = Path.Combine(pathCompatible, archivoDestino);
                        if (!string.IsNullOrEmpty(alumno.Foto))
                        {
                            var fotoanterior = Path.Combine(pathCompatible, alumno.Foto);
                            if (System.IO.File.Exists(fotoanterior))
                            {

                                System.IO.File.Delete(fotoanterior);
                            }

                        }
                        using (var fileStream = new FileStream(rutaDestino, FileMode.Create))
                        {
                            archivosFoto.CopyTo(fileStream);
                            alumno.Foto = archivoDestino;
                        };
                    }

                }




                try
                {
                    _context.Update(alumno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlumnoExists(alumno.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProfesorId"] = new SelectList(_context.profesores, "Id", "Nombre", alumno.ProfesorId);
            ViewData["CategoriaId"] = new SelectList(_context.categorias, "Id", "Id", alumno.CategoriaId);
            ViewData["CuotaId"] = new SelectList(_context.cuotas, "Id", "Id", alumno.CuotaId);
            ViewData["LocalidadId"] = new SelectList(_context.localidades, "Id", "Id", alumno.LocalidadId);
            ViewData["ProvinciaId"] = new SelectList(_context.provincias, "Id", "Id", alumno.ProvinciaId);
            return View(alumno);
        }

        // GET: Alumnos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = await _context.alumnos
                .Include(a => a.Profesor)
                .Include(a => a.categorias)
                .Include(a => a.cuotas)
                .Include(a => a.localidades)
                .Include(a => a.provincias)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }

        // POST: Alumnos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alumno = await _context.alumnos.FindAsync(id);
            _context.alumnos.Remove(alumno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlumnoExists(int id)
        {
            return _context.alumnos.Any(e => e.Id == id);
        }
    }
}
