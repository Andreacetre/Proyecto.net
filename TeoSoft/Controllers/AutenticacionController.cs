using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using TeoSoft.Data;
using TeoSoft.Models;

namespace TeoSoft.Controllers
{
    public class AutenticacionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AutenticacionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Autenticacion
        public IActionResult Index()
        {
            return View(new AutenticacionModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(AutenticacionModel model)
        {
            if (ModelState.IsValid)
            {
                var usuarioExistente = await _context.Autenticaciones
                    .FirstOrDefaultAsync(u => u.CorreoElectronico == model.CorreoElectronico);

                if (usuarioExistente != null)
                {
                    ModelState.AddModelError("CorreoElectronico", "Este correo electrónico ya está registrado.");
                    TempData["SweetAlert"] = "error|El registro falló|Este correo electrónico ya está registrado.";
                    return View("Index", model);
                }

                model.Contrasena = HashPassword(model.Contrasena);

                _context.Add(model);
                await _context.SaveChangesAsync();

                TempData["SweetAlert"] = "success|Registro exitoso|Por favor, inicia sesión.";
                return RedirectToAction(nameof(Index));
            }

            TempData["SweetAlert"] = "error|El registro falló|Por favor, verifica tus datos.";
            return View("Index", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string correo, string contrasena)
        {
            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contrasena))
            {
                ModelState.AddModelError("", "Por favor, ingresa correo y contraseña.");
                TempData["SweetAlert"] = "error|Inicio de sesión fallido|Por favor, ingresa correo y contraseña.";
                return View("Index", new AutenticacionModel());
            }

            var hashedPassword = HashPassword(contrasena);
            var usuario = await _context.Autenticaciones
                .FirstOrDefaultAsync(u => u.CorreoElectronico == correo &&
                                        u.Contrasena == hashedPassword);

            if (usuario == null)
            {
                ModelState.AddModelError("", "Correo electrónico o contraseña incorrectos.");
                TempData["SweetAlert"] = "error|Inicio de sesión fallido|Correo electrónico o contraseña incorrectos.";
                return View("Index", new AutenticacionModel());
            }

            HttpContext.Session.SetString("UserId", usuario.Id.ToString());
            HttpContext.Session.SetString("UserName", usuario.Nombre);

            TempData["SweetAlert"] = $"success|Inicio de sesión exitoso|Bienvenido, {usuario.Nombre}!";
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["SweetAlert"] = "success|Sesión cerrada|Has cerrado sesión exitosamente.";
            return RedirectToAction("Index");
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
