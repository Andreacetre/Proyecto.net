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
    public class DetallePedidoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DetallePedidoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DetallePedidoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DetallesPedidos.Include(d => d.Pedido).Include(d => d.Producto);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DetallePedidoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detallePedido = await _context.DetallesPedidos
                .Include(d => d.Pedido)
                .Include(d => d.Producto)
                .FirstOrDefaultAsync(m => m.IdDetPedido == id);
            if (detallePedido == null)
            {
                return NotFound();
            }

            return View(detallePedido);
        }

        // GET: DetallePedidoes/Create
        public IActionResult Create()
        {
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "DireccionEnvio");
            ViewData["IdProducto"] = new SelectList(_context.Productos, "ProductoId", "Nombre");
            return View();
        }

        // POST: DetallePedidoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDetPedido,IdPedido,IdProducto,Cantidad,PrecioUnitario")] DetallePedido detallePedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detallePedido);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Detalle de pedido creado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "DireccionEnvio", detallePedido.IdPedido);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "ProductoId", "Nombre", detallePedido.IdProducto);
            return View(detallePedido);
        }

        // GET: DetallePedidoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detallePedido = await _context.DetallesPedidos.FindAsync(id);
            if (detallePedido == null)
            {
                return NotFound();
            }
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "DireccionEnvio", detallePedido.IdPedido);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "ProductoId", "Nombre", detallePedido.IdProducto);
            return View(detallePedido);
        }

        // POST: DetallePedidoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDetPedido,IdPedido,IdProducto,Cantidad,PrecioUnitario")] DetallePedido detallePedido)
        {
            if (id != detallePedido.IdDetPedido)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detallePedido);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Detalle de pedido actualizado exitosamente.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetallePedidoExists(detallePedido.IdDetPedido))
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
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "DireccionEnvio", detallePedido.IdPedido);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "ProductoId", "Nombre", detallePedido.IdProducto);
            return View(detallePedido);
        }

        // POST: DetallePedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detallePedido = await _context.DetallesPedidos.FindAsync(id);
            if (detallePedido == null)
            {
                return Json(new { success = false, message = "Detalle de pedido no encontrado." });
            }

            try
            {
                _context.DetallesPedidos.Remove(detallePedido);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Detalle de pedido eliminado exitosamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al eliminar el detalle de pedido: " + ex.Message });
            }
        }

        private bool DetallePedidoExists(int id)
        {
            return _context.DetallesPedidos.Any(e => e.IdDetPedido == id);
        }
    }
}

