using Login.Entidades;
using Login.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly Contexto db;

        public UsuariosController(Contexto contexto)
        {
            db = contexto;
        }

        public IActionResult Index()
        {
            return View(db.USUARIOS.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Usuario dadosTela)
        {

            if (string.IsNullOrEmpty(dadosTela.Login))
            {
                TempData["erro"] = "Campo Login não pode estar em branco";
                return View();
            }

            if (string.IsNullOrEmpty(dadosTela.Senha))
            {
                TempData["erro"] = "Campo Senha não pode estar em branco";
                return View();
            }

            db.USUARIOS.Add(dadosTela);
            db.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}
