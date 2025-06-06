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
    public class AdmsController : Controller
    {
        private readonly AppDbContext _context;

        public AdmsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Adms
        public async Task<IActionResult> Index()
        {
            return View(await _context.Adms.ToListAsync());
        }

        // GET: Adms/Details/5
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

            var adm = await _context.Adms
                .FirstOrDefaultAsync(m => m.idAdm == id);
            if (adm == null)
            {
                return NotFound();
            }

            return View(adm);
        }

        // GET: Adms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Adms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idAdm,nomePromocao,Senha,Email")] Adm adm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adm);
        }

        // GET: Adms/Edit/5
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

            var adm = await _context.Adms.FindAsync(id);
            if (adm == null)
            {
                return NotFound();
            }
            return View(adm);
        }

        // POST: Adms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idAdm,nomePromocao,Senha,Email")] Adm adm)
        {
            if (id != adm.idAdm)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdmExists(adm.idAdm))
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
            return View(adm);
        }

        // GET: Adms/Delete/5
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

            var adm = await _context.Adms
                .FirstOrDefaultAsync(m => m.idAdm == id);
            if (adm == null)
            {
                return NotFound();
            }

            return View(adm);
        }

        // POST: Adms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adm = await _context.Adms.FindAsync(id);
            if (adm != null)
            {
                _context.Adms.Remove(adm);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdmExists(int id)
        {
            return _context.Adms.Any(e => e.idAdm == id);
        }
    }
}
