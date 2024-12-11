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
    public class ProductoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Productoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Productos.Include(p => p.CategoriaProducto);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Productoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.CategoriaProducto)
                .FirstOrDefaultAsync(m => m.ProductoId == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productoes/Create
        public IActionResult Create()
        {
            ViewData["CategoriaProductoId"] = new SelectList(_context.CategoriasProductos, "IdCatProduc", "Nombre");
            return View();
        }

        // POST: Productoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductoId,CodigoDeBarra,Nombre,Precio,Stock,Descripcion,IVA,FechaVencimiento,SinVencimiento,Estado,CategoriaProductoId")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Producto creado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaProductoId"] = new SelectList(_context.CategoriasProductos, "IdCatProduc", "Nombre", producto.CategoriaProductoId);
            return View(producto);
        }

        // GET: Productoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            ViewData["CategoriaProductoId"] = new SelectList(_context.CategoriasProductos, "IdCatProduc", "Nombre", producto.CategoriaProductoId);
            return View(producto);
        }

        // POST: Productoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductoId,CodigoDeBarra,Nombre,Precio,Stock,Descripcion,IVA,FechaVencimiento,SinVencimiento,Estado,CategoriaProductoId")] Producto producto)
        {
            if (id != producto.ProductoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Producto actualizado exitosamente.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.ProductoId))
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
            ViewData["CategoriaProductoId"] = new SelectList(_context.CategoriasProductos, "IdCatProduc", "Nombre", producto.CategoriaProductoId);
            return View(producto);
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return Json(new { success = false, message = "Producto no encontrado." });
            }

            try
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Producto eliminado exitosamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al eliminar el producto: " + ex.Message });
            }
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.ProductoId == id);
        }
    }
}