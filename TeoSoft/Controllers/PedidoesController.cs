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
    public class PedidoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PedidoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pedidoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Pedidos.Include(p => p.Cliente).Include(p => p.Producto);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Pedidoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Producto)
                .FirstOrDefaultAsync(m => m.IdPedido == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedidoes/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "ClienteId", "Nombre");
            ViewData["IdProducto"] = new SelectList(_context.Productos, "ProductoId", "Nombre");
            return View();
        }

        // POST: Pedidoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPedido,IdCliente,IdProducto,DireccionEnvio,MetodoPago,MontoTotal,FechaDelPedido,EstadoPedido")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Pedido creado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "ClienteId", "Nombre", pedido.IdCliente);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "ProductoId", "Nombre", pedido.IdProducto);
            return View(pedido);
        }

        // GET: Pedidoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "ClienteId", "Nombre", pedido.IdCliente);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "ProductoId", "Nombre", pedido.IdProducto);
            return View(pedido);
        }

        // POST: Pedidoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPedido,IdCliente,IdProducto,DireccionEnvio,MetodoPago,MontoTotal,FechaDelPedido,EstadoPedido")] Pedido pedido)
        {
            if (id != pedido.IdPedido)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Pedido actualizado exitosamente.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.IdPedido))
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "ClienteId", "Nombre", pedido.IdCliente);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "ProductoId", "Nombre", pedido.IdProducto);
            return View(pedido);
        }

        // POST: Pedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return Json(new { success = false, message = "Pedido no encontrado." });
            }

            try
            {
                _context.Pedidos.Remove(pedido);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Pedido eliminado exitosamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al eliminar el pedido: " + ex.Message });
            }
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.IdPedido == id);
        }
    }
}

