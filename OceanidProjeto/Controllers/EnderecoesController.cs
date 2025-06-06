using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OceanidProjeto.Data;
using OceanidProjeto.Models;

namespace OceanidProjeto.Controllers
{
    public class EnderecoesController : Controller
    {
        private readonly AppDbContext _context;

        public EnderecoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Enderecoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Enderecos.ToListAsync());
        }

        // GET: Enderecoes/Details/5
<<<<<<< HEAD
        public async Task<IActionResult> Details(int? id)
=======
        public async Task<IActionResult> Details(int id)
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9
        {
            if (id == null)
            {
                return NotFound();
            }

            var endereco = await _context.Enderecos
                .FirstOrDefaultAsync(m => m.idEnd == id);
            if (endereco == null)
            {
                return NotFound();
            }

            return View(endereco);
        }

        // GET: Enderecoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Enderecoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
<<<<<<< HEAD
        public async Task<IActionResult> Create([Bind("idEnd,Cep,Numero,Logradouro,Complemento,Bairro,Estado,Cidade")] Endereco endereco)
=======
        public async Task<IActionResult> Create([Bind("idEnd,cepEnd,numeroEnd,logradouro,complemento,bairro,estado,cidade")] Endereco endereco)
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9
        {
            if (ModelState.IsValid)
            {
                _context.Add(endereco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(endereco);
        }

        // GET: Enderecoes/Edit/5
<<<<<<< HEAD
        public async Task<IActionResult> Edit(int? id)
=======
        public async Task<IActionResult> Edit(int id)
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9
        {
            if (id == null)
            {
                return NotFound();
            }

            var endereco = await _context.Enderecos.FindAsync(id);
            if (endereco == null)
            {
                return NotFound();
            }
            return View(endereco);
        }

        // POST: Enderecoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
<<<<<<< HEAD
        public async Task<IActionResult> Edit(int id, [Bind("idEnd,Cep,Numero,Logradouro,Complemento,Bairro,Estado,Cidade")] Endereco endereco)
=======
        public async Task<IActionResult> Edit(int id, [Bind("idEnd,cepEnd,numeroEnd,logradouro,complemento,bairro,estado,cidade")] Endereco endereco)
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9
        {
            if (id != endereco.idEnd)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(endereco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnderecoExists(endereco.idEnd))
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
            return View(endereco);
        }

        // GET: Enderecoes/Delete/5
<<<<<<< HEAD
        public async Task<IActionResult> Delete(int? id)
=======
        public async Task<IActionResult> Delete(int id)
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9
        {
            if (id == null)
            {
                return NotFound();
            }

            var endereco = await _context.Enderecos
                .FirstOrDefaultAsync(m => m.idEnd == id);
            if (endereco == null)
            {
                return NotFound();
            }

            return View(endereco);
        }

        // POST: Enderecoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var endereco = await _context.Enderecos.FindAsync(id);
            if (endereco != null)
            {
                _context.Enderecos.Remove(endereco);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnderecoExists(int id)
        {
            return _context.Enderecos.Any(e => e.idEnd == id);
        }
    }
}
