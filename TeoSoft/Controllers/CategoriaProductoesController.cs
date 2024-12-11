using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeoSoft.Data;
using TeoSoft.Models;

namespace TeoSoft.Controllers
{
    public class CategoriaProductoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriaProductoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CategoriaProductoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.CategoriasProductos.ToListAsync());
        }

        // GET: CategoriaProductoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaProducto = await _context.CategoriasProductos
                .FirstOrDefaultAsync(m => m.IdCatProduc == id);
            if (categoriaProducto == null)
            {
                return NotFound();
            }

            return View(categoriaProducto);
        }

        // GET: CategoriaProductoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoriaProductoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCatProduc,Nombre,Descripcion,Estado")] CategoriaProducto categoriaProducto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoriaProducto);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Categoría de producto creada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(categoriaProducto);
        }

        // GET: CategoriaProductoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaProducto = await _context.CategoriasProductos.FindAsync(id);
            if (categoriaProducto == null)
            {
                return NotFound();
            }
            return View(categoriaProducto);
        }

        // POST: CategoriaProductoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCatProduc,Nombre,Descripcion,Estado")] CategoriaProducto categoriaProducto)
        {
            if (id != categoriaProducto.IdCatProduc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoriaProducto);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Categoría de producto actualizada correctamente.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaProductoExists(categoriaProducto.IdCatProduc))
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
            return View(categoriaProducto);
        }

        // GET: CategoriaProductoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaProducto = await _context.CategoriasProductos
                .FirstOrDefaultAsync(m => m.IdCatProduc == id);
            if (categoriaProducto == null)
            {
                return NotFound();
            }

            return View(categoriaProducto);
        }

        // POST: CategoriaProductoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoriaProducto = await _context.CategoriasProductos.FindAsync(id);
            if (categoriaProducto == null)
            {
                return Json(new { success = false, message = "Categoría no encontrada." });
            }

            try
            {
                _context.CategoriasProductos.Remove(categoriaProducto);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Categoría eliminada correctamente." });
            }
            catch (DbUpdateException ex)
            {
                // Log the exception details
                Console.WriteLine(ex.ToString());

                // Check if the exception is due to a foreign key constraint
                if (ex.InnerException != null && ex.InnerException.Message.Contains("FOREIGN KEY"))
                {
                    return Json(new { success = false, message = "No se puede eliminar esta categoría porque está siendo utilizada por otros registros." });
                }

                return Json(new { success = false, message = "Error al eliminar la categoría. Por favor, inténtelo de nuevo." });
            }
        }

        private bool CategoriaProductoExists(int id)
        {
            return _context.CategoriasProductos.Any(e => e.IdCatProduc == id);
        }
    }
}