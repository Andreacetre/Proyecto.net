using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeoSoft.Models;

namespace TeoSoft.Controllers
{
    public class DevolucionsController : Controller
    {
        // Datos simulados
        private static List<Venta> _ventas = new List<Venta>
        {
            new Venta { VentaId = 1, ClienteId = 1, IdProducto = 1, Cantidad = 5, Total = 500, Fecha = DateTime.Now.AddDays(-5) },
            new Venta { VentaId = 2, ClienteId = 2, IdProducto = 2, Cantidad = 3, Total = 300, Fecha = DateTime.Now.AddDays(-3) },
        };

        private static List<Producto> _productos = new List<Producto>
        {
            new Producto { ProductoId = 1, Nombre = "Producto 1" },
            new Producto { ProductoId = 2, Nombre = "Producto 2" },
        };

        private static List<Cliente> _clientes = new List<Cliente>
        {
            new Cliente { ClienteId = 1, Nombre = "Cliente 1" },
            new Cliente { ClienteId = 2, Nombre = "Cliente 2" },
        };

        private static List<Devolucion> _devoluciones = new List<Devolucion>();

        public IActionResult Index()
        {
            return View(_devoluciones);
        }

        public IActionResult Create()
        {
            var ventasConDetalles = _ventas.Select(v => new
            {
                v.VentaId,
                DisplayText = $"Venta #{v.VentaId} - {_clientes.FirstOrDefault(c => c.ClienteId == v.ClienteId)?.Nombre} - {_productos.FirstOrDefault(p => p.ProductoId == v.IdProducto)?.Nombre}"
            });

            ViewBag.VentaId = new SelectList(ventasConDetalles, "VentaId", "DisplayText");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("VentaId,ProductoId,FechaDeDevolucion,Cantidad,MotivoDeDevolucion,EstadoDeDevolucion")] Devolucion devolucion)
        {
            if (ModelState.IsValid)
            {
                var venta = _ventas.FirstOrDefault(v => v.VentaId == devolucion.VentaId);
                if (venta == null)
                {
                    return Json(new { success = false, message = "La venta seleccionada no existe." });
                }

                if (devolucion.Cantidad > venta.Cantidad)
                {
                    return Json(new { success = false, message = "La cantidad a devolver no puede ser mayor que la cantidad vendida." });
                }

                devolucion.DevolucionId = _devoluciones.Count + 1;
                devolucion.ProductoId = venta.IdProducto;
                _devoluciones.Add(devolucion);

                return Json(new { success = true, message = "Devolución creada exitosamente" });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, message = "Por favor, corrija los errores en el formulario.", errors });
        }

        [HttpGet]
        public IActionResult GetVentaDetails(int id)
        {
            var venta = _ventas.FirstOrDefault(v => v.VentaId == id);
            if (venta == null)
            {
                return NotFound();
            }

            var producto = _productos.FirstOrDefault(p => p.ProductoId == venta.IdProducto);

            return Json(new
            {
                productoId = venta.IdProducto,
                cantidad = venta.Cantidad,
                productoNombre = producto?.Nombre
            });
        }
    }
}

