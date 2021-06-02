using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fornecedores.Models.Contexto;
using Fornecedores.Models.Entidades;
using X.PagedList;
using Fornecedores.Models;
using Microsoft.AspNetCore.Authorization;

namespace Fornecedores.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly Contexto _context;

        public FornecedorController(Contexto context)
        {
            _context = context;
        }           

        [Authorize]
        public ActionResult UIndex()
        {
            var usuario = new Usuario();
            return View(usuario.GetUsuarios());
        }
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {

            var usuario = "Anônimo";
            var autenticado = false;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                usuario = HttpContext.User.Identity.Name;
                autenticado = true;
            }
            else
            {
                usuario = "Não Logado";
                autenticado = false;
            }

            ViewBag.usuario = usuario;
            ViewBag.autenticado = autenticado;
       

            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var fornecedores = from s in _context.Fornecedor
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                fornecedores = fornecedores.Where(s => s.RazaoSocial.Contains(searchString)
                                       || s.CNPJ.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    fornecedores = fornecedores.OrderByDescending(s => s.RazaoSocial);
                    break;
                case "Date":
                    fornecedores = fornecedores.OrderBy(s => s.DataCadastro);
                    break;
                //case "date_desc":
                //    fornecedores = fornecedores.OrderByDescending(s => s.date);
                //    break;
                default:
                    fornecedores = fornecedores.OrderBy(s => s.RazaoSocial);
                    break;
            }
            
            int pageSize = 4;
            return View(await Paginacao<Fornecedor>.CreateAsync(fornecedores.AsNoTracking(), _context.Fornecedor.ToList(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> SN()
        {
            return View(await _context.Fornecedor.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            var usuario = "Anônimo";
            var autenticado = false;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                usuario = HttpContext.User.Identity.Name;
                autenticado = true;
            }
            else
            {
                usuario = "Não Logado";
                autenticado = false;
            }

            ViewBag.usuario = usuario;
            ViewBag.autenticado = autenticado;
            if (id == null)
            {
                return NotFound();
            }

            var fornecedor = await _context.Fornecedor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fornecedor == null)
            {
                return NotFound();
            }

            return View(fornecedor);
        }

        public IActionResult Create()
        {
            var usuario = "Anônimo";
            var autenticado = false;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                usuario = HttpContext.User.Identity.Name;
                autenticado = true;
            }
            else
            {
                usuario = "Não Logado";
                autenticado = false;
            }

            ViewBag.usuario = usuario;
            ViewBag.autenticado = autenticado;

            var users = this._context.Usuario.Select(s => new
            {
                UId = s.UserId,
                UNome = s.Nome
            }).ToList();
            ViewBag.Resp = new SelectList(users, "UNome", "UNome");

            var cats = _context.ItemCategorias.Select(s => new
            {
                CId = s.CatId,
                CNome = s.CatNome
            }).ToList();
            ViewBag.Categorias = new MultiSelectList(cats, "CNome", "CNome");
        
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomeFantasia,RazaoSocial,Categoria,CNPJ,DataCadastro,Endereco,Cidade,Estado,Responsavel,Telefone,Email,CatItem")] Fornecedor fornecedor) 
        {
                if (_context.Fornecedor.Any(c => c.CNPJ == fornecedor.CNPJ))
            {
                ModelState.AddModelError("CNPJ", $"Esse CNPJ já está registrado.");

                var cats = _context.ItemCategorias.Select(s => new
                {
                    CId = s.CatId,
                    CNome = s.CatNome
                }).ToList();
                ViewBag.Categorias = new MultiSelectList(cats, "CNome", "CNome");
                var users = this._context.Usuario.Select(s => new
                {
                    UId = s.UserId,
                    UNome = s.Nome
                }).ToList();
                ViewBag.Resp = new SelectList(users, "UNome", "UNome");
            }


            if (_context.Fornecedor.Any(c => c.CNPJ == null))
            {
                ModelState.AddModelError("CNPJ", $"Esse CNPJ já está registrado.");

                var cats = _context.ItemCategorias.Select(s => new
                {
                    CId = s.CatId,
                    CNome = s.CatNome
                }).ToList();
                ViewBag.Categorias = new MultiSelectList(cats, "CNome", "CNome");
                var users = this._context.Usuario.Select(s => new
                {
                    UId = s.UserId,
                    UNome = s.Nome
                }).ToList();
                ViewBag.Resp = new SelectList(users, "UNome", "UNome");
            }

            if (_context.Fornecedor.Any(c => c.RazaoSocial == fornecedor.RazaoSocial))
            {
                ModelState.AddModelError("RazaoSocial", $"Essa Razão Social já está registrada.");
                var cats = _context.ItemCategorias.Select(s => new
                {
                    CId = s.CatId,
                    CNome = s.CatNome
                }).ToList();
                ViewBag.Categorias = new MultiSelectList(cats, "CNome", "CNome");
                var users = this._context.Usuario.Select(s => new
                {
                    UId = s.UserId,
                    UNome = s.Nome
                }).ToList();
                ViewBag.Resp = new SelectList(users, "UNome", "UNome");
            }
            
            if (ModelState.IsValid)
            {
                string scat = string.Join(",", fornecedor.Categoria);
                
                fornecedor.CatItem = scat;

                _context.Add(fornecedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fornecedor);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var usuario = "Anônimo";
            var autenticado = false;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                usuario = HttpContext.User.Identity.Name;
                autenticado = true;
            }
            else
            {
                usuario = "Não Logado";
                autenticado = false;
            }

            ViewBag.usuario = usuario;
            ViewBag.autenticado = autenticado;

            if (id == null)
            {
                return NotFound();
            }

            var users = this._context.Usuario.Select(s => new
            {
                UId = s.UserId,
                UNome = s.Nome
            }).ToList();
            ViewBag.Resp = new SelectList(users, "UNome", "UNome");

            var cats = _context.ItemCategorias.Select(s => new
            {
                CId = s.CatId,
                CNome = s.CatNome
            }).ToList();
            ViewBag.Categorias = new MultiSelectList(cats, "CNome", "CNome");

            Fornecedor fornecedor = _context.Fornecedor.Find(id);


           List<string> sl = fornecedor.CatItem.Split(',').ToList();

           fornecedor.Categoria = sl;


          fornecedor = await _context.Fornecedor.FindAsync(id);
            if (fornecedor == null)
            {
                return NotFound();
            }
            return View(fornecedor);
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeFantasia,RazaoSocial,Categoria,CNPJ,DataCadastro,Endereco,Cidade,Estado,Responsavel,Telefone,Email,CatItem")] Fornecedor fornecedor)
        {
            if (id != fornecedor.Id)
            {
                return NotFound();
            }                

            if (ModelState.IsValid)
            {
                string scat = string.Join(", ", fornecedor.Categoria);

                fornecedor.CatItem = scat;

                try
                {
                    _context.Update(fornecedor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FornecedorExists(fornecedor.Id))
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
            return View(fornecedor);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedor = await _context.Fornecedor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fornecedor == null)
            {
                return NotFound();
            }

            return View(fornecedor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fornecedor = await _context.Fornecedor.FindAsync(id);
            _context.Fornecedor.Remove(fornecedor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FornecedorExists(int id)
        {
            return _context.Fornecedor.Any(e => e.Id == id);
        }
    }
}
