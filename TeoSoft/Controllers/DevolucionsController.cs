using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeoSoft.Data;
using TeoSoft.Models;

namespace TeoSoft.Controllers
{
    public class DevolucionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DevolucionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Devolucions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Devoluciones.Include(d => d.Producto).Include(d => d.Venta);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Devolucions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var devolucion = await _context.Devoluciones
                .Include(d => d.Producto)
                .Include(d => d.Venta)
                .FirstOrDefaultAsync(m => m.IdDevolucion == id);
            if (devolucion == null)
            {
                return NotFound();
            }

            return View(devolucion);
        }

        // GET: Devolucions/Create
        public IActionResult Create()
        {
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "Nombre");
            ViewData["VentaId"] = new SelectList(_context.Ventas, "VentaId", "VentaId");
            return View();
        }

        // POST: Devolucions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDevolucion,VentaId,ProductoId,FechaDeDevolucion,Cantidad,MotivoDeDevolucion,EstadoDeDevolucion")] Devolucion devolucion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(devolucion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "Nombre", devolucion.ProductoId);
            ViewData["VentaId"] = new SelectList(_context.Ventas, "VentaId", "VentaId", devolucion.VentaId);
            return View(devolucion);
        }

        // GET: Devolucions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var devolucion = await _context.Devoluciones.FindAsync(id);
            if (devolucion == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "Nombre", devolucion.ProductoId);
            ViewData["VentaId"] = new SelectList(_context.Ventas, "VentaId", "VentaId", devolucion.VentaId);
            return View(devolucion);
        }

        // POST: Devolucions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDevolucion,VentaId,ProductoId,FechaDeDevolucion,Cantidad,MotivoDeDevolucion,EstadoDeDevolucion")] Devolucion devolucion)
        {
            if (id != devolucion.IdDevolucion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(devolucion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DevolucionExists(devolucion.IdDevolucion))
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
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "Nombre", devolucion.ProductoId);
            ViewData["VentaId"] = new SelectList(_context.Ventas, "VentaId", "VentaId", devolucion.VentaId);
            return View(devolucion);
        }

        // POST: Devolucions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var devolucion = await _context.Devoluciones.FindAsync(id);
            if (devolucion == null)
            {
                return Json(new { success = false, message = "Devolución no encontrada." });
            }

            try
            {
                _context.Devoluciones.Remove(devolucion);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Devolución eliminada correctamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al eliminar la devolución: " + ex.Message });
            }
        }

        private bool DevolucionExists(int id)
        {
            return _context.Devoluciones.Any(e => e.IdDevolucion == id);
        }
    }
}