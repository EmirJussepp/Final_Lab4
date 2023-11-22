using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Final_Lab4_1.Models;
using Microsoft.AspNetCore.Hosting;
using Final_Lab4_1.ModelVIew;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace Final_Lab4_1.Controllers
{

    [Authorize]
    public class ProfesoresController : Controller
    {
        private readonly AppDBcontext _context;
        private readonly IWebHostEnvironment env;

        public ProfesoresController(AppDBcontext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }

        // GET: Profesores
        [AllowAnonymous]

        public async Task<IActionResult> Index(string BusquedaNombre, int? TurnoId, int pagina = 1)
        {
            Paginador paginas = new Paginador();
            paginas.PaginaActual = pagina;
            paginas.RegistrosPorPagina = 3;

            var appDBcontext = _context.profesores.Include(p => p.Turno).Select(p=>p);
            if (!string.IsNullOrEmpty(BusquedaNombre))
            {
                appDBcontext = appDBcontext.Where(c => c.Nombre.Contains(BusquedaNombre) || c.Apellido.Contains(BusquedaNombre));
                paginas.ValoresQueryString.Add("BusquedaNombre", BusquedaNombre);
            }
            if (TurnoId.HasValue)
            {
                appDBcontext = appDBcontext.Where(c => c.TurnoId == TurnoId.Value);
                paginas.ValoresQueryString.Add("TurnoId", TurnoId.ToString());
            }
            paginas.TotalRegistros = appDBcontext.Count();

            var registros = appDBcontext
                .Skip((pagina - 1) * paginas.RegistrosPorPagina)
                .Take(paginas.RegistrosPorPagina);
            ProfesoresLista datos = new ProfesoresLista()
            {
                profesores = registros.ToList(),
                BusquedaNombre = BusquedaNombre,
                turnos = new SelectList(_context.turnos, "Id", "TurnosClase"),
                TurnoId = TurnoId,
                paginador = paginas
            };

            ViewData["TurnoId"] = new SelectList(_context.turnos, "Id", "TurnosClase");
            return View(datos);

            //return View(await appDBcontext.ToListAsync());
        }

        // GET: Profesores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesor = await _context.profesores
                .Include(p => p.Turno)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesor == null)
            {
                return NotFound();
            }

            return View(profesor);
        }

        // GET: Profesores/Create
        public IActionResult Create()
        {
            ViewData["TurnoId"] = new SelectList(_context.turnos, "Id", "Id");
            return View();
        }

        // POST: Profesores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Foto,Telefono,TurnoId")] Profesor profesor)
        {
            if (ModelState.IsValid)
            {

                var archivos = HttpContext.Request.Form.Files;
                if (archivos != null && archivos.Count > 0)
                {
                    var archivosFoto = archivos[0];
                    if (archivosFoto.Length > 0)
                    {
                        var pathCompatible = Path.Combine(env.WebRootPath, "images\\profesores");
                        var archivoDestino = Guid.NewGuid().ToString(); //Genera numeros aleatorios convirtiendo a string
                        archivoDestino = archivoDestino.Replace("-", "");//Le saca los guiones a los archivos para q no haya errores
                        archivoDestino += Path.GetExtension(archivosFoto.FileName);//Concatenamos la extension del archivo 
                        var rutaDestino = Path.Combine(pathCompatible, archivoDestino);
                        using (var fileStream = new FileStream(rutaDestino, FileMode.Create))
                        {
                            archivosFoto.CopyTo(fileStream);
                            profesor.Foto = archivoDestino;
                        };
                    }
                }

                _context.Add(profesor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TurnoId"] = new SelectList(_context.turnos, "Id", "Id", profesor.TurnoId);
            return View(profesor);
        }

        // GET: Profesores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesor = await _context.profesores.FindAsync(id);
            if (profesor == null)
            {
                return NotFound();
            }
            ViewData["TurnoId"] = new SelectList(_context.turnos, "Id", "Id", profesor.TurnoId);
            return View(profesor);
        }

        // POST: Profesores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Foto,Telefono,TurnoId")] Profesor profesor)
        {
            if (id != profesor.Id)
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
                        var pathCompatible = Path.Combine(env.WebRootPath, "images\\profesores");
                        var archivoDestino = Guid.NewGuid().ToString(); //Genera numeros aleatorios convirtiendo a string
                        archivoDestino = archivoDestino.Replace("-", "");//Le saca los guiones a los archivos para q no haya errores
                        archivoDestino += Path.GetExtension(archivosFoto.FileName);//Concatenamos la extension del archivo 
                        var rutaDestino = Path.Combine(pathCompatible, archivoDestino);
                        if (!string.IsNullOrEmpty(profesor.Foto))
                        {
                            var fotoanterior = Path.Combine(pathCompatible, profesor.Foto);
                            if (System.IO.File.Exists(fotoanterior))
                            {

                                System.IO.File.Delete(fotoanterior);
                            }

                        }
                        using (var fileStream = new FileStream(rutaDestino, FileMode.Create))
                        {
                            archivosFoto.CopyTo(fileStream);
                            profesor.Foto = archivoDestino;
                        };
                    }

                }

                try
                {
                    _context.Update(profesor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfesorExists(profesor.Id))
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
            ViewData["TurnoId"] = new SelectList(_context.turnos, "Id", "Id", profesor.TurnoId);
            return View(profesor);
        }

        // GET: Profesores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesor = await _context.profesores
                .Include(p => p.Turno)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesor == null)
            {
                return NotFound();
            }

            return View(profesor);
        }

        // POST: Profesores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profesor = await _context.profesores.FindAsync(id);
            _context.profesores.Remove(profesor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfesorExists(int id)
        {
            return _context.profesores.Any(e => e.Id == id);
        }
    }
}
