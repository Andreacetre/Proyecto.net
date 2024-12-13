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
    public class VentasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VentasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ventas
        public async Task<IActionResult> Index()
        {
            var ventas = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Producto)
                .ToListAsync();
            return View(ventas);
        }

        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Producto)
                .FirstOrDefaultAsync(m => m.VentaId == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // GET: Ventas/Create
        public IActionResult Create()
        {
            var venta = new Venta { Fecha = DateTime.Now, Estado = "Pendiente" };
            PrepareViewData(venta);
            return View(venta);
        }

        // POST: Ventas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Fecha,Cantidad,Total,ClienteId,IdProducto,Estado")] Venta venta)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var producto = await _context.Productos.FindAsync(venta.IdProducto);
                    if (producto != null)
                    {
                        venta.Total = Math.Round(producto.Precio * venta.Cantidad, 2);
                        venta.Fecha = DateTime.Now;
                        _context.Add(venta);
                        await _context.SaveChangesAsync();
                        return Json(new { success = true, message = "Venta creada exitosamente" });
                    }
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, message = "Error al crear la venta", errors });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al crear la venta: " + ex.Message });
            }
        }

        // GET: Ventas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }
            PrepareViewData(venta);
            return View(venta);
        }

        // POST: Ventas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VentaId,Fecha,Cantidad,Total,ClienteId,IdProducto,Estado")] Venta venta)
        {
            if (id != venta.VentaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var producto = await _context.Productos.FindAsync(venta.IdProducto);
                    if (producto != null)
                    {
                        venta.Total = Math.Round(producto.Precio * venta.Cantidad, 2);
                    }
                    _context.Update(venta);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentaExists(venta.VentaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            PrepareViewData(venta);
            return View(venta);
        }

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Producto)
                .FirstOrDefaultAsync(m => m.VentaId == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);
            _context.Ventas.Remove(venta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentaExists(int id)
        {
            return _context.Ventas.Any(e => e.VentaId == id);
        }

        // GET: Ventas/GetProductPrice/5
        public async Task<IActionResult> GetProductPrice(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            return Json(new { price = Math.Round(producto.Precio, 2) });
        }

        // GET: Ventas/GetVentaDetails/5
        public async Task<IActionResult> GetVentaDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.Producto)
                .FirstOrDefaultAsync(v => v.VentaId == id);

            if (venta == null)
            {
                return NotFound();
            }

            return Json(new
            {
                productoId = venta.IdProducto,
                cantidad = venta.Cantidad,
                productoNombre = venta.Producto.Nombre
            });
        }

        private void PrepareViewData(Venta venta)
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nombre", venta.ClienteId);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "ProductoId", "Nombre", venta.IdProducto);
            ViewData["EstadoOptions"] = new List<SelectListItem>
            {
                new SelectListItem { Value = "Pendiente", Text = "Pendiente" },
                new SelectListItem { Value = "Completada", Text = "Completada" },
                new SelectListItem { Value = "Anulada", Text = "Anulada" }
            };
        }
    }
}

