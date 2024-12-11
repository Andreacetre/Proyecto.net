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
    public class DetalleVentasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DetalleVentasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DetalleVentas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DetallesVentas.Include(d => d.Producto).Include(d => d.Venta);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DetalleVentas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleVenta = await _context.DetallesVentas
                .Include(d => d.Producto)
                .Include(d => d.Venta)
                .FirstOrDefaultAsync(m => m.DetalleVentaId == id);
            if (detalleVenta == null)
            {
                return NotFound();
            }

            return View(detalleVenta);
        }

        // GET: DetalleVentas/Create
        public IActionResult Create()
        {
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "Nombre");
            ViewData["VentaId"] = new SelectList(_context.Ventas, "VentaId", "VentaId");
            return View();
        }

        // POST: DetalleVentas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DetalleVentaId,VentaId,ProductoId,Cantidad,Precio,SubTotal")] DetalleVenta detalleVenta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalleVenta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "Nombre", detalleVenta.ProductoId);
            ViewData["VentaId"] = new SelectList(_context.Ventas, "VentaId", "VentaId", detalleVenta.VentaId);
            return View(detalleVenta);
        }

        // GET: DetalleVentas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleVenta = await _context.DetallesVentas.FindAsync(id);
            if (detalleVenta == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "Nombre", detalleVenta.ProductoId);
            ViewData["VentaId"] = new SelectList(_context.Ventas, "VentaId", "VentaId", detalleVenta.VentaId);
            return View(detalleVenta);
        }

        // POST: DetalleVentas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DetalleVentaId,VentaId,ProductoId,Cantidad,Precio,SubTotal")] DetalleVenta detalleVenta)
        {
            if (id != detalleVenta.DetalleVentaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleVenta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleVentaExists(detalleVenta.DetalleVentaId))
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
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "Nombre", detalleVenta.ProductoId);
            ViewData["VentaId"] = new SelectList(_context.Ventas, "VentaId", "VentaId", detalleVenta.VentaId);
            return View(detalleVenta);
        }

        // POST: DetalleVentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalleVenta = await _context.DetallesVentas.FindAsync(id);
            if (detalleVenta != null)
            {
                _context.DetallesVentas.Remove(detalleVenta);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Detalle de venta eliminado correctamente." });
            }
            return Json(new { success = false, message = "No se pudo eliminar el detalle de venta." });
        }

        private bool DetalleVentaExists(int id)
        {
            return _context.DetallesVentas.Any(e => e.DetalleVentaId == id);
        }
    }
}