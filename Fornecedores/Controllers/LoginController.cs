using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Fornecedores.Models.Entidades;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Fornecedores.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult UsuarioLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UsuarioLogin([Bind] Usuario _usuario)
        {
            var usuario = new Usuario();
            if (usuario.GetUsuarios().Any(u => u.Login == _usuario.Login && u.Senha == _usuario.Senha))
            {
                var userClaims = new List<Claim>()
                {
                    //define o cookie
                    new Claim(ClaimTypes.Name, _usuario.Login),
                    new Claim(ClaimTypes.Email, "drigo.plopes@gmail.com"),
                };
                var minhaIdentity = new ClaimsIdentity(userClaims, "Usuario");
                var userPrincipal = new ClaimsPrincipal(new[] { minhaIdentity });
                //cria o cookie
                HttpContext.SignInAsync(userPrincipal);
                return RedirectToAction("Index", "Fornecedor");
            }
            ViewBag.Message = "Usuári(a)o inválid(a)o...";
            return View(_usuario);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("UsuarioLogin", "Login");
        }
    }
}