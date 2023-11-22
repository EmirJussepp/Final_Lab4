using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Final_Lab4_1.Models;
using Microsoft.AspNetCore.Authorization;

namespace Final_Lab4_1.Controllers
{

    [Authorize]
    public class CuotasController : Controller
    {
        private readonly AppDBcontext _context;

        public CuotasController(AppDBcontext context)
        {
            _context = context;
        }


        // GET: Cuotas
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.cuotas.ToListAsync());
        }

        // GET: Cuotas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuota = await _context.cuotas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cuota == null)
            {
                return NotFound();
            }

            return View(cuota);
        }

        // GET: Cuotas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cuotas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EstadoCuota")] Cuota cuota)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cuota);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cuota);
        }

        // GET: Cuotas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuota = await _context.cuotas.FindAsync(id);
            if (cuota == null)
            {
                return NotFound();
            }
            return View(cuota);
        }

        // POST: Cuotas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EstadoCuota")] Cuota cuota)
        {
            if (id != cuota.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cuota);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CuotaExists(cuota.Id))
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
            return View(cuota);
        }

        // GET: Cuotas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuota = await _context.cuotas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cuota == null)
            {
                return NotFound();
            }

            return View(cuota);
        }

        // POST: Cuotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cuota = await _context.cuotas.FindAsync(id);
            _context.cuotas.Remove(cuota);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CuotaExists(int id)
        {
            return _context.cuotas.Any(e => e.Id == id);
        }
    }
}
