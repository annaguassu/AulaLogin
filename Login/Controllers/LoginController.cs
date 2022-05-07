using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Login.Controllers
{
    public class LoginController : Controller
    {
        private readonly Contexto db;

        public LoginController(Contexto contexto)
        {
            db = contexto;
        }

        public IActionResult Entrar()
        {
            return View();
        }
        [HttpPost]

        public async Task<ActionResult> Entrar(string login, string senha)
        {
            if(string.IsNullOrEmpty(login) || string.IsNullOrEmpty(senha))
            {
                TempData["erro"] = "Os campos não podem estar vazios";
            }


            Entidades.Usuario usuarioLogado = db.USUARIOS.Where(a =>
            a.Login == login && a.Senha == senha).FirstOrDefault();

            if(usuarioLogado == null)
            {
                TempData["erro"] = "Usuário e senha Inválido";
                return View();
            }

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, usuarioLogado.Nome));
            claims.Add(new Claim(ClaimTypes.Sid, usuarioLogado.Id.ToString()));

            var userIdentity = new ClaimsIdentity(claims, "Acesso");
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
            await HttpContext.SignInAsync("CookieAuthentication", principal, new AuthenticationProperties());

            return View();
        }

        public async Task<IActionResult> Logoff()
        {
            await HttpContext.SignOutAsync("CookieAuthentication");
            ViewData["ReturnUrl"] = "/";
            return Redirect("/Login/Entrar");
        }
    }
}
