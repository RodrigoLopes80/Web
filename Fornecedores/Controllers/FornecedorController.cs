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

namespace Fornecedores.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly Contexto _context;

        public FornecedorController(Contexto context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }


        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
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
                //    fornecedores = fornecedores.OrderByDescending(s => s.EnrollmentDate);
                //    break;
                default:
                    fornecedores = fornecedores.OrderBy(s => s.RazaoSocial);
                    break;
            }

            int pageSize = 4;
            return View(await Paginacao<Fornecedor>.CreateAsync(fornecedores.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> SN()
        {
            return View(await _context.Fornecedor.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
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

        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomeFantasia,RazaoSocial,Categoria,CNPJ,DataCadastro,Endereco,Cidade,Estado,Responsavel,Telefone,Email")] Fornecedor fornecedor)
        {

            if (_context.Fornecedor.Any(c => c.CNPJ == fornecedor.CNPJ))
            {
                ModelState.AddModelError("CNPJ", $"Esse CNPJ já está registrado.");
            }
            if (_context.Fornecedor.Any(c => c.RazaoSocial == fornecedor.RazaoSocial))
            {
                ModelState.AddModelError("RazaoSocial", $"Essa Razão Social já está registrada.");
            }
            
            if (ModelState.IsValid)
            {
                _context.Add(fornecedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fornecedor);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedor = await _context.Fornecedor.FindAsync(id);
            if (fornecedor == null)
            {
                return NotFound();
            }
            return View(fornecedor);
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeFantasia,RazaoSocial,Categoria,CNPJ,DataCadastro,Endereco,Cidade,Estado,Responsavel,Telefone,Email")] Fornecedor fornecedor)
        {
            if (id != fornecedor.Id)
            {
                return NotFound();
            }
                

            if (ModelState.IsValid)
            {
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
 
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FornecedorExists(int id)
        {
            return _context.Fornecedor.Any(e => e.Id == id);
        }
    }


}
