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

        // GET: Devoluciones
        public async Task<IActionResult> Index()
        {
            var devoluciones = await _context.Devoluciones
                .Include(d => d.Producto)
                .Include(d => d.Venta)
                .ToListAsync();
            return View(devoluciones);
        }

        // GET: Devoluciones/Details/5
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

        // GET: Devoluciones/Create
        public IActionResult Create()
        {
            var ventas = _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Producto)
                .Select(v => new
                {
                    v.VentaId,
                    DisplayText = $"Venta #{v.VentaId} - {v.Cliente.Nombre} - {v.Producto.Nombre}"
                });

            ViewData["VentaId"] = new SelectList(ventas, "VentaId", "DisplayText");
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "Nombre");

            var devolucion = new Devolucion
            {
                FechaDeDevolucion = DateTime.Now,
                EstadoDeDevolucion = "Pendiente"
            };

            return View(devolucion);
        }

        // POST: Devoluciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VentaId,ProductoId,FechaDeDevolucion,Cantidad,MotivoDeDevolucion,EstadoDeDevolucion")] Devolucion devolucion)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var venta = await _context.Ventas
                        .Include(v => v.Producto)
                        .FirstOrDefaultAsync(v => v.VentaId == devolucion.VentaId);

                    if (venta == null)
                    {
                        return Json(new { success = false, message = "La venta seleccionada no existe." });
                    }

                    if (devolucion.Cantidad > venta.Cantidad)
                    {
                        return Json(new { success = false, message = "La cantidad a devolver no puede ser mayor que la cantidad vendida." });
                    }

                    devolucion.ProductoId = venta.IdProducto;
                    _context.Add(devolucion);
                    await _context.SaveChangesAsync();

                    return Json(new { success = true, message = "Devolución creada exitosamente" });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "Error al crear la devolución: " + ex.Message });
                }
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, message = "Por favor, corrija los errores en el formulario.", errors });
        }

        // GET: Devoluciones/Edit/5
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

        // POST: Devoluciones/Edit/5
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

        // POST: Devoluciones/Delete/5
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
